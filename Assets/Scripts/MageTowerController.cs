using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTowerController : MonoBehaviour
{
    [SerializeField] private float _shootSpeed = 1f;
    [SerializeField] private float _buildTime = 0f;
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _crystal;
    private MeshFilter _meshFilter;
    [SerializeField] Mesh _buildingMesh;
    [SerializeField] Mesh _builtMesh;
    [SerializeField] Mesh _destroyedMesh;
    private List<HealthController> _targetQueue = new List<HealthController>();
    
    private bool _hasShot;
    private bool _isBuilt;

    private void Awake()
    {
        _meshFilter = _model.GetComponent<MeshFilter>();
        _meshFilter.mesh = _buildingMesh;
    }
    private void Start()
    {
        _isBuilt = false;

        if(!_isBuilt)
        {
            StartCoroutine(Build());
        }
    }
    private void Update()
    {
        if(_isBuilt && !_hasShot)
        {
            Shoot();
        }

        if (_model.activeInHierarchy == false)
        {
            _isBuilt = false;
            DestroyTower();
        }
    }

    private IEnumerator Build()
    {
        yield return new WaitForSeconds(_buildTime);
        _meshFilter.mesh = _builtMesh;
        _isBuilt = true; 
    }
    private void OnTriggerEnter(Collider other)
    {
        HealthController enemy = other.GetComponent<HealthController>();
        _targetQueue.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        HealthController enemy = other.GetComponent<HealthController>();
        _targetQueue.Remove(enemy);
    }

    private void Shoot()
    {
        _targetQueue.RemoveAll(HealthController => HealthController == null || !HealthController.gameObject.activeInHierarchy);
        
        if (_targetQueue.Count > 0)
        {   
            _hasShot = true;
            StartCoroutine(GenerateProjectile(_targetQueue[0]));
        }
    }

    private IEnumerator GenerateProjectile(HealthController target)
    {
        ProjectileController projectile = Instantiate(_projectileController);
        projectile.transform.position = _crystal.transform.position;
        projectile.Move(target);
        yield return new WaitForSeconds(_shootSpeed);
        _hasShot = false;
    }

    private void DestroyTower()
    {
        gameObject.SetActive(false);
    }

    

}
