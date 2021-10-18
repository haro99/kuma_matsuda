using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharaController
{

    // Use this for initialization
    void Start()
    {
        this.Init();
    }

    public new void Init()
    {
        base.Init();

        //this.posX = 8;
        //this.posY = 6;

        //gameObject.transform.position = MapController.calPosPlayer(this.posX, this.posY);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override GameObject GetCharaObject()
    {
        return gameObject;
    }

    public override void ChangeMoveAnimation()
    {
        this.ChangeAnimationWalk();
    }

    public override void MoveChara(GameObject DirectorObject, Vector2 nextPos)
    {
        //Debug.Log("MoveChara:enemy walk.");
        Walk(DirectorObject, nextPos);
    }

    private void Walk(GameObject DirectorObject, Vector2 nextPos)
    {
        Debug.Log("Walk.");
        iTween.MoveTo(gameObject, iTween.Hash("x", nextPos.x, "y", nextPos.y, "delay", 0, "time", 0.2f, "oncomplete", "EnemyMoved", "oncompletetarget", gameObject));
        //this.OrderUpdate();
        this.isOrderUpdateRequest = true;

    }

    void EnemyMoved()
    {
        //Debug.Log("EnemyMoved: start.");

        if (this.getLimitValue() <= 0)
        {
            this.ChangeAnimationForwardStand();
        }
    }


    protected override void OrderUpdate()
    {
        SpriteRenderer renderChara = this.gameObject.GetComponent<SpriteRenderer>();
        renderChara.sortingOrder = SugorokuDirector.GetInstance().MapController.GetOrderInLayerForTile(this.posX, this.posY, MapController.MapObjectOrderIndex.Enemy);
    }
}
