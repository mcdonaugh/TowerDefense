using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    [SerializeField] protected float _colliderRadius;
    [SerializeField] protected ProjectileController _projectile;
    [SerializeField] protected float _actionTime;
    protected Transform _projectileOrigin;
    protected SphereCollider _detectionCollider;
    protected List<EnemyController> _targetQueue = new List<EnemyController>();
    protected int _maxProjectiles = 3;

    public float ColliderRadius
    {
        get => _colliderRadius;
        protected set => _colliderRadius = value;
    }

    public List<EnemyController> TargetQueue
    {
        get => _targetQueue;
    }

    public virtual void OnTowerPlaced()
    {
        _detectionCollider = gameObject.AddComponent<SphereCollider>();
        _detectionCollider.radius = _colliderRadius;
        _detectionCollider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        
        if(enemy != null)
        {
            _targetQueue.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if(enemy != null)
        {
            _targetQueue.Remove(enemy);
        }
    }
}
