using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text tooltipText;
    public RectTransform backgroundRectTransform;

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform.parent, 
            Input.mousePosition, 
            null, 
            out position
        );
        transform.localPosition = position;
    }

    public void ShowTooltip(string message)
    {
        gameObject.SetActive(true);
        tooltipText.text = message;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
