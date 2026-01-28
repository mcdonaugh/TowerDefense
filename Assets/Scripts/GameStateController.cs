using System;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Canvas _mainMenuUI;
    [SerializeField] private Canvas _gameUI;
    [SerializeField] private EnemySpawnerController _enemySpawnerController;
    private bool _gameIsActive;
    private void Awake()
    {
        ResetGame();
    }

    public void StartGame()
    {
        _gameIsActive = true;
        _gameUI.gameObject.SetActive(true);
        _enemySpawnerController.gameObject.SetActive(true);

        _mainMenuUI.gameObject.SetActive(false);
    }

    private void ResetGame()
    {
        _gameIsActive = false;
        _gameUI.gameObject.SetActive(false);
        _enemySpawnerController.gameObject.SetActive(false);

        _mainMenuUI.gameObject.SetActive(true);
    }
}
