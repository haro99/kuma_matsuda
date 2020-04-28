using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharaController : MonoBehaviour
{
    [SerializeField]
    int limitValue = 0;

    protected Const.Animation animationNow;

    public int posX;
    public int posY;

    public Const.Direction direction;
    public Const.Direction choiceDirection;

    public bool isLeftDirection;
    public bool isDownDirection;

    [SerializeField]
    public Vector2 adjustPosition = new Vector2(0, 0);


    protected bool isOrderUpdateRequest;

    // Use this for initialization
    void Start()
    {
        this.Init();
    }

    public void Init()
    {

        this.direction = Const.Direction.none;
        this.choiceDirection = Const.Direction.none;

        this.isLeftDirection = true;
        this.isDownDirection = true;

        this.isOrderUpdateRequest = true;
    }

    public void Move(Const.Direction dir)
    {
        //Debug.Log ("direction = " + dir);

        this.direction = dir;

        if (Const.Direction.up == dir)
        {
            this.posY--;
            this.isLeftDirection = true;
            this.isDownDirection = false;
        }
        else if (Const.Direction.right == dir)
        {
            this.posX++;
            this.isLeftDirection = false;
            this.isDownDirection = false;
        }
        else if (Const.Direction.down == dir)
        {
            this.posY++;
            this.isLeftDirection = false;
            this.isDownDirection = true;
        }
        else if (Const.Direction.left == dir)
        {
            this.posX--;
            this.isLeftDirection = true;
            this.isDownDirection = true;
        }


        SugorokuDirector.GetInstance().SoundPlayCardSugorokuMove();
    }

    protected void ChangeAnimation(Const.Animation animation)
    {
        // 同一アニメーションの設定はしない
        if (this.animationNow != animation)
        {
            Animator animator = GetCharaObject().GetComponent<Animator>();
            animator.SetTrigger(animation.ToString());
            this.animationNow = animation;
            //Debug.Log("ChangeAnimation: " + animation.ToString());
        }
    }

    protected abstract GameObject GetCharaObject();

    protected abstract void OrderUpdate();

    public void changeBrightLayer()
    {
        GetCharaObject().GetComponent<SpriteRenderer>().sortingLayerName = "Bright";
    }

    public abstract void ChangeMoveAnimation();

    public void setLimitValue(int limitValue)
    {
        this.limitValue = limitValue;
    }

    public int getLimitValue()
    {
        return limitValue;
    }

    public int decreaseLimitValue()
    {
        return --limitValue;
    }

    public void setPos(int x, int y)
    {
        this.posX = x;
        this.posY = y;
    }

    public abstract void MoveChara(GameObject DirectorObject, Vector2 nextPos);

    public void ChangeAnimationForwardStand()
    {
        //Debug.Log("ChangeAnimationForwardStand: 20.");
        this.ChangeLeftAndRightDirection(true);
        this.ChangeAnimation(Const.Animation.StandTrigger);
    }

    public void ChangeAnimationStand()
    {
        //Debug.Log("ChangeAnimationStand: 10.");
        this.ChangeLeftAndRightDirection();
        if (!this.isDownDirection)
        {
            this.ChangeAnimation(Const.Animation.StandBackTrigger);
        }
        else
        {
            this.ChangeAnimation(Const.Animation.StandTrigger);
        }
    }

    public void ChangeAnimationWalk()
    {
        //Debug.Log("ChangeAnimationWalk.");
        this.ChangeLeftAndRightDirection();
        if (!this.isDownDirection)
        {
            this.ChangeAnimation(Const.Animation.WalkBackTrigger);
        }
        else
        {
            this.ChangeAnimation(Const.Animation.WalkTrigger);
        }
    }

    public void ChangeLeftAndRightDirection()
    {
        this.ChangeLeftAndRightDirection(false);
    }

    public void ChangeLeftAndRightDirection(bool isFront)
    {
        if (this.isLeftDirection)
        {
            // 左向き
            Debug.Log("dir : 左向き");
            Vector3 vec1 = this.transform.localScale;
            vec1.x = Mathf.Abs(this.transform.localScale.x);
            this.transform.localScale = vec1;

            //Transform transformChara = transform.Find("Chara");
            Transform transformChara;// = transform.Find("Chara");

            if (this is PlayerController)
            {
                transformChara = transform.Find("Chara");

            }
            else
            {
                transformChara = this.transform;
            }

            foreach (Transform child in transformChara)
            {
                //Debug.Log("child.name : "　+ child.name);
                if ("Canvas" == child.name)
                {
                    GameObject canvas = child.gameObject;
                    Vector3 vec2 = canvas.transform.localScale;
                    vec2.x = Mathf.Abs(canvas.transform.localScale.x);
                    canvas.transform.localScale = vec2;
                    Debug.Log("canvas.transform.localScal : "　+ canvas.transform.localScale);
                }
            }
        }
        else if (!this.isLeftDirection)
        {
            // 右向き
            //Debug.Log("dir : 右向き");
            Vector3 vec1 = this.transform.localScale;
            vec1.x = Mathf.Abs(this.transform.localScale.x) * -1;
            this.transform.localScale = vec1;

            Transform transformChara;// = transform.Find("Chara");

            if (this is PlayerController)
            {
                transformChara = transform.Find("Chara");
            }
            else
            {
                transformChara = this.transform;
            }

            foreach (Transform child in transformChara)
            {
                //if ("Canvas" == child.name)
                if ("Canvas" == child.name)
                {
                    GameObject canvas = child.gameObject;
                    Vector3 vec2 = canvas.transform.localScale;
                    vec2.x = Mathf.Abs(canvas.transform.localScale.x) * -1;
                    canvas.transform.localScale = vec2;
                }
            }
        }

        // 後ろ向きの画像の向きが逆のため反転
        if (!isFront && (Const.Direction.up == this.direction || Const.Direction.right == this.direction))
        {
            //Debug.Log("dir : 反転 (" + isFront + ")");
            Vector3 vec1 = this.transform.localScale;
            vec1.x = this.transform.localScale.x * -1;
            this.transform.localScale = vec1;

            Transform transformChara;// = transform.Find("Chara");

            if (this is PlayerController)
            {
                transformChara = transform.Find("Chara");
                Debug.Log("121 aaaa");
            }
            else
            {
                transformChara = this.transform;
            }

            foreach (Transform child in transformChara)
            {
                //if ("Canvas" == child.name)
                if ("Canvas" == child.name)
                {
                    GameObject canvas = child.gameObject;
                    Vector3 vec2 = canvas.transform.localScale;
                    vec2.x = canvas.transform.localScale.x * -1;
                    canvas.transform.localScale = vec2;
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (this.isOrderUpdateRequest)
        {
            this.OrderUpdate();
            this.isOrderUpdateRequest = false;
        }

    }
}
