using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMenuController : MonoBehaviour
{

    private List<CardIcon> cardIcons;

    public GameObject CardDetailPrinter;
    private CardDetailPrinter cardDetailPrinter;

    [SerializeField] Toggle m_Toggle;


    private void Awake()
    {
        this.cardDetailPrinter = this.CardDetailPrinter.GetComponent<CardDetailPrinter>();
    }

    // Use this for initialization
    void Start ()
    {
        // カード確認
        CardData cardData = SaveData.GetClass<CardData>("card", new CardData());
        foreach (var i in cardData.CardNoSet)
        {
            //Debug.Log("cardNo : " + i);

            GameObject card = gameObject.transform.Find("Card" + i).gameObject;


            CardIcon icon = card.GetComponent<CardIcon>();
            icon.Initalize(i,this);

            //Sprite spriteCardImage = Resources.Load("images/room/card/" + i, typeof(Sprite)) as Sprite;
            //card.GetComponent<Image>().sprite = spriteCardImage;
        }	
    }

    public void ShowDetail( int cardID )
    {
        this.gameObject.SetActive(false);
        HomeDirector.GetInstance().SoundPlaySelect();
        this.cardDetailPrinter.SetCardID(cardID);
    }

    //==========================================================================
    // メニューを閉じる
    //==========================================================================
    public void CloseCardMenu()
    {
        m_Toggle.isOn = false;
    }
}
