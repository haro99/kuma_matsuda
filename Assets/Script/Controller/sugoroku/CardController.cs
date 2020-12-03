using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class CardController : MonoBehaviour
public class CardController : MovablObject
{

    float t = 0;
    float total = 0;
    int x = 0;

    bool finishFlg = false;

    private int cardID;

    // Use this for initialization
    void Start ()
    {
        // 1秒かけて、y軸を90度回転
        //iTween.RotateTo(gameObject, iTween.Hash("y", 90, "time", 1f));
        // 5秒かけて、xy軸を拡大
        //iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1, "time", 5.0f));
        //this.ChangeStatus(Status.None);
    }


    private enum Status
    {
        None ,
        RollIn ,
        RollOut ,
        ZoomHop,
        ZoomFit,
        Finish ,
    }

    private Status status;
    private LimitTimeCounter statusTime;
    private int lastRollCount;


    private void ChangeStatus( Status status )
    {
        this.status = status;
        switch( status )
        {
            case Status.None:
                this.SetScale(0f, 0f);
                break;
            case Status.RollIn:
                this.SetScale(1f, 1f);
                this.SetRotate(0f, 90f, 0f);
                this.statusTime.Start(0.05f + (float)lastRollCount * 0.07f);
                break;
            case Status.RollOut:
                this.SetScale(1f, 1f);
                this.SetRotate(0f, 0f, 0f);
                this.statusTime.Start(0.05f + (float)lastRollCount * 0.07f);
                break;
            case Status.ZoomHop:
                this.ShowNewCard();

                this.SetScale(1f, 1f);
                this.statusTime.Start(0.1f);

                SugorokuDirector.GetInstance().SoundPlayCardGet();
                break;
            case Status.ZoomFit:
                this.SetScale(2.5f, 2.5f);
                this.statusTime.Start(0.3f);
                break;
            case Status.Finish:
                this.ShowNewCard();
                this.SetScale(2f, 2f);
                this.SetRotate(0f, 0f, 0f);
                break;
        }
    }

    private void ShowNewCard()
    {
        Sprite cardImage = Resources.Load("images/card/" + this.cardID, typeof(Sprite)) as Sprite;
        this.GetComponent<Image>().sprite = cardImage;

    }

    private void UpdateStatus()
    {
        this.statusTime.Update();

        switch (status)
        {
            case Status.None:
                break;
            case Status.RollIn:

                this.SetRotate(0f,90f * this.statusTime.Ratio, 0f);

                if( this.statusTime.IsFinished )
                {

                    if (this.lastRollCount <= 0)
                        this.ChangeStatus(Status.ZoomHop);
                    else
                    {
                        this.lastRollCount--;
                        this.ChangeStatus(Status.RollOut);
                    }
                }

                break;
            case Status.RollOut:
                this.SetRotate(0f,90f * this.statusTime.RatioReverse, 0f);

                if (this.statusTime.IsFinished)
                {
                    this.ChangeStatus(Status.RollIn);
                }
                break;
            case Status.ZoomHop:
                this.SetScale(1f + 1.5f * this.statusTime.RatioReverse, 1f + 1.5f * this.statusTime.RatioReverse);
                if (this.statusTime.IsFinished)
                {
                    this.ChangeStatus(Status.ZoomFit);
                }
                break;
            case Status.ZoomFit:
                this.SetScale(2f + 0.5f * this.statusTime.Ratio, 2f + 0.5f * this.statusTime.Ratio);
                if (this.statusTime.IsFinished)
                {
                    this.ChangeStatus(Status.Finish);
                }
                break;
            case Status.Finish:
                break;
        }

    }


    public void Open( int newCardID )
    {
        this.cardID = newCardID;
        this.t = 0;
        this.total = 0;
        this.finishFlg = false;

        this.lastRollCount = 5;

        Sprite cardImage = Resources.Load("images/card/card_back", typeof(Sprite)) as Sprite;
        this.GetComponent<Image>().sprite = cardImage;

        this.statusTime = new LimitTimeCounter();

        this.ChangeStatus(Status.RollOut);


    }

    // Update is called once per frame
    void Update ()
    {
        this.UpdateStatus();
        /*
        if (IsFinish())
        {
            //nothing
        }
        else if(total > 5)
        {
            Sprite cardImage = Resources.Load("images/card/" + this.cardID, typeof(Sprite)) as Sprite;
            this.GetComponent<Image>().sprite = cardImage;

            // 4秒かけて、xy軸を2倍に拡大
            iTween.ScaleTo(gameObject, iTween.Hash("x", 2, "y", 2, "time", 4.0f, "oncomplete", "finish", "oncompletetarget", gameObject));
        }
        else if (t < 0.5)
        {
            t += Time.deltaTime;
        }
        else
        {
            x += 90;
            // 1秒かけて、y軸を180度回転
            iTween.RotateTo(gameObject, iTween.Hash("y", x % 180, "time", 1f));

            total += t;
            t = 0;
        }
        */
    }

    public bool IsFinish()
    {
        //return finishFlg;

        return this.status == Status.Finish || this.status == Status.None;
    }


    public void Finish()
    {
        //finishFlg = true;
        this.ChangeStatus(Status.Finish);
    }
}
