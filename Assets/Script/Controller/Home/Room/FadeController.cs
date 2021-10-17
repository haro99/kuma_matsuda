using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private SpriteRenderer sprite;


    private enum Status
    {
        FadeIn ,
        None ,
        FadeOut,
        BlackOut ,
    }
    private Status status;
    public bool IsBlackOut { get { return this.status == Status.BlackOut; } }
    private LimitTimeCounter statusTime;

    private void Awake()
    {
        this.statusTime = new LimitTimeCounter();
    }
    // Use this for initialization
    void Start ()
    {
        this.sprite = this.GetComponent<SpriteRenderer>();
        this.sprite.enabled = true;
        this.statusTime = new LimitTimeCounter();

        this.SetStatus(Status.BlackOut);
        Debug.Log(status);
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.statusTime.Update();
        switch (this.status)
        {
            case Status.FadeIn:

                this.SetBlackStrong(this.statusTime.Ratio);

                if( this.statusTime.IsFinished )
                {
                    this.SetStatus( Status.None);
                }

                break;
            case Status.FadeOut:
                this.SetBlackStrong(this.statusTime.RatioReverse);
                if (this.statusTime.IsFinished)
                {
                    this.SetStatus(Status.BlackOut);
                }
                break;
        }
    }

    private void SetBlackStrong( float ratio )
    {
        this.sprite.color = new Color( 1f , 1f , 1f , ratio );
    }


    private void SetStatus( Status status )
    {
        this.status = status;

        switch( this.status )
        {
            case Status.FadeIn:
                this.statusTime.Start(1f);
                break;
            case Status.FadeOut:
                this.statusTime.Start(1f);
                break;

            case Status.None:
                this.SetBlackStrong(0f);
                break;
            case Status.BlackOut:
                this.SetBlackStrong(1f);
                break;
        }
    }

    public void FadeIn()
    {
        this.SetStatus(Status.FadeIn);
    }

    public void FadeOut()
    {
        this.SetStatus(Status.FadeOut);
    }

}
