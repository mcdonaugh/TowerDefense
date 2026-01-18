using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _turnSpeed = 4f;
    [SerializeField] private float _threshhold = .1f;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    public LineManager LineManager{get; set;}
    private int _laneToGoto;
    private List<PathAnchor> _pathAnchors = new List<PathAnchor>();
    private PathAnchor _nextPosition;
    private HealthController _currentOpponent;
    private int _positionIndex;
    private bool _isAttacking;
    private Animator _animator;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        gameObject.SetActive(false);
        _animator = GetComponentInChildren<Animator>();
        _positionIndex = 0;

    }
    void OnEnable()
    {
        _laneToGoto = Random.Range(0,3);

        if (_laneToGoto == 0)
        {
            _pathAnchors = LineManager.TopLane;
        }
        else if (_laneToGoto == 1)
        {
            _pathAnchors = LineManager.MidLane;
        }
        else
        {
            _pathAnchors = LineManager.BotLane;
        }
        _nextPosition = _pathAnchors[_positionIndex];
    }

    void OnDisable()
    {
        _isAttacking = false;
        _positionIndex = 0;
    }

    private void Update()
    {
   
        float distance = Vector3.Distance(_nextPosition.transform.position, transform.position);

        if (distance <= _threshhold && _positionIndex < _pathAnchors.Count - 1)
        {   
            _nextPosition = _pathAnchors[++_positionIndex];
        }

        Move();
        LookAt();        
        DrawLines();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthController>() == true)
        {
            _currentOpponent = other.GetComponent<HealthController>();
            _isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
        _currentHealth = _maxHealth;
    }

    private void LookAt()
    {
        Vector3 direction = _nextPosition.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0,angle,0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 direction = _nextPosition.transform.position - transform.position;
        Vector3 newDirection = new Vector3(direction.x,0,direction.z);

        if (!_isAttacking)
        {
            transform.position += newDirection.normalized * _moveSpeed * Time.deltaTime;
            _animator.Play("Walk");
        }

    }

    private IEnumerator Attack()
    {
        while (_currentOpponent.gameObject.activeInHierarchy)
        {
            _isAttacking = true;
            _animator.Play("Attack");
            _currentOpponent.TakeDamage(_damage);
            yield return new WaitForSeconds(1f);
        }

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
