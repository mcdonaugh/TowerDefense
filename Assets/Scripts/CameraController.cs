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

        if (_direction.x != 0 || _direction.y != 0 )
        {
            MoveCamera();
        }

        if (transform.position.y > 4 && transform.position.y < 8)
        {
            ZoomCamera();
        }
    }

    private void GetInputDirection()
    {
        if(Input.mousePosition.x > (Screen.width - _mouseMovementArea))
        {
            _direction = new Vector2(transform.right.x,transform.right.z);
        }
        else if(Input.mousePosition.x < _mouseMovementArea)
        {
            _direction = new Vector2(-transform.right.x,-transform.right.z);
        }
        else if(Input.mousePosition.y > (Screen.height - _mouseMovementArea))
        {
            _direction = new Vector2(transform.forward.x,transform.forward.z);
        }
        else if(Input.mousePosition.y < _mouseMovementArea)
        {
            _direction = new Vector2(-transform.forward.x,-transform.forward.z);
        }
        else
        {
            _direction = new Vector2(0,0);
        }
    } 

    private void ZoomCamera()
    {
        _scrollDirection = Input.GetAxis("Mouse ScrollWheel");

        _nextZoom = transform.position + _scrollDirection * transform.forward * _zoomSpeed * Time.deltaTime;

        if (IsWithinZoomBounds(_nextZoom))
        {
            if (IsWithinBounds(_nextDestination))
            {
                transform.position += _scrollDirection * transform.forward * _zoomSpeed * Time.deltaTime;     
            }
            else
            {
                transform.position += _scrollDirection * new Vector3(0,transform.forward.y,0) * _zoomSpeed * Time.deltaTime; 
            }
        }
    }

    private void MoveCamera()
    {
        _nextDestination = transform.position + new Vector3(_direction.x,0,_direction.y) * _moveSpeed * Time.deltaTime;
        
        if(_nextDestination.x > _maxWidth || _nextDestination.x < -_maxWidth)
        {
            transform.position += new Vector3(0,0,_direction.y) * _moveSpeed * Time.deltaTime;  
        }

        if(_nextDestination.z > _maxLength || _nextDestination.z < -_maxLength)
        {
            transform.position += new Vector3(_direction.x,0,0) * _moveSpeed * Time.deltaTime;  
        }

        if(IsWithinBounds(_nextDestination))
        {
            transform.position += new Vector3(_direction.x,0,_direction.y) * _moveSpeed * Time.deltaTime;        
        }            
    }

    private bool IsWithinBounds(Vector3 nextDestination)
    {
        if (nextDestination.x < _maxWidth && nextDestination.x > -_maxWidth + _boundsOffset && nextDestination.z < _maxLength - _boundsOffset && nextDestination.z > -_maxLength) 
        {
            return true;
        }
        
        return false;
    }

    private bool IsWithinZoomBounds(Vector3 nextZoom)
    {
        if (nextZoom.y < _maxZoom && nextZoom.y > _minZoom)
        {
            return true;
        }

        return false;
    }
}
