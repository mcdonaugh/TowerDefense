using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _threshhold = .1f;
    [SerializeField] private List<PathAnchor> _pathAnchors = new List<PathAnchor>();
    [SerializeField] private int _damage;
    private PathAnchor _nextPosition;
    private HealthScript _currentOpponent;
    private int _positionIndex;
    private bool _isAttacking;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _isAttacking = false;
    }

    private void Start()
    {
        _nextPosition = _pathAnchors[0];
    }

    private void Update()
    {

        
        float distance = Vector3.Distance(_nextPosition.transform.position, transform.position);

        if (distance <= _threshhold)
        {   
            if (_positionIndex < _pathAnchors.Count)
            {
                _nextPosition = _pathAnchors[_positionIndex++];
                LookAt();
            }
        }

        Move();
        DrawLines();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            _currentOpponent = other.GetComponent<HealthScript>();
            _isAttacking = true;
            StartCoroutine(Attack());
        }
        else
        {
            _isAttacking = false;
        }
    }


    private void LookAt()
    {
        Vector3 direction = _nextPosition.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,angle,0);
        _animator.Play("Walk");
    }

    private void Move()
    {
        Vector3 direction = _nextPosition.transform.position - transform.position;

        if (!_isAttacking && _positionIndex < _pathAnchors.Count + 1)
        {
            transform.position += direction.normalized * _moveSpeed * Time.deltaTime;
        }
    }

    private IEnumerator Attack()
    {
        while (_currentOpponent._currentHealth > 0)
        {
            _animator.Play("Attack");
            Debug.Log(_currentOpponent._currentHealth);
            yield return new WaitForSeconds(1f);
            _currentOpponent._currentHealth -= _damage;
        }
        _animator.Play("Walk");
        Debug.Log(_currentOpponent._currentHealth);
        _isAttacking = false;
        
    }

    private void DrawLines()
    {
        Debug.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y,_nextPosition.transform.position.z), Color.blue);
        Debug.DrawLine(new Vector3(transform.position.x,transform.position.y,_nextPosition.transform.position.z),_nextPosition.transform.position, Color.red);
        Debug.DrawLine(transform.position, _nextPosition.transform.position, Color.magenta);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        
        for (int i = 0; i < _pathAnchors.Count - 1; i++)
        {
            Gizmos.DrawLine(_pathAnchors[i].transform.position, _pathAnchors[i+1].transform.position);
        }

    }
}
