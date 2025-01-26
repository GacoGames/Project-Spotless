using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class CopyRectTransform : MonoBehaviour
{
    [SerializeField] private RectTransform referenceRectTransform;
    [SerializeField] private Vector2 padding;

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private bool smoothTransition = true;

    void Update()
    {
        if (referenceRectTransform == null) return;

        CopySize();
    }

    private void CopySize()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        if (textMeshPro == null) textMeshPro = referenceRectTransform.GetComponent<TextMeshProUGUI>();

        // if (string.IsNullOrEmpty(textMeshPro.text))
        // {
        //     gameObject.SetActive(false);
        //     return;
        // }

        // if (!gameObject.activeSelf)
        // {
        //     gameObject.SetActive(true);
        // }

        Vector2 newSize;
        Vector3 referencePosition = referenceRectTransform.localPosition;

        if (textMeshPro != null)
        {
            // Force TextMeshPro to update its geometry
            textMeshPro.ForceMeshUpdate();

            // Get the bounds of the text
            Bounds textBounds = textMeshPro.textBounds;

            // Calculate the size with padding
            newSize = new Vector2(textBounds.size.x, textBounds.size.y) + padding;
        }
        else
        {
            // Use reference RectTransform size
            newSize = referenceRectTransform.rect.size + padding;
        }

        // Instantly set size and position
        rectTransform.sizeDelta = newSize;
        rectTransform.localPosition = referencePosition;
    }
}