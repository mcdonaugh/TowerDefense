using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTowerController : MonoBehaviour
{
    [SerializeField] private int _damage = 2;
    [SerializeField] private ProjectileController _projectileController;
    private List<HealthController> _targetQueue = new List<HealthController>();
    private bool _hasShot;

    private void Update()
    {
        if(!_hasShot)
        {
            Shoot();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        HealthController enemy = other.GetComponent<HealthController>();
        _targetQueue.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        HealthController enemy = other.GetComponent<HealthController>();
        _targetQueue.Remove(enemy);
    }

    private void Shoot()
    {
        _targetQueue.RemoveAll(HealthController => HealthController == null || !HealthController.gameObject.activeInHierarchy);
        
        if (_targetQueue.Count > 0)
        {   
            _hasShot = true;
            StartCoroutine(GenerateProjectile(_targetQueue[0]));

        }
    }

    private IEnumerator GenerateProjectile(HealthController target)
    {
        ProjectileController projectile = Instantiate(_projectileController);
        projectile.Move(target);
        yield return new WaitForSeconds(2f);
        _hasShot = false;
    }
    

}
