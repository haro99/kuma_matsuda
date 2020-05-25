using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconEffecter : MonoBehaviour
{
    private RectTransform rectTransform;

    public int PopLevel;
    private int popCount;

    public enum StatusType
    {
        Active ,
        Pop,
        PopReturn
    }

    public StatusType status;

    private LimitTimeCounter statusTime;

    public void Pop()
    {
        this.PopLevel += 3;

        if (this.PopLevel > 10)
            this.PopLevel = 10;

        this.popCount = 0;
        this.StatusChange(StatusType.Pop);
    }

    private void StatusChange( StatusType status )
    {
        this.status = status;

        switch (this.status)
        {
            case StatusType.Active:
                this.rectTransform.localScale = new Vector3(1f, 1f, 1f);
                break;

            case StatusType.Pop:
                //this.statusTime.Start(0.1f + 0.05f * (float)popCount);
                this.statusTime.Start(0.07f);
                break;
            case StatusType.PopReturn:
                this.statusTime.Start(0.07f);
                break;
        }

    }


    private void StatusUpdate()
    {
        switch( this.status )
        {
            case StatusType.Active:
                break;

            case StatusType.Pop:

                this.statusTime.Update();

                if (this.statusTime.IsFinished)
                {
                    this.PopLevel--;
                    this.popCount++;

                    if (this.popCount >= 5)
                        this.popCount = 5;

                    this.StatusChange(StatusType.PopReturn);
                }
                else
                {
                    float scale = 0.98f + ( 0.02f + 0.1f * (float)Mathf.Min(this.PopLevel,5) )* this.statusTime.Ratio;
                    this.rectTransform.localScale = new Vector3(scale, scale, 1f);
                }

                break;
            case StatusType.PopReturn:

                this.statusTime.Update();

                if (this.statusTime.IsFinished)
                {
                    if( this.PopLevel <= 0 )
                        this.StatusChange(StatusType.Active);
                    else
                        this.StatusChange(StatusType.Pop);
                }
                else
                {
                    float scale = 0.98f + (0.02f + 0.1f * (float)Mathf.Min(this.PopLevel, 5)) * this.statusTime.RatioReverse;
                    this.rectTransform.localScale = new Vector3(scale, scale, 1f);
                }

                break;
        }
    }

        
    // Start is called before the first frame update
    void Start()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.statusTime = new LimitTimeCounter();
        this.StatusChange(StatusType.Active);
    }

    // Update is called once per frame
    void Update()
    {
        this.StatusUpdate();
    }
}
