using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] [Range(0.01f, 1f)] 
    private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    private Vector3 velocity = Vector3.zero; // (0, 0, 0)

    private void LateUpdate()
    {
        // tüm upadatelerden sonra çalýþýr
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}   
