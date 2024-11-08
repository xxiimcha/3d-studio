using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NecklaceStudioManager : MonoBehaviour
{
    public GameObject necklaceDetailsMenu;
    public GameObject necklaceDetailsMenuLength;
    public Button saveBtn;
    public Button toMainBtn;
    public Button nextBtn;
    public Button uinextBtn;
    public Button backBtn;
    public Button cancelBtn;
    public Button necklaceBtn;
    public Button ringBtn;

    // Reference to the spawn point (Transform component)
    public Transform spawnPoint;

    //ui buttons
    public Button shookBtn;
    public Button lobsterClawBtn;
    public TMP_Text totalPrice;
    public TMP_Text necklaceItemListText;

    // Start is called before the first frame update
    void Start()
    {
        // Add listener to call LoadMainMenu on button click
        toMainBtn.onClick.AddListener(() => GameManager.Instance.OnToMainButtonClicked());
        saveBtn.onClick.AddListener(() => GameManager.Instance.OnSaveButtonClicked());
        nextBtn.onClick.AddListener(() => GameManager.Instance.OnNextButtonClicked(necklaceDetailsMenu, necklaceDetailsMenuLength));
        uinextBtn.onClick.AddListener(() => GameManager.Instance.OnNextButtonClicked(necklaceDetailsMenu, necklaceDetailsMenuLength));
        backBtn.onClick.AddListener(() => GameManager.Instance.OnBackButtonClicked(necklaceDetailsMenu, necklaceDetailsMenuLength));
        cancelBtn.onClick.AddListener(() => GameManager.Instance.OnBackButtonClicked(necklaceDetailsMenu, necklaceDetailsMenuLength));
        necklaceBtn.onClick.AddListener(() => GameManager.Instance.OnNecklaceButtonClicked());
        ringBtn.onClick.AddListener(() => GameManager.Instance.OnRingButtonClicked());

        shookBtn.onClick.AddListener(() => SetLocktype("S-Hook"));
        lobsterClawBtn.onClick.AddListener(() => SetLocktype("Lobster Claw"));

        GameManager.Instance.OnTotalPriceChanged += UpdateTotalPrice;
        GameManager.Instance.OnNecklaceItemListChanged += UpdateNecklaceItemList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLocktype(string ltype)
    {
        GameManager.Instance.locktype = ltype;
    }

    public void SetLength(int length)
    {
        GameManager.Instance.length = length;
    }


    // Methods to update UI elements when GameManager variables change
    private void UpdateTotalPrice(float value)
    {
        if (totalPrice.text != value.ToString())
        {
            totalPrice.text = "Total Price" + value.ToString();
        }
    }

    private void UpdateNecklaceItemList(string value)
    {
        if (necklaceItemListText.text != value)
        {
            necklaceItemListText.text = value;
        }
    }

    public void SpawnAccBtn(GameObject obj)
    {
        // Check if the prefab and spawn point are set
        if (obj != null && spawnPoint != null)
        {
            // Instantiate the prefab at the spawn point's position and rotation
            Instantiate(obj, spawnPoint.position, spawnPoint.rotation);
            GameManager.Instance.necklaceItemList.Add(obj);
        }
        else
        {
            Debug.LogWarning("Prefab or spawn point is not assigned!");
        }
    }
}
