using System.Collections;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private LineManager _lineManager;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _maxSpawn = 3;
    private bool _hasSpawned;

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
        int enemiesToSpawn = Random.Range(1,_maxSpawn);

        for(int i = 0; i <= enemiesToSpawn; i++)
        {
            EnemyController enemy = Instantiate(_enemy);
            enemy.LineManager = _lineManager;
            enemy.transform.position = _lineManager.BotLane[0].transform.position;
            yield return new WaitForSeconds(1);    
        }   
    }


}