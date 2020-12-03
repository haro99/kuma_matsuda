using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDetailPrinter : MonoBehaviour , ICloseButtonListener
{
    private Image cardImage;
    private CardCostPrinter cardCostPrinter;

    private CardData cardData;

    private CloseButton closeButton;

    private void Awake()
    {
        this.cardImage = this.transform.Find("CardDetail").gameObject.GetComponent<Image>();
        this.cardCostPrinter = this.transform.Find("CardCostCount").gameObject.GetComponent<CardCostPrinter>();

        this.cardData = SaveData.GetClass<CardData>("card", new CardData());

        this.closeButton = this.transform.Find("CloseButton").gameObject.GetComponent<CloseButton>();
        this.closeButton.SetCloseListener(this);
        
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetCardID( int cardID )
    {
        this.gameObject.SetActive(true);
        this.cardImage.sprite = Resources.Load("images/card/" + cardID, typeof(Sprite)) as Sprite;

        this.cardCostPrinter.SetNumber(this.cardData.GetCardCost(cardID));

    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
