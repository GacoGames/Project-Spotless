using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    [Header("Pan Settings")]
    public float dragSpeed = 2.0f;
    public float keyPanSpeed = 5.0f; // Speed for WASD/Arrow key input
    public float inertiaDamping = 5.0f;

    [Header("Boundary Settings")]
    public Collider2D boundaryCollider; // 2D Collider to define boundaries

    private Vector3 dragOrigin;
    private Vector3 velocity;
    private bool isDragging;

    private void Update()
    {
        HandleMouseInput();
        HandleKeyboardInput();
        ApplyInertia();
        ClampCameraToBoundary();
    }

    private void HandleMouseInput()
    {
        // Start dragging
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
            velocity = Vector3.zero; // Reset velocity when dragging starts
        }

        // While dragging
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = dragOrigin - currentMousePosition;

            // Move the camera
            Vector3 movement = new Vector3(difference.x, difference.y, 0) * dragSpeed * Time.deltaTime;
            transform.position += movement;

            // Update drag origin
            dragOrigin = currentMousePosition;

            // Store velocity for inertia
            velocity = movement;
        }

        // Stop dragging
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void HandleKeyboardInput()
    {
        // WASD or Arrow key input
        Vector3 inputDirection = new Vector3(
            Input.GetAxis("Horizontal"), // A/D or Left/Right
            Input.GetAxis("Vertical"),   // W/S or Up/Down
            0
        );

        if (inputDirection != Vector3.zero)
        {
            Vector3 movement = inputDirection * keyPanSpeed * Time.deltaTime;
            transform.position += movement;

            // Store velocity for inertia
            velocity = movement;
        }
    }

    private void ApplyInertia()
    {
        if (!isDragging && velocity != Vector3.zero)
        {
            // Apply inertia to camera movement
            transform.position += velocity;

            // Gradually reduce velocity over time
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * inertiaDamping);

            // Stop completely when velocity is very small
            if (velocity.magnitude < 0.01f)
            {
                velocity = Vector3.zero;
            }
        }
    }

    private void ClampCameraToBoundary()
    {
        if (boundaryCollider != null)
        {
            Bounds bounds = boundaryCollider.bounds;
            Vector3 clampedPosition = transform.position;

            clampedPosition.x = Mathf.Clamp(clampedPosition.x, bounds.min.x, bounds.max.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, bounds.min.y, bounds.max.y);

            transform.position = clampedPosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the boundary collider in the editor for visualization
        if (boundaryCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(boundaryCollider.bounds.center, boundaryCollider.bounds.size);
        }
    }
}
