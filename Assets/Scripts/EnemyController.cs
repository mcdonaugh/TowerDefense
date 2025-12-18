using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _threshhold = .1f;
    [SerializeField] private List<PathAnchor> _pathAnchors = new List<PathAnchor>();
    private PathAnchor _nextPosition;
    private int _positionIndex;


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
            }
            else
            {
                _positionIndex = 0;
            }
        }

        LookAt();
        Move();
        DrawLines();
        
    }

    private void LookAt()
    {
        Vector3 direction = _nextPosition.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,angle,0);
    }

    private void Move()
    {
        Vector3 direction = _nextPosition.transform.position - transform.position;
        transform.position += direction.normalized * _moveSpeed * Time.deltaTime;
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
        Gizmos.DrawLine(_pathAnchors[_pathAnchors.Count - 1].transform.position, _pathAnchors[0].transform.position);
        Debug.Log($"Path Anchors:{_pathAnchors.Count}, Index: {_positionIndex}");

    }
}
