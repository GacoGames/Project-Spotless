using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverText : MonoBehaviour
{
    public static HoverText Instance { get; private set; } // Static instance for global access
    public TextMeshProUGUI hoverText;     // Reference to the UI text element
    public Vector2 textOffset = new Vector2(15f, -15f); // Offset from the mouse position

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (hoverText != null)
        {
            hoverText.text = string.Empty;
        }
    }

    private void Update()
    {
        UpdateHoverTextPosition();
    }

    public void ShowHoverText(string text)
    {
        if (hoverText != null)
        {
            hoverText.text = text;
        }
    }

    public void HideHoverText()
    {
        if (hoverText != null)
        {
            hoverText.text = string.Empty;
        }
    }

    private void UpdateHoverTextPosition()
    {
        if (hoverText != null && hoverText.gameObject.activeSelf)
        {
            Vector2 mousePosition = Input.mousePosition;
            hoverText.transform.position = mousePosition + textOffset;
        }
    }
}
