using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClothesDetailsMenuColor : MonoBehaviour
{
    public TMP_Text dessNameTxt;
    public TMP_Text hipsTxt;
    public TMP_Text waistTxt;
    public TMP_Text bustTxt;
    public TMP_Text heightTxt;
    public TMP_Text colorTxt;
    public TMP_Text totalPriceTxt;
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
        this.dessNameTxt.text = "";
        this.hipsTxt.text = "";
        this.waistTxt.text = "";
        this.bustTxt.text = "";
        this.heightTxt.text = "";
        this.totalPriceTxt.text = "";
        this.colorTxt.text = "";

        this.dessNameTxt.text = "Dress Name: " + " " + GameManager.Instance.DressName;
        this.hipsTxt.text = "Hips: " + " " + GameManager.Instance.Hips;
        this.waistTxt.text = "Waist: " + " " + GameManager.Instance.Waist;
        this.bustTxt.text = "Bust: " + " " + GameManager.Instance.Bust;
        this.heightTxt.text = "Heigth: " + " " + GameManager.Instance.Height;
        this.totalPriceTxt.text = "Total Price: " + " " + GameManager.Instance.Price;
        this.colorTxt.text = "Color: " + " " + GameManager.Instance.Color;
    }

    public void DressColorChange(int color)
    {
        GameManager.Instance.Color = (ColorEnum)color;
        this.colorTxt.text = "Color: ";
        this.colorTxt.text = this.colorTxt.text + " " + GameManager.Instance.Color;
    }
}
