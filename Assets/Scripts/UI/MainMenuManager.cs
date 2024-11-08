using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management
using UnityEngine.UI; // Needed for UI elements

public class MainMenuManager : MonoBehaviour
{
    // You can assign these buttons in the Inspector
    public Button startButton;
    public Button settingsButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners for button clicks
        startButton.onClick.AddListener(OnStartButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    // Method to load the gameplay scene
    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("SavesScene"); // Replace with your gameplay scene name
    }

    // Method to load the options/settings scene
    private void OnSettingsButtonClicked()
    {
        //SceneManager.LoadScene("OptionsScene"); // Replace with your options scene name
    }
}
