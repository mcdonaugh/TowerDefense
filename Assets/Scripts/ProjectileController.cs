using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage = 2;
    [SerializeField] private float _moveSpeed;
    private EnemyController _target;
    private Vector3 _direction;


    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);


        if(other.GetComponent<EnemyController>() != null)
        {
            EnemyController target = other.GetComponent<EnemyController>();
            target.TakeDamage(_damage);
        }
        
        Destruct();
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDestruct());
    }

    private void Destruct()
    {
        gameObject.SetActive(false);
    }

    public void SetTarget(EnemyController target)
    {
        _target = target;
        Vector3 _direction = (_target.transform.position - transform.position).normalized;
        transform.LookAt(_target.transform.position);  
    }

    private void Move()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2);
        Destruct();
    }

}
