using System.Collections;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private LineManager _lineManager;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _maxSpawn = 3;
    public EnemyController[] Enemies;
    private int _maxEnemies = 20;
    private bool _hasSpawned;

    private void Awake()
    {
        Enemies = new EnemyController[_maxEnemies];
        GenerateEnemyPool(); 
    }
    
    private void Update()
    {
        if(!_hasSpawned)
        {
            _hasSpawned = true;
            StartCoroutine(SpawnWave());
        }
    }
    private IEnumerator SpawnWave()
    {
        StartCoroutine(GenerateEnemies());
        yield return new WaitForSeconds(_spawnTime);
        _hasSpawned = false;
    }
    
    private IEnumerator GenerateEnemies()
{
    int enemiesToSpawn = UnityEngine.Random.Range(1, _maxSpawn + 1);

    for(int i = 0; i < enemiesToSpawn; i++)
    {
        for(int j = 0; j < Enemies.Length; j++)
        {
            if(!Enemies[j].gameObject.activeInHierarchy)
            {
                EnemyController enemy = Enemies[j];
                enemy.transform.position = _lineManager.BotLane[0].transform.position;
                enemy.gameObject.SetActive(true);
                break;
            }
        }

        yield return new WaitForSeconds(.2f);
    }   
}
    private void GenerateEnemyPool()
    {
        for(int i = 0; i < _maxEnemies; i++)
        {
            Enemies[i] = Instantiate(_enemy);
            Enemies[i].LineManager = _lineManager;
        }
    }


}