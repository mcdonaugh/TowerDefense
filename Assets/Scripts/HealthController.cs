using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public event Action OnDeath;
    public event Action UpdateHealth;
    [SerializeField] public int MaxHealth;
    public int CurrentHealth {get; private set;}

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    
    private void OnEnable()
    {
        UpdateHealth?.Invoke();    
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        UpdateHealth?.Invoke();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        CurrentHealth = MaxHealth;
        OnDeath?.Invoke();
    }
}
