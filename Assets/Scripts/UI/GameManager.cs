using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject screenshotPrefab;  // Prefab with an Image component
    public GameObject contentParentObject;      // Parent transform to hold the screenshot instances
    // Static instance of GameManager
    public static GameManager Instance { get; private set; }
    [Header("Dress")]
    public string _dressName;
    public float height, bust, hips, waist, price;
    public ColorEnum _color = ColorEnum.Red; // Backing field for the variable

    // Events for each variable change for Dress
    public event Action<string> OnDressNameChanged;
    public event Action<float> OnHeightChanged;
    public event Action<float> OnBustChanged;
    public event Action<float> OnHipsChanged;
    public event Action<float> OnWaistChanged;
    public event Action<ColorEnum> colorOnVariableChanged; // Event for variable changes
    public int currentDress;

    // Events for each variable change for Necklace
    public event Action<float> OnTotalPriceChanged;
    public event Action<string> OnNecklaceItemListChanged;

    [Header("Necklace")]
    public List<GameObject> necklaceItemList = new List<GameObject>();
    public List<NecklaceItem> necklaceItemList1 = new List<NecklaceItem>();
    public string locktype;
    public int length;
    public float totalPrice;
    public string necklaceItemListDet;

    [Header("Ring")]
    public string ringType;
    public string ringStone;
    public string ringSize;
    public string ringTotalPrice;
    public RingColor ringColor;
    public int ringTypePrice;
    public int ringStonePrice;

    [Header("StudioMenuUI")]
    private string saveDirectory;


    //Dress functions
    // Properties to trigger events on change
    public string DressName
    {
        get { return _dressName; }
        set
        {
            _dressName = value;
            OnDressNameChanged?.Invoke(_dressName);
        }
    }

    public float Height
    {
        get { return height; }
        set
        {
            height = value;
            Debug.Log("Height changed to: " + height);
            OnHeightChanged?.Invoke(height);
        }
    }

    public float Bust
    {
        get { return bust; }
        set
        {
            bust = value;
            OnBustChanged?.Invoke(bust);
        }
    }

    public float Hips
    {
        get { return hips; }
        set
        {
            hips = value;
            OnHipsChanged?.Invoke(hips);
        }
    }

    public float Waist
    {
        get { return waist; }
        set
        {
            waist = value;
            OnWaistChanged?.Invoke(waist);
        }
    }

    public float Price
    {
        get { return price; }
        set
        {
            price = value;
            OnWaistChanged?.Invoke(price);
        }
    }

    public ColorEnum Color
    {
        get => _color;
        set
        {
            _color = value;
            colorOnVariableChanged?.Invoke(_color); // Notify subscribers
        }
    }

    //Necklace properties and functions

    public float TotalPrice
    {
        get { return totalPrice; }
        set
        {
            totalPrice = value;
            OnTotalPriceChanged?.Invoke(totalPrice);
        }
    }

    public string NecklaceItemListDet
    {
        get { return necklaceItemListDet; }
        set
        {
            necklaceItemListDet = value;
            OnTotalPriceChanged?.Invoke(totalPrice);
        }
    }

    public void DisplayNecklaceItemListDet()
    {
        necklaceItemListDet = string.Empty;
        int tPrice = 0;
        foreach (var item in necklaceItemList)
        {
            //necklaceItemListText.text += item.GetComponent<Necklace>().accname + " = " + item.GetComponent<Necklace>().price.ToString("C", new CultureInfo("fil-PH")) + "\n";
            //tPrice += item.GetComponent<Necklace>().price;
            TotalPrice += tPrice;
        }

    }

    

    private void Awake()
    {
        // Check if instance already exists
        if (Instance != null && Instance != this)
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }
        // Set this instance as the Singleton instance
        Instance = this;
        // Make this GameManager persistent between scenes
        DontDestroyOnLoad(gameObject);

        // Set up the save directory path
        saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    private void Start()
    {
        // Initialization logic here
        Debug.Log("GameManager Initialized");
    }

    public void OnNextButtonClicked(GameObject firstUI, GameObject secondUI)
    {
        firstUI.SetActive(false);
        secondUI.SetActive(true);
    }

    public void OnSaveButtonClicked()
    {
        GameManager.Instance.SaveGameData();
    }

    public void OnBackButtonClicked(GameObject firstUI, GameObject secondUI)
    {
        firstUI.SetActive(true);
        secondUI.SetActive(false);
    }

    public void OnToMainButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnNecklaceButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.Necklace);
        SceneManager.LoadScene("NecklaceStudioScene");
    }

    public void OnRingButtonClicked()
    {
        SceneStateManager.Instance.SetState(SceneState.Ring);
        SceneManager.LoadScene("RingStudioScene");
    }

    public void SaveGameData()
    {
        object data = null;
        // Create a unique identifier
        string uniqueId = System.Guid.NewGuid().ToString();

        // Capture a screenshot
        string screenshotFileName = $"screenshot_{uniqueId}.png";
        string screenshotPath = Path.Combine(saveDirectory, screenshotFileName);
        StartCoroutine(CaptureAndSaveScreenshot(screenshotPath));

        // Create save data with a reference to the screenshot
        switch (SceneStateManager.Instance.currentState)
        {
            case SceneState.MainMenu:
                break;
            case SceneState.Dress:
                data = new ClothData
                {
                    currentSceneState = SceneStateManager.Instance.currentState,
                    currentDressIndex = this.currentDress,
                    dressColor = this._color,
                    dressName = this._dressName,
                    height = this.height,
                    bust = this.bust,
                    hips = this.hips,
                    waist = this.waist,
                    price = this.price,
                    screenshotFileName = screenshotFileName
                };
                break;
            case SceneState.Necklace:
                
                foreach (var obj in necklaceItemList)
                {
                    NecklaceItem item = new NecklaceItem
                    {
                        prefabName = obj.transform.name,
                        position = obj.transform.position,
                        rotation = obj.transform.rotation
                    };
                    necklaceItemList1.Add(item);
                }
                data = new NecklaceData
                {
                    currentSceneState = SceneStateManager.Instance.currentState,
                    necklaceItemList = this.necklaceItemList1,
                    locktype = this.locktype,
                    length = this.length,
                    totalPrice = this.totalPrice,
                    necklaceItemListDet = this.necklaceItemListDet,
                    screenshotFileName = screenshotFileName
                };
                break;
            case SceneState.Ring:
                data = new RingData
                {
                    currentSceneState = SceneStateManager.Instance.currentState,
                    ringType = this.ringType,
                    ringStone = this.ringStone,
                    ringSize = this.ringSize,
                    ringTotalPrice = this.ringTotalPrice,
                    ringColor = this.ringColor,
                    ringTypePrice = this.ringTypePrice,
                    ringStonePrice = this.ringStonePrice,
                    screenshotFileName = screenshotFileName
                };
                break;
            default:
                break;
        }
        
        // Convert the save data to JSON
        string jsonData = JsonUtility.ToJson(data, true);

        // Save JSON data to file
        string jsonFileName = $"save_{uniqueId}.json";
        string jsonFilePath = Path.Combine(saveDirectory, jsonFileName);

        File.WriteAllText(jsonFilePath, jsonData);

        Debug.Log($"Game data saved: {jsonData}");
    }

    private IEnumerator CaptureAndSaveScreenshot(string path)
    {
        yield return new WaitForEndOfFrame();

        // Capture the screen into a Texture2D
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();

        // Encode texture to PNG
        byte[] imageBytes = screenTexture.EncodeToPNG();

        // Save the PNG file
        File.WriteAllBytes(path, imageBytes);

        Debug.Log($"Screenshot saved to: {path}");

        // Clean up
        Destroy(screenTexture);
    }

    public void LoadAllScreenshots()
    {
        // Ensure the directory exists
        if (!Directory.Exists(saveDirectory))
        {
            Debug.LogError($"Directory not found: {saveDirectory}");
            return;
        }

        // Get all image files (assuming .jpg for this example)
        string[] screenshotFiles = Directory.GetFiles(saveDirectory, "screenshot_*.png");
        contentParentObject = GameObject.Find("ImagesContainer");
        
        foreach (string filePath in screenshotFiles)
        {
            // Load the image from file
            byte[] imageData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageData))
            {
                // Instantiate the prefab and set it up
                GameObject screenshotInstance = Instantiate(screenshotPrefab, contentParentObject.transform);

                // Access the second child and its first child
                Transform secondChild = screenshotInstance.transform.GetChild(1); // Get the second child
                Transform desiredChild = secondChild.GetChild(0); // Get the child of the second child
                                                                  // Assuming the button is a direct child of the prefab
                Button button = screenshotInstance.GetComponentInChildren<Button>();
                if (button != null)
                {
                    // Get the file name without the extension
                    string screenshotFileName = Path.GetFileNameWithoutExtension(filePath); // "screenshot_85ffd178-14a1-4186-8707-9ec4e1ed9b59"

                    // Split by underscore and get the last part, which is the UUID
                    string uuid = screenshotFileName.Split('_')[1]; // "85ffd178-14a1-4186-8707-9ec4e1ed9b59"

                    // Assign the function you want to call when the button is clicked
                    button.onClick.AddListener(() => OnScreenshotButtonClick(uuid));
                }
                // Set up the Image component
                Image imageComponent = desiredChild.GetComponent<Image>();
                if (imageComponent != null)
                {
                    // Convert Texture2D to Sprite and assign to Image
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    Sprite screenshotSprite = Sprite.Create(texture, rect, pivot);
                    imageComponent.sprite = screenshotSprite;

                    Debug.Log($"Assigned image to child: {desiredChild.name}");
                }
                else
                {
                    Debug.LogWarning($"No Image component found on {desiredChild.name}");
                }
            }
            else
            {
                Debug.LogWarning($"Failed to load image from {filePath}");
            }
        }
    }

    public void LoadGameData(string uuid)
    {
        // Assuming the directory exists and is set
        if (!Directory.Exists(saveDirectory))
        {
            Debug.LogError($"Save directory not found: {saveDirectory}");
            return;
        }

        // Get the most recent save file
        string[] jsonFiles = Directory.GetFiles(saveDirectory, "save_" + uuid + ".json");
        if (jsonFiles.Length == 0)
        {
            Debug.LogWarning("No save files found.");
            return;
        }

        // Load the most recent save file (or you can implement a way to choose a specific one)
        string jsonFilePath = jsonFiles[0];
        // Read the JSON data
        string jsonData = File.ReadAllText(jsonFilePath);
        Debug.Log($"Loading game data from: {jsonFilePath}");

        // Deserialize the JSON to a dynamic object to figure out which type it corresponds to
        GameData data = JsonUtility.FromJson<GameData>(jsonData);
 
        switch ((SceneState)data.currentSceneState)
        {
            case SceneState.MainMenu:
                // Handle loading for the main menu if necessary
                break;
            case SceneState.Dress:
                data = JsonUtility.FromJson<ClothData>(jsonData);
                LoadDressData((ClothData)data);
                SceneManager.LoadScene("ClothesStudioScene");
                SceneStateManager.Instance.SetState(SceneState.Dress);
                break;
            case SceneState.Ring:
                data = JsonUtility.FromJson<RingData>(jsonData);
                LoadRingData((RingData)data);
                SceneManager.LoadScene("RingStudioScene");
                SceneStateManager.Instance.SetState(SceneState.Ring);
                break;
            case SceneState.Necklace:
                data = JsonUtility.FromJson<NecklaceData>(jsonData);
                LoadNecklaceData((NecklaceData)data);
                SceneManager.LoadScene("NecklaceStudioScene");
                SceneStateManager.Instance.SetState(SceneState.Necklace);
                break;
            default:
                Debug.LogWarning("Unknown scene state.");
                break;
        }
    }

    // Example function to be called on button click
    public void OnScreenshotButtonClick(string uuid)
    {
        LoadGameData(uuid);
        Debug.Log("Screenshot button clicked!" + uuid);
    }

    // Example methods to load specific data types
    private void LoadDressData(ClothData clothData)
    {
        this.currentDress = clothData.currentDressIndex;
        this._color = clothData.dressColor;
        this._dressName = clothData.dressName;
        this.height = clothData.height;
        this.bust = clothData.bust;
        this.hips = clothData.hips;
        this.waist = clothData.waist;
        this.price = clothData.price;
    }

    private void LoadNecklaceData(NecklaceData necklaceData)
    {
        //this.necklaceItemList = necklaceData.necklaceItemList;
        this.locktype = necklaceData.locktype;
        this.length = necklaceData.length;
        this.totalPrice = necklaceData.totalPrice;
        this.necklaceItemListDet = necklaceData.necklaceItemListDet;
    }

    private void LoadRingData(RingData ringData)
    {
        this.ringType = ringData.ringType;
        this.ringStone = ringData.ringStone;
        this.ringSize = ringData.ringSize;
        this.ringTotalPrice = ringData.ringTotalPrice;
        this.ringColor = ringData.ringColor;
        this.ringTypePrice = ringData.ringTypePrice;
        this.ringStonePrice = ringData.ringStonePrice;
    }
}

public enum ColorEnum
{
    Red,
    Green,
    Yellow,
    Blue,
    White,
    Black
}

public enum SceneState
{
    MainMenu,
    Dress,
    Ring,
    Necklace
}

public enum RingColor
{
    Silver,
    Gold,
    RoseGold
}

[System.Serializable]
public class GameData
{
    public SceneState currentSceneState;
}

[System.Serializable]
public class ClothData : GameData
{
    public int currentDressIndex;
    public ColorEnum dressColor;
    public string dressName;
    public float height;
    public float bust;
    public float hips;
    public float waist;
    public float price;
    public string screenshotFileName;
}

[System.Serializable]
public class NecklaceData : GameData
{
    public List<NecklaceItem> necklaceItemList = new List<NecklaceItem>();
    public string locktype;
    public int length;
    public float totalPrice;
    public string necklaceItemListDet;
    public string screenshotFileName;
}

[System.Serializable]
public class RingData : GameData
{
    public string ringType;
    public string ringStone;
    public string ringSize;
    public string ringTotalPrice;
    public RingColor ringColor;
    public int ringTypePrice;
    public int ringStonePrice;
    public string screenshotFileName;
}

[System.Serializable]
public class NecklaceItem
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
}