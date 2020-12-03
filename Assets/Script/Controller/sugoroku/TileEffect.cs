using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private float initalWaiTime;

    private LimitTimeCounter statusTime;

    private Status status;

    private SpriteRenderer renderFront;
    private SpriteRenderer renderBack;

    private enum Status
    {
        InitalWait ,
        Wait ,
        Jump ,
    }

    private void StatusChange( Status status )
    {
        this.status = status;

        switch( status )
        {
            case Status.InitalWait:
                this.transform.localPosition = new Vector3(0, 0f, 0);
                this.statusTime.Start(this.initalWaiTime);
                break;
            case Status.Wait:
                this.transform.localPosition = new Vector3(0, 0f, 0);
                this.statusTime.Start(5f);
                break;
            case Status.Jump:
                this.statusTime.Start(0.3f);
                break;
        }
    }

    private void StatusUpdate()
    {
        this.statusTime.Update();

        switch (status)
        {
            case Status.InitalWait:

                if (this.statusTime.IsFinished)
                {
                    this.StatusChange(Status.Jump);
                }

                break;
            case Status.Wait:

                if (this.statusTime.IsFinished)
                {
                    this.StatusChange(Status.Jump);
                }

                break;
            case Status.Jump:

                if (this.statusTime.IsFinished)
                {
                    this.transform.localPosition = new Vector3(0, 0f, 0);
                    this.StatusChange(Status.Wait);
                }
                else
                    this.transform.localPosition = new Vector3(0, 1f * this.statusTime.Ratio, 0);

                break;
        }

    }

    public void Initalize( int index )
    {
        //this.initalWaiTime = (float)(index%31) * 0.1f;
        this.initalWaiTime = index * 0.1f;
    }


    void Start()
    {
        this.statusTime = new LimitTimeCounter();
        this.StatusChange(Status.InitalWait);

    }

    // Update is called once per frame
    void Update()
    {
        this.StatusUpdate();
    }

    public void SetSortingOrder(int orderFront , int orderBack)
    {
        this.renderFront = this.transform.Find("Front").GetComponent<SpriteRenderer>();
        this.renderBack = this.transform.Find("Back").GetComponent<SpriteRenderer>();

        this.renderFront.sortingOrder = orderFront;
        this.renderBack.sortingOrder = orderBack;
    }
}
