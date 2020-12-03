using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingItem : SpriteObject
{
    
    private enum StatusType
    {
        PopUpWait ,
        PopUp ,
        FlyWait ,
        Fly ,
        Finish ,
    }

    private StatusType status;
    private LimitTimeCounter statusTime;

    private float popXFrom, popYFrom;
    public float popJumpX, popJumpY , popGravityY;

    public float popScale, popRotateSpeed;

    private int index;

    private ItemCountUiPrinter targetUI;

    public void Initalize( int index , ItemController itemController , ItemCountUiPrinter targetUI )
    {
        this.index = index;

        this.targetUI = targetUI;

        Vector3 position = itemController.transform.position;

        this.transform.position = position;
        this.statusTime = new LimitTimeCounter();

        this.popXFrom = position.x;
        this.popYFrom = position.y;

        this.popRotateSpeed = Random.Range( -10f , 10f );
        this.popScale = 1f;

        this.render = this.GetComponent<SpriteRenderer>();
        this.SortingOrder = itemController.ImageObject.GetComponent<SpriteRenderer>().sortingOrder;

        string itemName = itemController.GetItem().ToString();
        string itemFilepath = "images/sugoroku/item/item_" + itemName;
        this.Sprite = Resources.Load(itemFilepath, typeof(Sprite)) as Sprite;



        SugorokuDirector.GetInstance().MapController.GetOrderInLayerForTile(itemController.TileIndexX, itemController.TileIndexY, MapController.MapObjectOrderIndex.TileEffect_Front);
        this.StatusChange(StatusType.PopUpWait);



    }

    private void StatusChange( StatusType status )
    {
        this.status = status;

        switch (this.status)
        {
            case StatusType.PopUpWait:
                this.statusTime.Start( 0.01f + index * 0.1f );
                break;

            case StatusType.PopUp:
                this.popJumpX = Random.Range(-5f, 5f);
                this.popJumpY = Random.Range(8f, 12f);
                this.popGravityY = 2f;

                this.statusTime.Start(0.5f);
                break;

            case StatusType.FlyWait:
                this.statusTime.Start(0.1f);
                break;

            case StatusType.Fly:
                break;

            case StatusType.Finish:
                this.targetUI.AddHaveCount();
                base.Destroy();
                break;
        }
    }

    private void StatusUpdate()
    {
        Vector3 temp = this.transform.position;

        this.statusTime.Update();

        switch ( this.status )
        {
            case StatusType.PopUpWait:

                if (this.statusTime.IsFinished)
                {
                    this.StatusChange(StatusType.PopUp);
                }
                break;

            case StatusType.PopUp:
                temp.x +=this.popJumpX * Time.deltaTime;
                temp.y += ( this.popJumpY) * Time.deltaTime;
                //temp.y +=( -this.popJumpY + this.popGravityY ) * Time.deltaTime;

                this.popJumpY -= 25f * Time.deltaTime;
                this.popJumpX *= 1f -( 0.01f * Time.deltaTime);

                this.popScale += 0.5f * Time.deltaTime;

                this.transform.position = temp;

                if( this.statusTime.IsFinished )
                {
                    this.StatusChange(StatusType.FlyWait);
                }
                break;

            case StatusType.FlyWait:

                if (this.statusTime.IsFinished)
                {
                    this.StatusChange(StatusType.Fly);
                }
                break;


            case StatusType.Fly:

                Vector3 targetPosition = this.targetUI.GetScreenPosition();

                

                Vector3 myScreenPosition = SugorokuDirector.GetInstance().Camera.WorldToScreenPoint(this.transform.position);

                /*
                float w = this.transform.position.x - targetPosition.x;
                float h = this.transform.position.y - targetPosition.y;
                float a = Mathf.Atan2(h, w) + Mathf.PI;

                temp.x += Mathf.Cos(a) * 2f * Time.deltaTime;
                temp.y += Mathf.Sin(a) * 2f * Time.deltaTime;
                */
                this.popScale *= 1f - (0.01f * Time.deltaTime);

                float w = myScreenPosition.x - targetPosition.x;
                float h = myScreenPosition.y - targetPosition.y;
                float a = Mathf.Atan2(h, w) + Mathf.PI;
                float d = Mathf.Sqrt(h * h + w * w);

                this.w = (int)myScreenPosition.x;
                this.h = (int)myScreenPosition.y;

                float moveDistance = 30f * Time.deltaTime;

                if ( d <= 30f || h >= 0 )
                {
                    this.StatusChange(StatusType.Finish);
                    return;
                }

                temp.x += Mathf.Cos(a) * moveDistance;
                temp.y += Mathf.Sin(a) * moveDistance;

                this.transform.position = temp;


                break;
            case StatusType.Finish:
                break;
        }

        this.popRotateSpeed *= 1f - (0.1f * Time.deltaTime);
        this.Rotate += this.popRotateSpeed * Time.deltaTime;

        this.SetScale(this.popScale);
    }

    public int w, h;

    // Update is called once per frame
    void Update()
    {
        this.StatusUpdate();
    }
}
