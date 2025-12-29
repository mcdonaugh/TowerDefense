using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage = 2;
    public EnemyController _target{get; set;}
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        Move(_target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyController>() != null)
        {
            EnemyController target = other.GetComponent<EnemyController>();
            target.TakeDamage(_damage);
            Destruct();
        }
        else
        {
            Destruct();
        }
    }

    private void Destruct()
    {
        Debug.Log("Destruct");
        gameObject.SetActive(false);
    }

    public void Move(EnemyController target)
    {
        _target = target;
        Vector3 direction = _target.transform.position - transform.position; 
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

}
