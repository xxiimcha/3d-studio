using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static Tooltip tooltip;

    private void Awake()
    {
        tooltip = FindObjectOfType<Tooltip>();
    }

    public static void Show(string content)
    {
        if (tooltip != null)
        {
            tooltip.ShowTooltip(content);
        }
    }

    public static void Hide()
    {
        if (tooltip != null)
        {
            tooltip.HideTooltip();
        }
    }
}
