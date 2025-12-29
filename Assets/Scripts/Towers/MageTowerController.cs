using System.Collections;
using UnityEngine;

public class MageTowerController : BaseTower
{
    [SerializeField] private float _shootSpeed = 1f;
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private GameObject _crystal;
    private bool _hasShot;

    public override void OnTowerPlaced()
    {
        base.OnTowerPlaced();
    }
    
    private void Update()
    {
        if(!_hasShot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _targetQueue.RemoveAll(EnemyController => EnemyController == null || !EnemyController.gameObject.activeInHierarchy);
        
        if (_targetQueue.Count > 0)
        {   
            _hasShot = true;
            StartCoroutine(GenerateProjectile(_targetQueue[0]));
        }
    }

    private IEnumerator GenerateProjectile(EnemyController target)
    {
        ProjectileController projectile = Instantiate(_projectileController);
        projectile.transform.position = _crystal.transform.position;
        projectile.Move(target);
        yield return new WaitForSeconds(_shootSpeed);
        _hasShot = false;
    }
}
