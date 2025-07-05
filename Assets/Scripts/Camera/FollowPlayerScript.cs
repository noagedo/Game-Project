using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    private Vector3 offset;
    private Quaternion fixedRotation;

    void Start()
    {
        
        offset = transform.position - target.position;

      
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        
        transform.rotation = fixedRotation;
    }
}
