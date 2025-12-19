using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage = 2;
    public HealthController _target{get; set;}
    private float _moveSpeed = 20f;

    private void Update()
    {
        Move(_target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HealthController>() != null)
        {
            HealthController target = other.GetComponent<HealthController>();
            target.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }

    public void Move(HealthController target)
    {
        _target = target;
        Vector3 direction = _target.transform.position - transform.position; 
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }  
}
