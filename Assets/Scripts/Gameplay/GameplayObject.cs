using UnityEngine;

public abstract class GameplayObject : MonoBehaviour
{
    public string objectName; // Name of the object to display in hover text
    public bool isInteractable = true; // Whether the object can currently be interacted with

    public virtual void OnHover()
    {
        if (isInteractable && HoverText.Instance != null)
        {
            HoverText.Instance.ShowHoverText(objectName);
        }
    }

    public virtual void OnHoverEnd()
    {
        if (HoverText.Instance != null)
        {
            HoverText.Instance.HideHoverText();
        }
    }

    public virtual void OnClick()
    {
        Debug.Log($"{objectName} clicked!");
    }
}
