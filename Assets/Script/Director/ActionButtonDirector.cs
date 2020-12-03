using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionButtonDirector : MonoBehaviour {

	public GameObject diceObject;
	DiceController diceController;

	public GameObject sugorokuDirectorObject;
	SugorokuDirector sugorokuDirector;

	public GameObject startMessage;
    public GameObject star;

    public GameObject card;
    CardController cardController;

    public GameObject playerObject;

    private List<int> showCardIDs;
    private int showCardIndex;

    private GameObject CutInCanvas;

    private CutInController cutInController;

    void Start ()
    {
		sugorokuDirector = sugorokuDirectorObject.GetComponent<SugorokuDirector> ();
		diceController = diceObject.GetComponent<DiceController> ();
        cardController = card.GetComponent<CardController>();

        this.CutInCanvas = GameObject.Find("CanvasCutIn");
        this.cutInController = this.CutInCanvas.GetComponent<CutInController>();

        this.showCardIDs = new List<int>();
        
    }

    public void StageClear()
    {
        this.showCardIDs.Clear();

        ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());
        CardData cardData = SaveData.GetClass<CardData>("card", new CardData());

        int totalStar = clearStar.GetTotalStarCount();
        int newestCardId = cardData.GetNewestCardID(totalStar);


        for (int checkCardID = 1; checkCardID <= newestCardId; checkCardID++)
        {
            if (!cardData.CardNoSet.Contains(checkCardID))
            {
                this.showCardIDs.Add(checkCardID);

                cardData.CardNoSet.Add(checkCardID);


    
            }
        }
    

        this.showCardIndex = 0;

        SaveData.SetClass<CardData>("card", cardData);
        SaveData.Save();

        //
    }


    private enum CardGetStatus
    {
        None ,
        CutInAnimation , 
        RollAnimation ,
    }

    private CardGetStatus cardGetStatus;

    public void Update()
    {
        switch( this.cardGetStatus )
        {
            case CardGetStatus.CutInAnimation:
                if (this.cutInController.IsFinished)
                {
                    this.cardGetStatus = CardGetStatus.RollAnimation;

                    int showCardID = this.showCardIDs[this.showCardIndex];
                    cardController.Open(showCardID);

                    star.SetActive(false);
                    card.SetActive(true);

                }
                break;

            case CardGetStatus.RollAnimation:

                if (cardController.IsFinish())
                {
                    this.showCardIndex++;
                    cardController.Finish();

                    if (this.showCardIDs.Count > this.showCardIndex)
                        this.cardGetStatus = CardGetStatus.CutInAnimation;
                }
                break;
        }
    }

    public void onClick()
    {
        //Debug.Log("onClick: start");
        if (sugorokuDirector.isWait())
        {
            // startMessageを削除
            startMessage.SetActive(false);
            sugorokuDirector.ChangeWaitDiceClick();
        }
        else if( sugorokuDirector.isSpecialSelecting() )
        {
            sugorokuDirector.SpecialSelectSkipRequest();
        }

        if (sugorokuDirector.isClear())
        {

            // GETカード
            //CardData cardData = SaveData.GetClass<CardData>("card", new CardData());


            if (cardController.IsFinish())
            {
                if (this.showCardIDs.Count <= this.showCardIndex)
                {
                    SceneManager.LoadScene("HomeScene");
                    return;
                }
            }

            if (this.showCardIDs.Count > this.showCardIndex)
            {

                if( this.cardGetStatus == CardGetStatus.None )
                {
                    this.cutInController.Open();
                    this.cardGetStatus = CardGetStatus.CutInAnimation;
                }
                else if (this.cardGetStatus == CardGetStatus.CutInAnimation)
                {
                    this.cutInController.Close();
                }
                else if (this.cardGetStatus == CardGetStatus.RollAnimation )
                {
                    this.cardController.Finish();
                }

                /*
                if( !cardController.IsFinish())
                {
                    cardController.Finish();

                }
                else
                {
                    
                    int showCardID = this.showCardIDs[this.showCardIndex];

                    star.SetActive(false);
                    card.SetActive(true);
                    cardController.Open(showCardID);
                    this.showCardIndex++;
                }
                */
            }
            else
            {
                cardController.Finish();
            }
        }

        if (sugorokuDirector.isGameOver())
        {
            SceneManager.LoadScene("HomeScene");
        }

        if (sugorokuDirector.isPlayerMove())
        {
            // ジャンプ
            // playerObject.GetComponent<PlayerController>().ReserveJump();
        }
    }

    //==========================================================================
    //
    //==========================================================================
    public void stopDice()
    {
        //Debug.Log("stopDice: start");
        if (sugorokuDirector.isWaitDiceClick())
        {
			if (diceController.isRotation())
            {
                Debug.Log("stopDice: stopDice.");
                diceController.StopDice ();
                gameObject.GetComponent<ActionButtonController>().StopCount();

                SugorokuDirector.GetInstance().SoundPlaySaiStop();
			}
		}
	}
}
