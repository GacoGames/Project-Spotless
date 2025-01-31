using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CopyRectTransform : MonoBehaviour
{
    [SerializeField] private RectTransform referenceRectTransform;
    [SerializeField] private Vector2 padding;

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private Image image;

    private void LateUpdate()
    {
        if (referenceRectTransform == null) return;
        CopySize();
    }

    private void CopySize()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        if (textMeshPro == null) textMeshPro = referenceRectTransform.GetComponent<TextMeshProUGUI>();
        if (image == null) image = GetComponent<Image>();

        Vector2 newSize;
        Vector3 referencePosition = referenceRectTransform.localPosition;

        if (textMeshPro != null)
        {
            // Force canvas update to ensure layout is processed
            Canvas.ForceUpdateCanvases();
            // Update text mesh to reflect changes
            textMeshPro.ForceMeshUpdate();

            // Get updated text bounds
            Bounds textBounds = textMeshPro.textBounds;

            newSize = new Vector2(textBounds.size.x, textBounds.size.y) + padding;

            // Handle empty text
            if (string.IsNullOrEmpty(textMeshPro.text))
            {
                newSize = Vector2.zero;
            }
        }
        else
        {
            // Fallback to rect transform size
            newSize = referenceRectTransform.rect.size + padding;
        }

        // Apply changes
        rectTransform.sizeDelta = newSize;
        rectTransform.localPosition = referencePosition;
    }
}