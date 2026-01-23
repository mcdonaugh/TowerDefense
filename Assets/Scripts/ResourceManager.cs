using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnerController _enemySpawnerController;
    [SerializeField] private int _gold;
    public int GoldTotal {get; private set;}

    private void OnEnable()
    {
        foreach (var enemy in _enemySpawnerController.Enemies)
        {
            HealthController enemyHealthController = enemy.GetComponent<HealthController>();
            enemyHealthController.OnDeath += OnEnemyDeathEventHandler;
        }    
    }

    private void OnEnemyDeathEventHandler()
    {
        UpdateGold(_gold);
    }

    public void UpdateGold(int amount)
    {
            GoldTotal += amount;      
    }
}
