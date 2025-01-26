using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The object to follow

    [Header("Follow Settings")]
    public float followSpeed = 5.0f;
    public Vector3 offset; // Offset from the target's position

    private void LateUpdate()
    {
        if (target != null)
        {
            // Smoothly move the camera towards the target's position with offset
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
