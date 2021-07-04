using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KumaForRoom : MovablObject
{

    private static KumaForRoom Instance = null;
    public static KumaForRoom GetInstance()
    {
        if (KumaForRoom.Instance == null)
            KumaForRoom.Instance = GameObject.Find("Kuma").GetComponent<KumaForRoom>();

        return KumaForRoom.Instance;
    }

    private HomeDirector director;

    private Animator animator;
    private GameObject talkBalloonObject;
    //private SpriteRenderer talkBalloonRender;
    private GameObject body;

    private LimitTimeCounter speechTime;

    private TalkBalloon talkBalloon;

    public int animation_hash_stand { get; private set; }
    public int animation_hash_walk { get; private set; }

    public int animation_hash_sitin { get; private set; }
    public int animation_hash_sitactive { get; private set; }
    public int animation_hash_sitout { get; private set; }

    public int animation_hash_bedin { get; private set; }
    public int animation_hash_bedactive { get; private set; }
    public int animation_hash_bedout { get; private set; }

    public int animation_hash_glad { get; private set; }

    public int activeAnimationHash;


    public enum TurnType
    {
        Left ,
        Right,
    }
    private TurnType turnType;


    private void Awake()
    {
        this.director = HomeDirector.GetInstance();

        this.body = this.transform.Find("Body").gameObject;
        this.talkBalloonObject = this.body.transform.Find("TalkBalloon").gameObject;
        
        //this.talkBalloonRender = this.talkBalloonObject.transform.Find("Image").gameObject.GetComponent<SpriteRenderer>();
        this.talkBalloon = this.talkBalloonObject.transform.Find("Image").GetComponent<TalkBalloon>();

        this.speechTime = new LimitTimeCounter();
    }

    // Use this for initialization
    void Start ()
    {

        this.animator = this.GetComponent<Animator>();

        this.animation_hash_stand = Animator.StringToHash("Base Layer.Stand");
        this.animation_hash_walk = Animator.StringToHash("Base Layer.Walk");
        this.animation_hash_sitin = Animator.StringToHash("Base Layer.SitIn");
        this.animation_hash_sitactive = Animator.StringToHash("Base Layer.SitActive");
        this.animation_hash_sitout = Animator.StringToHash("Base Layer.SitOut");
        this.animation_hash_bedin = Animator.StringToHash("Base Layer.BedIn");
        this.animation_hash_bedactive = Animator.StringToHash("Base Layer.BedActive");
        this.animation_hash_bedout = Animator.StringToHash("Base Layer.BedOut");

        this.animation_hash_glad = Animator.StringToHash("Base Layer.Glad");


        this.activeAnimationHash = 0x00;
        this.AnimationStart(this.animation_hash_stand);
    }

	
	// Update is called once per frame
	void Update ()
    {
        /*
        this.speechTime.Update();
        if (this.speechTime.IsFinished)
        {
            this.Say(SpeechData.SpeechID_None);
        }
        */

        // クリックでリアクション
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("click.");
 
            GameObject clickedGameObject = null;
 
            Debug.Log(Input.mousePosition); 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray.origin); 
            Debug.Log(ray.direction); 
            // 衝突するレイヤーはすべて入れる
            int LayerObject = LayerMask.GetMask(new string[] { "Default" });
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2) ray.origin, (Vector2) ray.direction, 1, LayerObject);
 
            if (hit2d) {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log(clickedGameObject); 
                if (clickedGameObject.name == "Kuma") {
                    this.AnimationStart(this.animation_hash_glad);
                }
            } 
        }
	}


    public void Say( int speechID )
    {
        this.talkBalloon.Say(speechID);
    }
    /*
    public void Say( int speechID )
    {
        int starTotalCount = this.director.GetTotalStarCount();

        if( SpeechData.GetCanSpeech( speechID , starTotalCount ) )
        {
            this.talkBalloonRender.sprite = SpeechData.GetSprite(speechID);

            if( speechID != SpeechData.SpeechID_None )
                this.speechTime.Start(2);
        }
    }
    */


    public void AnimationStart( int animationHash )
    {

        if (this.activeAnimationHash != animationHash)
        {
            this.animator.CrossFade(animationHash, 0f, 0, 0.0f);
            this.activeAnimationHash = animationHash;
        }
    }

    public bool GetIsAnimationFinished()
    {
        return this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    public int GetAnimationHash()
    {
        return this.activeAnimationHash;
    }


    public bool GetIsSaying()
    {
        return this.talkBalloon.GetIsSaying();
    }

    /*
    public bool GetIsSaying()
    {
        return !this.speechTime.IsFinished;
    }
    */

    /*
    public void Stand()
    {
        this.AnimationStart(this.animation_hash_stand);
    }
    public void Walk()
    {
        this.AnimationStart(this.animation_hash_walk);
    }
    public void Sit()
    {
        this.AnimationStart(this.animation_hash_sitactive);
    }
    public void BedIn()
    {
        this.AnimationStart(this.animation_hash_bedin);
    }
    */

    public void TurnTypeChange( TurnType turnType )
    {
        Vector3 bodyScale = this.transform.localScale;
        Vector3 talkBalloonScale = this.talkBalloonObject.transform.localScale;

        this.turnType = turnType;

        if( turnType == TurnType.Left )
        {
            bodyScale.x = 1f;
            talkBalloonScale.x = 1f;
        }
        else
        {
            bodyScale.x = -1f;
            talkBalloonScale.x = -1f;
        }

        this.transform.localScale = bodyScale;
        this.talkBalloonObject.transform.localScale = talkBalloonScale;
    }


    public TurnType TurnDirection
    {
        get
        {
            if (this.transform.localScale.x > 0f)
                return TurnType.Left;
            else
                return TurnType.Right;
        }
    }


}
