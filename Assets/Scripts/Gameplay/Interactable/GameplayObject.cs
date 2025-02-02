using UnityEngine;

public abstract class GameplayObject : MonoBehaviour
{
    public abstract string ObjectName { get; }
    public bool isInteractable = true; // Whether the object can currently be interacted with

    public virtual void OnHover()
    {
        if (isInteractable && HoverText.Instance != null)
        {
            HoverText.Instance.ShowHoverText(ObjectName);
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
        Debug.Log($"{ObjectName} clicked!");
    }
}
