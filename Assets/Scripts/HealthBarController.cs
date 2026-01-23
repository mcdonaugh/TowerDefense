using TMPro;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] HealthController _healthController;
    [SerializeField] GameObject _healthFill;
    [SerializeField] TMP_Text _healthText;
    

    private void Awake()
    {
        Reset();
    }

    private void OnEnable() => _healthController.UpdateHealth += OnUpdateHealthActionHandler;

    private void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }

    private void OnUpdateHealthActionHandler()
    {
        UpdateHealthText();
        UpdateHealthFill();
    }

    private void UpdateHealthFill()
    {
        _healthFill.transform.localScale = new Vector2((float)_healthController.CurrentHealth/(float)_healthController.MaxHealth,1);
    }

    private void UpdateHealthText()
    {
        _healthText.SetText($"{_healthController.CurrentHealth}/{_healthController.MaxHealth}");
    }

    private void Reset()
    {
        _healthText.SetText($"{_healthController.MaxHealth}/{_healthController.MaxHealth}");
        _healthFill.transform.localScale =  new Vector3(1,1);
    }
}
