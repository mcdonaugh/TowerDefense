using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Collider _environmentCollider;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _zoomSpeed = 5;
    [SerializeField] private float _boundsOffset = 4;
    [SerializeField] private float _maxZoom = 8;
    [SerializeField] private float _minZoom = 4;
    [SerializeField] private float _mouseMovementArea = 20;
    private Vector3 _direction;
    private float _scrollDirection;
    private float _maxWidth;
    private float _maxLength;
    private Vector3 _nextDestination;
    private Vector3 _nextZoom;

    private void Awake()
    {
        _maxLength = _environmentCollider.bounds.max.z;
        _maxWidth = _environmentCollider.bounds.max.x;
    }

    private void Update()
    {
        GetInputDirection();

        if (_direction.x != 0 || _direction.y != 0)
        {
            MoveCamera();
        }

        if (transform.position.y > _minZoom && transform.position.y < _maxZoom)
        {
            ZoomCamera();
        }
    }

    private void GetInputDirection()
    {
        if(Input.mousePosition.x > (Screen.width - _mouseMovementArea))
        {
            _direction = new Vector2(transform.right.x, transform.right.z);
        }
        else if(Input.mousePosition.x < _mouseMovementArea)
        {
            _direction = new Vector2(-transform.right.x, -transform.right.z);
        }
        else if(Input.mousePosition.y > (Screen.height - _mouseMovementArea))
        {
            _direction = new Vector2(transform.forward.x, transform.forward.z);
        }
        else if(Input.mousePosition.y < _mouseMovementArea)
        {
            _direction = new Vector2(-transform.forward.x, -transform.forward.z);
        }
        else
        {
            _direction = new Vector2(0, 0);
        }
    } 

    private void ZoomCamera()
    {
        _scrollDirection = Input.GetAxis("Mouse ScrollWheel");

        _nextZoom = transform.position + _scrollDirection * transform.forward * _zoomSpeed * Time.deltaTime;

        if (IsWithinZoomBounds(_nextZoom))
        {
            _nextDestination = _nextZoom;
            
            if (IsWithinBounds(_nextDestination))
            {
                transform.position = _nextZoom;     
            }
            else
            {
                transform.position += _scrollDirection * new Vector3(0, transform.forward.y, 0) * _zoomSpeed * Time.deltaTime; 
            }
        }
    }

    private void MoveCamera()
    {
        Vector3 movement = new Vector3(_direction.x, 0, _direction.y) * _moveSpeed * Time.deltaTime;
        _nextDestination = transform.position + movement;
        
        // Clamp each axis independently to maintain asymmetric bounds
        float clampedX = Mathf.Clamp(_nextDestination.x, -_maxWidth, _maxWidth - _boundsOffset);
        float clampedZ = Mathf.Clamp(_nextDestination.z, -_maxLength, _maxLength - _boundsOffset);
        
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }

    private bool IsWithinBounds(Vector3 nextDestination)
    {
        return nextDestination.x < _maxWidth - _boundsOffset && 
               nextDestination.x > -_maxWidth && 
               nextDestination.z < _maxLength - _boundsOffset && 
               nextDestination.z > -_maxLength;
    }

    private bool IsWithinZoomBounds(Vector3 nextZoom)
    {
        return nextZoom.y < _maxZoom && nextZoom.y > _minZoom;
    }
}