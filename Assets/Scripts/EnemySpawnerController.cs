using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private LineManager _lineManager;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _maxSpawn = 3;
    private EnemyController[] _enemies;
    private int _maxEnemies = 20;
    private bool _hasSpawned;

    private void Awake()
    {
        _enemies = new EnemyController[_maxEnemies];
    }

    private void Start()
    {
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
        for(int j = 0; j < _enemies.Length; j++)
        {
            if(!_enemies[j].gameObject.activeInHierarchy)
            {
                EnemyController enemy = _enemies[j];
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
            _enemies[i] = Instantiate(_enemy);
            _enemies[i].LineManager = _lineManager;
        }
    }


}