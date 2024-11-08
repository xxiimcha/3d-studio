using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    // Static instance of SceneStateManager
    public static SceneStateManager Instance { get; private set; }

    // Current state of the scene
    public SceneState currentState;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this; // Set the instance to this
            DontDestroyOnLoad(gameObject); // Optionally, don't destroy this object on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate
        }
    }

    private void Start()
    {
        // Initialize the scene to the default state
        SetState(SceneState.MainMenu); // Set the initial state to Dress
    }

    // Method to set the current state
    public void SetState(SceneState newState)
    {
        // Exit the current state
        ExitState(currentState);

        // Change the state
        currentState = newState;

        // Enter the new state
        EnterState(currentState);
    }

    // Method to handle entering a state
    private void EnterState(SceneState state)
    {
        switch (state)
        {
            case SceneState.MainMenu:
                Debug.Log("Entering MainMenu State");
                // Add logic to activate the dress scene
                break;
            case SceneState.Dress:
                Debug.Log("Entering Dress State");
                // Add logic to activate the dress scene
                break;
            case SceneState.Ring:
                Debug.Log("Entering Ring State");
                // Add logic to activate the ring scene
                break;
            case SceneState.Necklace:
                Debug.Log("Entering Necklace State");
                // Add logic to activate the necklace scene
                break;
        }
    }

    // Method to handle exiting a state
    private void ExitState(SceneState state)
    {
        switch (state)
        {
            case SceneState.Dress:
                Debug.Log("Exiting Dress State");
                // Add logic to deactivate the dress scene
                break;
            case SceneState.Ring:
                Debug.Log("Exiting Ring State");
                // Add logic to deactivate the ring scene
                break;
            case SceneState.Necklace:
                Debug.Log("Exiting Necklace State");
                // Add logic to deactivate the necklace scene
                break;
        }
    }
}
