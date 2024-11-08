using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClothesStudioManager : MonoBehaviour
{
    public GameObject clothesDetailsMenu;
    public GameObject clothesDetailsMenuColor;
    public TMP_InputField dressNameInput;
    public Slider heightSlider, bustSlider, hipsSlider, waistSlider;
    public TMP_Text heightValueTxt, bustValueTxt, hipsValueTxt, waistValueTxt;
    public Button saveBtn;
    public Button toMainBtn;
    public Button nextBtn;
    //smallicon
    public Button backBtn;
    public int dressColor;

    // Reference to all dress models in the scene
    [SerializeField] private List<GameObject> dresses;

    private void Start()
    {
        // Set initial values from GameManager
        if (GameManager.Instance != null)
        {
            dressNameInput.text = GameManager.Instance.DressName;
            heightSlider.value = GameManager.Instance.Height;
            bustSlider.value = GameManager.Instance.Bust;
            hipsSlider.value = GameManager.Instance.Hips;
            waistSlider.value = GameManager.Instance.Waist;

            heightValueTxt.text = GameManager.Instance.Height.ToString() + " inch";
            bustValueTxt.text = GameManager.Instance.Bust.ToString() + " inch";
            hipsValueTxt.text = GameManager.Instance.Hips.ToString() + " inch";
            waistValueTxt.text = GameManager.Instance.Waist.ToString() + " inch";

            // Subscribe to GameManager events
            GameManager.Instance.OnDressNameChanged += UpdateDressName;
            GameManager.Instance.OnHeightChanged += UpdateHeight;
            GameManager.Instance.OnBustChanged += UpdateBust;
            GameManager.Instance.OnHipsChanged += UpdateHips;
            GameManager.Instance.OnWaistChanged += UpdateWaist;
            GameManager.Instance.colorOnVariableChanged += UpdateColor;
        }

        // Add listeners to UI elements
        dressNameInput.onValueChanged.AddListener(OnDressNameInputChanged);
        heightSlider.onValueChanged.AddListener(OnHeightSliderChanged);
        bustSlider.onValueChanged.AddListener(OnBustSliderChanged);
        hipsSlider.onValueChanged.AddListener(OnHipsSliderChanged);
        waistSlider.onValueChanged.AddListener(OnWaistSliderChanged);

        // Add listener to call LoadMainMenu on button click
        saveBtn.onClick.AddListener(() => GameManager.Instance.OnSaveButtonClicked());
        toMainBtn.onClick.AddListener(() => GameManager.Instance.OnToMainButtonClicked());
        nextBtn.onClick.AddListener(() => GameManager.Instance.OnNextButtonClicked(clothesDetailsMenu, clothesDetailsMenuColor));
        backBtn.onClick.AddListener(() => GameManager.Instance.OnBackButtonClicked(clothesDetailsMenu, clothesDetailsMenuColor));
    }

    private void OnDressNameInputChanged(string value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DressName = value;
        }
    }

    private void OnHeightSliderChanged(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Height = value;
            heightValueTxt.text = value.ToString() + " inch";
        }
    }

    private void OnBustSliderChanged(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Bust = value;
            bustValueTxt.text = value.ToString() + " inch";
        }
    }

    private void OnHipsSliderChanged(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Hips = value;
            hipsValueTxt.text = value.ToString() + " inch";
        }
    }

    private void OnWaistSliderChanged(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Waist = value;
            waistValueTxt.text = value.ToString() + " inch";
        }
    }

    // Methods to update UI elements when GameManager variables change
    private void UpdateDressName(string value)
    {
        if (dressNameInput.text != value)
        {
            dressNameInput.text = value;
        }
    }

    private void UpdateHeight(float value)
    {
        if (heightSlider.value != value)
        {
            heightSlider.value = value;
        }
    }

    private void UpdateBust(float value)
    {
        if (bustSlider.value != value)
        {
            bustSlider.value = value;
        }
    }

    private void UpdateHips(float value)
    {
        if (hipsSlider.value != value)
        {
            hipsSlider.value = value;
        }
    }

    private void UpdateWaist(float value)
    {
        if (waistSlider.value != value)
        {
            waistSlider.value = value;
        }
    }

    private void UpdateColor(ColorEnum value)
    {
        ChangeDressColor(value);
    }

    public void OnEnable()
    {
        ChangeDress(GameManager.Instance.currentDress);
        ChangeDressColor(GameManager.Instance.Color);
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to avoid memory leaks
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDressNameChanged -= UpdateDressName;
            GameManager.Instance.OnHeightChanged -= UpdateHeight;
            GameManager.Instance.OnBustChanged -= UpdateBust;
            GameManager.Instance.OnHipsChanged -= UpdateHips;
            GameManager.Instance.OnWaistChanged -= UpdateWaist;
            GameManager.Instance.colorOnVariableChanged -= UpdateColor;
        }
    }

    // Method to update the current dress based on GameManager's dress index
    public void UpdateDressVisibility(int currentDressIndex)
    {
        for (int i = 0; i < dresses.Count; i++)
        {
            dresses[i].SetActive(i == currentDressIndex);
        }
    }

    public void ChangeDress(int dressIndex)
    {
        Debug.Log(dressIndex);
        GameManager.Instance.currentDress = dressIndex;
        UpdateDressVisibility(GameManager.Instance.currentDress);
    }

    // Method to change the color of the currently active dress
    public void ChangeDressColor(ColorEnum newColor)
    {
        Color color = Color.white;
        switch (newColor)
        {
            case ColorEnum.Red:
                color = Color.red;
                break;
            case ColorEnum.Green:
                color = Color.green;
                break;
            case ColorEnum.Yellow:
                color = Color.yellow;
                break;
            case ColorEnum.Blue:
                color = Color.blue;
                break;
            case ColorEnum.White:
                color = Color.white;
                break;
            case ColorEnum.Black:
                color = Color.black;
                break;
            default:
                break;
        }
        // Find the currently active dress
        foreach (GameObject dress in dresses)
        {
            if (dress.activeSelf) // Check if this is the active dress
            {
                // Get the Renderer component
                Renderer renderer = dress.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Set the color on the material (URP Lit material uses _BaseColor)
                    renderer.material.SetColor("_BaseColor", color);
                }
                foreach (Material mat in renderer.materials)
                {
                    string materialName = mat.name.Replace(" (Instance)", "");
                    if (materialName == "addon" || materialName == "topback.001" || materialName == "skirtfr.001" || materialName == "skirtback.001")
                    {
                        mat.SetColor("_BaseColor", color);
                    }
                }
                break;
            }
        }
    }
}
