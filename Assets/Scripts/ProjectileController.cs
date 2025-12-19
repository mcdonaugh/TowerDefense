using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage = 2;
    public EnemyController _target{get; set;}
    private float _moveSpeed = 20f;

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
        }

        Destruct();
    }

    public void Move(EnemyController target)
    {
        _target = target;
        Vector3 direction = _target.transform.position - transform.position; 
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }  

    private void Destruct()
    {
        gameObject.SetActive(false);
    }
}
