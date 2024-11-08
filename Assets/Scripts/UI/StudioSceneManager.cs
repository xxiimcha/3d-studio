using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudioSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to load the SavesScene scene
    public void OnBackToSavesButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.MainMenu);
        SceneManager.LoadScene("SavesScene"); // Replace with your gameplay scene name
    }

    // Method to load the Studio scene
    public void OnBackToClothesButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.MainMenu);
        SceneManager.LoadScene("SavesScene"); // Replace with your gameplay scene name
    }

    // Method to load the SavesScene scene
    public void OnBackToAccessoryButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.MainMenu);
        SceneManager.LoadScene("SavesScene"); // Replace with your gameplay scene name
    }

    // Method to load the ClothesStudioScene scene
    public void OnToClothesButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.Dress);
        SceneManager.LoadScene("ClothesStudioScene"); // Replace with your gameplay scene name
    }

    // Method to load the AccessoriesStudioScene scene
    public void OnToAccessoryButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.Necklace);
        SceneManager.LoadScene("NeckLaceStudioScene"); // Replace with your gameplay scene name
    }
}
