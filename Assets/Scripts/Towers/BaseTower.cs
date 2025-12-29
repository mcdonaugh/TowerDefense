using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    [SerializeField] protected float _colliderRadius;
    protected SphereCollider _detectionCollider;
    protected List<EnemyController> _targetQueue = new List<EnemyController>();


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
        _targetQueue.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        _targetQueue.Remove(enemy);
    }
}
