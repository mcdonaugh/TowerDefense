using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTowerController : MonoBehaviour
{
    [SerializeField] private int _damage = 2;
    private List<HealthController> _targetQueue = new List<HealthController>();

    private void OnTriggerEnter(Collider other)
    {
        HealthController enemy = other.GetComponent<HealthController>();
        _targetQueue.Add(enemy);
        StartCoroutine(Shoot());
    }

    private void OnTriggerExit(Collider other)
    {
        HealthController enemy = other.GetComponent<HealthController>();
        _targetQueue.Remove(enemy);
    }

    private IEnumerator Shoot()
    {
        while (_targetQueue[0] != null && _targetQueue[0].gameObject.activeInHierarchy)
        {   
            _targetQueue[0].TakeDamage(_damage);
            _targetQueue.RemoveAll(item => item == null || !item.gameObject.activeInHierarchy);
            yield return new WaitForSeconds(2f);
        }
    }
    

}
