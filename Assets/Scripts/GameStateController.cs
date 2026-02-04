using System;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Canvas _mainMenuUI;
    [SerializeField] private Canvas _gameUI;
    [SerializeField] private Canvas _pauseUI;
    [SerializeField] private EnemySpawnerController _enemySpawnerController;
    private bool _gameIsActive;
    private bool _gameIsPaused;

    private void Awake()
    {
        ResetGame();
    }

    private void Update()
    {
        if(_gameIsActive && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if(!_gameIsPaused)
        {
            PauseGame();
            _gameIsPaused = true;
        }
        else
        {
            ResumeGame();
            _gameIsPaused = false;
        }
    }

    public void StartGame()
    {
        _gameIsActive = true;
        _gameUI.gameObject.SetActive(true);
        _pauseUI.gameObject.SetActive(false);
        _enemySpawnerController.gameObject.SetActive(true);

        _mainMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        _gameUI.gameObject.SetActive(false);

        _pauseUI.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        _gameIsActive = false;
        _gameUI.gameObject.SetActive(false);
        _pauseUI.gameObject.SetActive(false);
        _enemySpawnerController.gameObject.SetActive(false);

        _mainMenuUI.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        _gameIsActive = true;
        _gameIsPaused = false;
        _gameUI.gameObject.SetActive(true);

        _pauseUI.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
