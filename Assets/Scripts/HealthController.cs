using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        _currentHealth = _maxHealth;
    }
}
