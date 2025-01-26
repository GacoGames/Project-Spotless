using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask interactionLayer; // Only interactable layers will be considered

    private GameplayObject currentHoveredObject;

    private void Update()
    {
        HandleHoverAndClick();
    }

    private void HandleHoverAndClick()
    {
        if (mainCamera == null) return;

        // Raycast from the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, interactionLayer);

        GameplayObject hitObject = null;

        if (hit.collider != null)
        {
            // Check if the object is a GameplayObject
            hitObject = hit.collider.GetComponent<GameplayObject>();
            if (hitObject != null && hitObject.isInteractable)
            {
                if (hitObject != currentHoveredObject)
                {
                    // Call OnHoverEnd on the previously hovered object
                    currentHoveredObject?.OnHoverEnd();

                    // Call OnHover on the new object
                    hitObject.OnHover();
                }
            }
        }

        // If no object is hit or the hit object is not interactable
        if (hitObject == null && currentHoveredObject != null)
        {
            // Call OnHoverEnd on the previously hovered object
            currentHoveredObject.OnHoverEnd();
        }

        // Update the current hovered object
        currentHoveredObject = hitObject;

        // Check for mouse click
        if (currentHoveredObject != null && Input.GetMouseButtonDown(0))
        {
            currentHoveredObject.OnClick();
        }
    }
}
