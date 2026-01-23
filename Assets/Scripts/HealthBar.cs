using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
