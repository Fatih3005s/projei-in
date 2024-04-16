using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Kameranın takip edeceği hedef (genellikle oyuncu)
    public bool followOnXAxis = true; // Kameranın x ekseni boyunca mı takip edileceği
    public float smoothSpeed = 0.125f; // Kameranın takip etme hızı

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        if (followOnXAxis)
        {
            targetPosition.y = transform.position.y;
        }
        else
        {
            targetPosition.x = transform.position.x;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
