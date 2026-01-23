using System.Collections;
using System.Linq;
using UnityEngine;
public class ArcherTowerController : BaseTower
{
    [SerializeField] private GameObject _archer;
    private ProjectileController[] _projectilePool;
    private bool _hasShot;

    public override void OnTowerPlaced()
    {
        base.OnTowerPlaced();

        _archer = Instantiate(_archer);
        _archer.transform.SetParent(transform);
        _archer.transform.localPosition = new Vector3(0,4,0);

        _projectilePool = new ProjectileController[_maxProjectiles];

        _projectileOrigin = _archer.transform.Find("ProjectileOrigin");
        
        GenerateProjectilePool();
    }

    private void Update()
    {

        if(_targetQueue.Count > 0)
        {
            Vector3 direction = _targetQueue[0].transform.position - _archer.transform.position;

            // Debug.DrawLine(_archer.transform.position, _targetQueue[0].transform.position, Color.red);
            // Debug.DrawLine(_targetQueue[0].transform.position, new Vector3(_targetQueue[0].transform.position.x,_targetQueue[0].transform.position.y,_archer.transform.position.z), Color.yellow);
            // Debug.DrawLine(_archer.transform.position, new Vector3(_targetQueue[0].transform.position.x,_targetQueue[0].transform.position.y,_archer.transform.position.z), Color.blue);

            float angle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg;

            _archer.transform.rotation = Quaternion.Euler(0,angle,0);

            if(!_hasShot)
            {
                _hasShot = true;
                StartCoroutine(Shoot());
            }

        }
    }

    private void GenerateProjectilePool()
    {
        for (int i = 0; i < _maxProjectiles; i++)
        {
            ProjectileController projectile = Instantiate(_projectile);
            projectile.gameObject.SetActive(false);
            _projectilePool[i] = projectile;
        } 
    }

    private IEnumerator Shoot()
    {
        
        if (_projectilePool.Length > 0)
        {    
            foreach (var projectile in _projectilePool)
            {   
                if(_targetQueue[0]!= null && projectile.gameObject.activeInHierarchy != true)
                {
                    projectile.gameObject.SetActive(true);
                    projectile.transform.position = _projectileOrigin.transform.position;
                    projectile.transform.rotation = _projectileOrigin.transform.rotation;
                    projectile.SetTarget(_targetQueue[0]);
                    break;
                }
            }
        }

        yield return new WaitForSeconds(_actionTime);
        _hasShot = false;

        CleanupQueue();   
    }

    private void CleanupQueue()
    {
        for (int i = _targetQueue.Count - 1; i >= 0; i--)
        {
            if (_targetQueue[i] == null || !_targetQueue[i].gameObject.activeInHierarchy)
                _targetQueue.RemoveAt(i);
        }
    }

    
}
