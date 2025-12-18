using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    public int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        Die();
    }

    private void Die()
    {
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            gameObject.SetActive(false);
        }
    }
}
