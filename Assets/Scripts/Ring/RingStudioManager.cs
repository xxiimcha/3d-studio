using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RingStudioManager : MonoBehaviour
{
    public GameObject ringDetailsMenu;
    public GameObject ringDetailsMenuColor;
    public GameObject ring;
    public GameObject ringStone;
    public Button saveBtn;
    public Button toMainBtn;
    public Button nextBtn;
    public Button UInextBtn;
    public Button backBtn;
    public Button necklaceBtn;
    public Button ringBtn;
    public Button singleRingTypeBtn;
    public Button triStoneRingTypeBtn;
    public TMP_InputField ringSizeInputField;
    // Start is called before the first frame update
    void Start()
    {
        toMainBtn.onClick.AddListener(() => GameManager.Instance.OnToMainButtonClicked());
        saveBtn.onClick.AddListener(() => GameManager.Instance.OnSaveButtonClicked());
        nextBtn.onClick.AddListener(() => GameManager.Instance.OnNextButtonClicked(ringDetailsMenu, ringDetailsMenuColor));
        UInextBtn.onClick.AddListener(() => GameManager.Instance.OnNextButtonClicked(ringDetailsMenu, ringDetailsMenuColor));
        backBtn.onClick.AddListener(() => GameManager.Instance.OnBackButtonClicked(ringDetailsMenu, ringDetailsMenuColor));
        necklaceBtn.onClick.AddListener(() => GameManager.Instance.OnNecklaceButtonClicked());
        ringBtn.onClick.AddListener(() => GameManager.Instance.OnRingButtonClicked());

        ringSizeInputField.onEndEdit.AddListener(SetRingSize);
        ChangeRingColor(ring, GameManager.Instance.ringColor);
        ChangeRingStone(ringStone, GameManager.Instance.ringStone);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRingType(string ringType)
    {
        if(ringType == "Single")
        {
            GameManager.Instance.ringTypePrice = 19000;
            GameManager.Instance.ringType = ringType;
        } else if(ringType == "Tri-Stone")
        {
            GameManager.Instance.ringTypePrice = 15000;
            GameManager.Instance.ringType = ringType;
        }
        
    }

    public void SetRingColor(int ringColor)
    {  
        GameManager.Instance.ringColor = (RingColor)ringColor;
        ChangeRingColor(ring, GameManager.Instance.ringColor);
    }

    public void SetRingSize(string ringSize)
    {
        GameManager.Instance.ringSize = ringSize;
    }

    public void SetRingStone(string rStone)
    {
        GameManager.Instance.ringStone = rStone;
        ChangeRingStone(ringStone, GameManager.Instance.ringStone);
    }

    // Function to change the color of a GameObject's material
    public void ChangeRingColor(GameObject obj, RingColor newColor)
    {
        Color nColor = Color.clear;
        switch (newColor)
        {
            case RingColor.Silver:
                nColor = Color.white;
                break;
            case RingColor.Gold:
                nColor = Color.yellow;
                break;
            case RingColor.RoseGold:
                nColor = Color.red;
                break;
            default:
                break;
        }
        // Get the Renderer component from the GameObject
        Renderer renderer = obj.GetComponent<Renderer>();

        // Ensure the GameObject has a Renderer and a material to modify
        if (renderer != null && renderer.material != null)
        {
            // Set the color property for URP Lit shader
            renderer.material.SetColor("_BaseColor", nColor);
        }
        else
        {
            Debug.LogWarning("GameObject does not have a Renderer or a material attached.");
        }
    }

    // Function to change the color of a GameObject's material
    public void ChangeRingStone(GameObject obj, string ringStone)
    {
        Color rsColor = Color.clear;
        switch (ringStone)
        {
            case "Diamond":
                rsColor = Color.white;
                break;
            case "Sappire":
                rsColor = Color.blue;
                break;
            case "Emerald":
                rsColor = Color.green;
                break;
            case "Ruby":
                rsColor = Color.red;
                break;
            default:
                break;
        }
        // Get the Renderer component from the GameObject
        Renderer renderer = obj.GetComponent<Renderer>();

        // Ensure the GameObject has a Renderer and a material to modify
        if (renderer != null && renderer.material != null)
        {
            // Set the color property for URP Lit shader
            renderer.material.SetColor("_BaseColor", rsColor);
        }
        else
        {
            Debug.LogWarning("GameObject does not have a Renderer or a material attached.");
        }
    }
}
