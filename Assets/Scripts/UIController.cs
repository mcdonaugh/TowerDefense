using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private TMP_Text _goldAmount;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private EnemySpawnerController _enemySpawnerController;
    [SerializeField] private int _scoreIncrement;
    private int _score;

    private void Awake()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
        _goldAmount.text = _resourceManager.GoldTotal.ToString();
    }

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
        Debug.Log("Called");
        UpdateGoldText();
        UpdateScore();
    }

    private void UpdateGoldText()
    {
        _goldAmount.text = _resourceManager.GoldTotal.ToString();
    }

    private void UpdateScore()
    {
        _score += _scoreIncrement;
        _scoreText.text = _score.ToString();
    }
}
