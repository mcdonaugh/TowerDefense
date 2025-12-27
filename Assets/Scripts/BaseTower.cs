using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _colliderRadius;
    protected SphereCollider _detectionCollider;
    protected BoxCollider _hitCollider;

    public int HitPoints 
    { 
        get => _maxHealth; 
        protected set => _maxHealth = value; 
    }

    public float ColliderRadius
    {
        get => _colliderRadius;
        protected set => _colliderRadius = value;
    }
    private int _currentHealth;

    public virtual void OnTowerPlaced()
    {
        Debug.Log($"{name} is placed");
        _currentHealth = _maxHealth;

        _detectionCollider = GetComponentInChildren<SphereCollider>();
        _detectionCollider.radius = _colliderRadius;

        _hitCollider = GetComponent<BoxCollider>();
        _hitCollider.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        _currentHealth = _maxHealth;
    }
}
