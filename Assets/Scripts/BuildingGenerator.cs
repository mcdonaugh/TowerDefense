using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingGenerator : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private GameObject[] _towers;
    [SerializeField] private List<BaseTower> _towerPool;
    [SerializeField] private Material _previewMaterial;
    private Material _originalMaterial;
    private List<MeshRenderer> _childMeshRenderers; 
    private GameObject _cursor; 
    private Ray _ray;
    private RaycastHit _hit;
    private bool _isInPreviewMode;
    private int _currentIndex;
    [SerializeField] private BaseTower _currentTower;

    private void Awake()
    {
        _towerPool = new List<BaseTower>();
    }
    private void Start()
    {
       _cursor = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
    }
    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHitting = Physics.Raycast(_ray, out _hit, Mathf.Infinity, _groundLayerMask);
        
        
        if (isHitting && EventSystem.current.IsPointerOverGameObject() == false)
        {
            _cursor.transform.position =_hit.point;
            // Debug.Log(_hit.transform.name);
            // Debug.DrawLine(_ray.origin, _hit.point, Color.red);

            if(_isInPreviewMode)
            {
                _currentTower.transform.position = _hit.point;

                if(Input.GetMouseButton(0))
                {
                    _currentTower.transform.position = _hit.point;
                    _currentTower.GetComponent<MeshRenderer>().material = _originalMaterial;
                    
                    foreach (var meshRenderer in _childMeshRenderers)
                    {
                        meshRenderer.GetComponent<MeshRenderer>().material = _originalMaterial;
                    }

                    _childMeshRenderers.Clear();
                    _currentTower.OnTowerPlaced();
                    ExitPreviewMode();
                }
            }    
        }
    }
    
    public void ShowPreview(int index)
    {
        if(!_isInPreviewMode)
        {
            _currentIndex = index;
            _currentTower = null;

            GetTowerFromPool();

            if(_currentTower == null)
            {
               AddTowerToPool();
            }

            _originalMaterial = _currentTower.GetComponent<MeshRenderer>().sharedMaterial;
            _childMeshRenderers = _currentTower.GetComponentsInChildren<MeshRenderer>().ToList();

            foreach (var meshRenderer in _childMeshRenderers)
            {
                meshRenderer.GetComponent<MeshRenderer>().material = _previewMaterial;
            }

            _currentTower.GetComponent<MeshRenderer>().material = _previewMaterial;
            _isInPreviewMode = true;
        }

        else
        {
            _currentTower.gameObject.SetActive(false);
            ExitPreviewMode();
        }
            
    }

    private void GetTowerFromPool()
    {
        foreach (var tower in _towerPool)
        {
            if (tower.name.Contains(_towers[_currentIndex].name) && !tower.gameObject.activeInHierarchy)
            {
                _currentTower = tower;
                _currentTower.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void AddTowerToPool()
    {
        GameObject newTower = Instantiate(_towers[_currentIndex]);
        _currentTower = newTower.GetComponent<BaseTower>();
        _towerPool.Add(_currentTower);
    }


    public void ExitPreviewMode()
    {
        if(_isInPreviewMode)
        {
            _isInPreviewMode = false;
        }
    }
}