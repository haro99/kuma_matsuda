using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountPrinter : MonoBehaviour
{
    private Text amountText;
    private Image image;

	// Use this for initialization
	void Awake()
    {
        this.image = this.GetComponent<Image>();
        this.amountText = this.transform.Find("amount").GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public int Value
    {
        set
        {
            this.amountText.text = value.ToString();
        }
    }

    public Sprite Image
    {
        set
        {
            this.image.sprite = value;
        }
    }
}
