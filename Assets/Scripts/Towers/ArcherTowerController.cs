using UnityEngine;

public class ArcherTowerController : BaseTower
{
    [SerializeField] private GameObject _archer;

    public override void OnTowerPlaced()
    {
        base.OnTowerPlaced();
        _archer = Instantiate(_archer);
        _archer.transform.SetParent(transform);
        _archer.transform.localPosition = new Vector3(0,4,0);
    }

    private void Update()
    {
        if(_targetQueue.Count > 0)
        {
        Vector3 direction = _targetQueue[0].transform.position - _archer.transform.position;
        Debug.DrawLine(_archer.transform.position, _targetQueue[0].transform.position, Color.red);
        Debug.DrawLine(_targetQueue[0].transform.position, new Vector3(_targetQueue[0].transform.position.x,_targetQueue[0].transform.position.y,_archer.transform.position.z), Color.yellow);
        Debug.DrawLine(_archer.transform.position, new Vector3(_targetQueue[0].transform.position.x,_targetQueue[0].transform.position.y,_archer.transform.position.z), Color.blue);

        float angle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg;
        
        Debug.Log(angle);

        _archer.transform.rotation = Quaternion.Euler(0,angle,0);
        
        }
    }
}
