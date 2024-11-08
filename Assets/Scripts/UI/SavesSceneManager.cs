using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavesSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        GameManager.Instance.LoadAllScreenshots();
    }

    // Method to load the Studio scene
    public void OnNewItemButtonClicked()
    {
        SceneManager.LoadScene("StudioScene"); // Replace with your gameplay scene name
    }
}
