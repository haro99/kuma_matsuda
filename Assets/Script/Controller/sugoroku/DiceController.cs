using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{

    public enum State
    {
        none = 0,
        starting = 1,
        rotation = 2,
        stoping = 3,
        stop = 4
    }

    public Sprite number1;
    public Sprite number2;
    public Sprite number3;
    public Sprite number4;
    public Sprite number5;
    public Sprite number6;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public State state;
    private int value;

    public GameObject SugorokuDirector;

    private float mTime;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        state = State.none;

        this.Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case (State.none):
                this.None();
                break;
            case (State.starting):
                this.Init();
                break;
            case (State.rotation):
                mTime = 0;
                break;
            case (State.stoping):
                this.Stop();
                break;
            case (State.stop):
                break;
        }
        //Debug.Log("Dice:" + value);
        //Debug.Log("State:" + state);
    }

    void None()
    {
        animator.enabled = false;
        //gameObject.SetActive (false);
    }

    void Init()
    {
        //Debug.Log("Dice.Init: start.");
        //this.transform.localPosition = new Vector3(2.5f, -3, 10);
        animator.speed = 3;
        animator.enabled = true;
        state = State.rotation;

        // ランダム
        value = Random.Range(1, 7);
        //value = 35;
    }

    void Stop()
    {
        mTime += Time.deltaTime;

        if (animator.speed >= 3)
        {
            GameObject shadow = gameObject.transform.Find("Canvas").gameObject.transform.Find("shadow").gameObject;
            iTween.ScaleTo(shadow, iTween.Hash("x", 0.5f, "y", 0.5f, "delay", 0.0f, "time", 0.1f));
            iTween.ScaleTo(shadow, iTween.Hash("x", 1.0f, "y", 1.0f, "delay", 0.1f, "time", 0.1f));
            iTween.ScaleTo(shadow, iTween.Hash("x", 0.5f, "y", 0.5f, "delay", 0.2f, "time", 0.1f));
            iTween.ScaleTo(shadow, iTween.Hash("x", 1.0f, "y", 1.0f, "delay", 0.3f, "time", 0.1f));

            //Debug.Log("Stop: iTween.");
            Vector3 pos = this.gameObject.transform.position;
            Vector3[] path = new Vector3[6];
            path[0] = new Vector3(pos.x - 1.0f, pos.y + 7.0f, 0);
            path[1] = new Vector3(pos.x - 2.0f, pos.y + 1.0f, 0);
            path[2] = new Vector3(pos.x - 2.5f, pos.y + 3.5f, 0);
            path[3] = new Vector3(pos.x - 3.0f, pos.y + 1.5f, 0);
            path[4] = new Vector3(pos.x - 3.5f, pos.y + 3.5f, 0);
            path[5] = new Vector3(pos.x - 4.0f, pos.y + 2.0f, 0);
            iTween.MoveTo(this.gameObject, iTween.Hash("path", path, "delay", 0, "time", 0.5f, "easeType", iTween.EaseType.linear));
            //iTween.MoveBy(this.gameObject, iTween.Hash("x", -1.0f, "y", 1.0f, "delay", 0, "time", 1.0f, "easeType", iTween.EaseType.easeInOutCubic));
        }

        if (mTime < 0.5f)
        {
            if (animator.speed > 0.1f)
            {
                animator.speed = animator.speed - 0.1f;
            }
        }
        else
        {
            switch (value)
            {
                case (1):
                    spriteRenderer.sprite = number1;
                    break;
                case (2):
                    spriteRenderer.sprite = number2;
                    break;
                case (3):
                    spriteRenderer.sprite = number3;
                    break;
                case (4):
                    spriteRenderer.sprite = number4;
                    break;
                case (5):
                    spriteRenderer.sprite = number5;
                    break;
                case (6):
                    spriteRenderer.sprite = number6;
                    break;
                default:
                    spriteRenderer.sprite = number1;
                    break;
            }
            //iTween.RotateFrom(gameObject, iTween.Hash("z", -5, "time", 0.1f, "islocal", true));
            animator.speed = 0;
            animator.enabled = false;

            state = State.stop;
        }
        //Debug.Log("Speed:" + animator.speed);
    }

    public void changeState(State state)
    {
        this.state = state;
    }

    public int getValue()
    {
        return value;
    }

    public bool isRotation()
    {
        return (State.rotation == state);
    }

    public bool isStop()
    {
        return (State.stop == state);
    }

    public void StartDice()
    {
        //Debug.Log("StartDice: start.");
        this.changeState(State.starting);
    }

    public void StopDice()
    {
        //Debug.Log("StopDice: start.");
        this.changeState(State.stoping);
        this.SugorokuDirector.GetComponent<SugorokuDirector>().ChangeDiceWait();
    }

}
