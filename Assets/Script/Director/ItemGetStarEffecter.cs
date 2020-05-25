using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetStarEffecter : SpriteObject
{

    //public int PopLevel;

    //private float baseY;
    //private float baseScaleX, baseScaleY;
    private float baseScale;
    private float rotateSpeed;

    private float popY , popX;


    public enum StatusType
    {
        Pop,
        PopReturn ,
        Finish
    }

    public StatusType status;

    private LimitTimeCounter statusTime;
    //private LimitTimeCounter colorChangeTime;
    //private LimitTimeCounter scaleChangeTime;


    public void Initalize(ItemController itemController )
    {
        base.Start();

        this.rotateSpeed = Random.Range(-0.1f, 0.1f);
        this.Rotate = Random.Range(-6f, 6f);

        // this.baseScaleX = this.transform.localScale.x;
        // this.baseScaleY = this.transform.localScale.y;
        this.baseScale = Random.Range(0.3f, 1f);

        this.popX = Random.Range(-2f, 2f);
        this.popY = Random.Range(2f, 4f);

        //this.colorChangeTime = new LimitTimeCounter();
        //this.scaleChangeTime = new LimitTimeCounter();

        this.statusTime = new LimitTimeCounter();
        //this.StatusChange(StatusType.Pop);
        this.statusTime.Start(3f);

        this.SortingOrder = itemController.ImageObject.GetComponent<SpriteRenderer>().sortingOrder;
        this.SetPosition(itemController.ImageObject.transform.position);

        /*
        switch( itemController.GetItem() )
        {
            case Const.Item.egg:
                this.SetColor(1f, 1f, 0f,1f);
                break;
            case Const.Item.fish:
                this.SetColor(0.9f, 0.9f, 1f,1f);
                break;
            case Const.Item.meat:
                this.SetColor(1f, 0.5f, 0.5f, 1f);
                break;
            case Const.Item.vegetables:
                this.SetColor(0.5f, 1f, 0.5f, 1f);
                break;
        }
        */
        this.SetColor(1f, 1f, 0.5f, 1f);

        //this.baseY = itemController.ImageObject.transform.position.y;

    }

    /*
    private void StatusChange(StatusType status)
    {
        this.status = status;

        switch (this.status)
        {
            case StatusType.Pop:
                this.statusTime.Start(1f + (float)Random.Range(0.01f, 0.05f));

                this.colorChangeTime.Start(0.35f);
                this.scaleChangeTime.Start(0.5f);

                break;
            case StatusType.PopReturn:
                this.statusTime.Start(0.05f + (float)Random.Range(0.01f, 0.05f));
                break;
            case StatusType.Finish:
                this.Destroy();
                break;
        }

    }


    private void StatusUpdate()
    {


        switch (this.status)
        {
            case StatusType.Pop:

                this.statusTime.Update();

                if (this.statusTime.IsFinished)
                {
                    this.StatusChange(StatusType.PopReturn);
                }
                else
                {
                    //float y = this.Y+ 3f * Time.deltaTime;
                    //this.SetPosition(this.X, y);
                }

                break;
            case StatusType.PopReturn:

                this.statusTime.Update();

                if (this.statusTime.IsFinished)
                {
                        this.StatusChange(StatusType.Finish);
                }
                else
                {
                    //float y = this.Y - 2f * Time.deltaTime;
                    //this.SetPosition(this.X, y);
                    this.SetColor(this.Color.r, this.Color.g, this.Color.b, this.statusTime.Ratio);
                }

                break;
        }
    }
    */

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        /*

        this.rotateSpeed = Random.Range(-1f, 1f);
        this.Rotate = Random.Range(-6f, 6f);

        this.baseScaleX = this.transform.localScale.x;
        this.baseScaleY = this.transform.localScale.y;

        this.baseY = this.Y;

        this.colorChangeTime = new LimitTimeCounter();
        this.scaleChangeTime = new LimitTimeCounter();

        this.statusTime = new LimitTimeCounter();
        this.StatusChange(StatusType.Pop);
        */
    }

    // Update is called once per frame
    void Update()
    {
        //this.StatusUpdate();

        /*
        if (!this.colorChangeTime.IsFinished)
        {
            this.colorChangeTime.Update();

            this.SetColor(this.Color.r, this.Color.g, this.Color.b, this.colorChangeTime.Ratio);
        }
        */

        this.statusTime.Update();
        if (this.statusTime.IsFinished)
            this.Destroy();


        float x = this.X + this.popX * Time.deltaTime;
        float y = this.Y + this.popY * Time.deltaTime;
        this.SetPosition(x, y);

        this.popY -= 5 * Time.deltaTime;
        this.popX *= 1f - 0.3f * Time.deltaTime;


        if (!this.statusTime.IsFinished)
        {
            this.statusTime.Update();

            this.SetScale( this.baseScale * this.statusTime.Ratio);
        }

        //this.SetColor(this.Color.r, this.Color.g, this.Color.b, this.statusTime.Ratio);

        this.rotateSpeed *= 1f - (0.3f * Time.deltaTime);
        this.Rotate += this.rotateSpeed;
    }
}
