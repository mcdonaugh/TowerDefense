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
}
