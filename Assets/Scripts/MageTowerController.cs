using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTowerController : MonoBehaviour
{
    [SerializeField] private float _shootSpeed = 1;
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private GameObject _model;
    private List<HealthController> _targetQueue = new List<HealthController>();
    private bool _hasShot;


    private void Update()
    {
        if(!_hasShot)
        {
            Shoot();
        }

        if (_model.activeInHierarchy == false)
        {
            gameObject.SetActive(false);
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
        yield return new WaitForSeconds(_shootSpeed);
        _hasShot = false;
    }
    

}
