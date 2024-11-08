using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPanel; // Reference to TooltipPanel
    public Text tooltipText;        // Reference to TooltipText

    void Start()
    {
        tooltipPanel.SetActive(false); // Hide the tooltip initially
    }

    // Method to show the tooltip with a specific message and position
    public void ShowTooltip(string message, Vector2 position)
    {
        tooltipText.text = message;
        tooltipPanel.transform.position = position;
        tooltipPanel.SetActive(true);
    }

    // Method to hide the tooltip
    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}
