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
        int enemiesToSpawn = UnityEngine.Random.Range(1,_maxSpawn);

        for(int i = 0; i <= enemiesToSpawn; i++)
        {
            for(int j = 0; j < _enemies.Length; j++)
            {
                if(!_enemies[j].gameObject.activeInHierarchy)
                {
                    EnemyController enemy = _enemies[j];
                    enemy.LineManager = _lineManager;
                    enemy.transform.position = _lineManager.BotLane[0].transform.position;
                    enemy.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1);   
                }
            }
        }   
    }

    private void GenerateEnemyPool()
    {
        _enemies = new EnemyController[_maxEnemies];

        for(int i = 0; i < _maxEnemies; i++)
        {
            EnemyController enemy = Instantiate(_enemy);
            enemy.gameObject.SetActive(false);
            _enemies[i] = enemy;
        }
    }

    // private void GrabFromPool()
    // {
    //     for(int i = 0; i < _enemies.Length; i++)
    //     {
    //         if(_enemies[i].gameObject.activeInHierarchy == false)
    //         {
    //             EnemyController enemy = _enemies[i];
    //             enemy.LineManager = _lineManager;
    //             enemy.transform.position = _lineManager.BotLane[0].transform.position;
    //             enemy.gameObject.SetActive(true);
    //             yield return new WaitForSeconds(1);   
    //         }
    //     }
    // }


}