using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharaController
{
    public GameObject sceneObject;
    private StageDataTable stageData;

    private int[] itemCount = new int[5] { 0, 0, 0, 0, 0 };

    [SerializeField]
    private bool isJump = false;
    [SerializeField]
    private bool reserveJump = false;

    private void Start()
    {
        this.Init();

        this.animationNow = Const.Animation.StandTrigger;
    }

    public new void Init()
    {
        base.Init();

        stageData = sceneObject.GetComponent<SugorokuScene>().GetStageData();
        int[] pos = stageData.GetStartPos();
        Debug.Log("start = [" + pos[0] + "," + pos[1] + "]");

        //this.InitalizePosition(pos[0], pos[1]);
        /*
        this.posX = pos[0];
        this.posY = pos[1];

        gameObject.transform.position = MapController.calPosPlayer(this.posX, this.posY, this.adjustPosition);
        */
    }

    public void InitalizePosition( int x, int y )
    {
        gameObject.transform.position = MapController.calPosPlayer(x, y, this.adjustPosition);
        this.posX = x;
        this.posY = y;
    }

    public int incrementItemCount(int number,int count)
    {
        if (SugorokuDirector.GetInstance().IsItemX2Mode)
        {
            itemCount[number] += (count*2);
            return itemCount[number];
        }
        else
        {
            itemCount[number] += count;
            return itemCount[number];
        }
    }

    public int[] getItemCount()
    {
        return itemCount;
    }

    public int getItemCount(int num)
    {
        return itemCount[num];
    }

    protected override GameObject GetCharaObject()
    {
        return transform.Find("Chara").gameObject;
    }

    public void ChangeAnimationRun()
    {
        this.ChangeLeftAndRightDirection();
        if (!this.isDownDirection)
        {
            this.ChangeAnimation(Const.Animation.RunBackTrigger);
        }
        else
        {
            this.ChangeAnimation(Const.Animation.RunTrigger);
        }
    }

    public void ChangeAnimationLaugh()
    {
        this.ChangeAnimation(Const.Animation.LaughTrigger);
    }

    public void ChangeAnimationSad()
    {
        this.ChangeAnimation(Const.Animation.SadTrigger);
    }

    public void ChangeAnimationAnger()
    {
        this.ChangeAnimation(Const.Animation.AngerTrigger);
    }

    public override void ChangeMoveAnimation()
    {
        this.ChangeAnimationRun();
    }

    public override void MoveChara(GameObject DirectorObject, Vector2 nextPos)
    {
        if (reserveJump && !isJump)
        {
            Jump(DirectorObject, nextPos);
        }
        else
        {
            Run(DirectorObject, nextPos);
        }
    }


    private void Jump(GameObject DirectorObject, Vector2 nextPos)
    {
        if (!isJump)
        {
            //Debug.Log("jump.");
            isJump = true;
            iTween.MoveTo(gameObject, iTween.Hash("x", nextPos.x, "y", nextPos.y + 1.5f, "delay", 0, "time", 0.4f, "oncomplete", "PlayerMoved", "oncompletetarget", DirectorObject));
            iTween.MoveAdd(gameObject, iTween.Hash("y", 0, "delay", 0.4f, "time", 0.1f, "oncomplete", "Jumped", "oncompletetarget", gameObject));
        }
        //Debug.Log("jumping.");
    }

    private void Jumped()
    {
        //Debug.Log("jumped.");
        reserveJump = false;
        isJump = false;

    }

    private void Run(GameObject DirectorObject, Vector2 nextPos)
    {
        //Debug.Log("Run.");
        iTween.MoveTo(gameObject, iTween.Hash("x", nextPos.x, "y", nextPos.y, "delay", 0, "time", 0.2f, "oncomplete", "PlayerMoved", "oncompletetarget", DirectorObject));

        //this.OrderUpdate();
        this.isOrderUpdateRequest = true;

    }

    public void ReserveJump()
    {
        //Debug.Log("reserve jump.");
        reserveJump = true;
    }

    public bool isJumping()
    {
        return isJump;
    }

    protected override void OrderUpdate()
    {
        SpriteRenderer renderChara = this.gameObject.transform.Find("Chara").GetComponent<SpriteRenderer>();
        renderChara.sortingOrder = SugorokuDirector.GetInstance().MapController.GetOrderInLayerForTile(this.posX, this.posY, MapController.MapObjectOrderIndex.Player);

        SpriteRenderer renderShadow = this.gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>();
        renderShadow.sortingOrder = SugorokuDirector.GetInstance().MapController.GetOrderInLayerForTile(this.posX, this.posY, MapController.MapObjectOrderIndex.Shadow);
    }
}
