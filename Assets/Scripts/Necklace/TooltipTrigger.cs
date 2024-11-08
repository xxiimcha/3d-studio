using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipMessage;      // The message to display
    public TooltipManager tooltipManager; // Reference to TooltipManager

    // When the pointer enters, show the tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipManager.ShowTooltip(tooltipMessage, Input.mousePosition);
    }

    // When the pointer exits, hide the tooltip
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipManager.HideTooltip();
    }
}
