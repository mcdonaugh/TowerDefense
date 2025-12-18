using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    private void Start()
    {
        _enemy = Instantiate(_enemy); 
    }
}