using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBalloon : MonoBehaviour
{
    //private GameObject talkBalloon;
    private SpriteRenderer talkBalloonRender;

    private HomeDirector director;

    private int animation_hash_talkballon_hello;
    private int animation_hash_talkballon_Hungry;
    private int animation_hash_talkballon_Zzz;
    private int animation_hash_talkballon_Spleepy;
    private int animation_hash_talkballon_Bored;

    private int activeAnimationHash;
    private Animator animator;


    public enum StatusType
    { 
        None ,
        Speaking ,
        AfterDelay,
    }
    public StatusType status;
    private LimitTimeCounter statusTime;



    private void Awake()
    {
        this.director = HomeDirector.GetInstance();

        this.talkBalloonRender = this.GetComponent<SpriteRenderer>();

        this.statusTime = new LimitTimeCounter();

        this.animator = this.GetComponent<Animator>();

        this.animation_hash_talkballon_hello = Animator.StringToHash("Base Layer.talkballon_hello");
        this.animation_hash_talkballon_Hungry = Animator.StringToHash("Base Layer.talkballon_Hungry");
        this.animation_hash_talkballon_Zzz = Animator.StringToHash("Base Layer.talkballon_Zzz");
        this.animation_hash_talkballon_Spleepy = Animator.StringToHash("Base Layer.talkballon_Spleepy");
        this.animation_hash_talkballon_Bored = Animator.StringToHash("Base Layer.talkballon_Bored");


    }
    


    // Start is called before the first frame update
    void Start()
    {
        this.Say(SpeechData.SpeechID_None);
        
    }


    // Update is called once per frame
    void Update()
    {
        this.StatusUpdate();
    }




    private void StatusChange( StatusType status )
    {

        this.status = status;

        switch( this.status )
        {
            case StatusType.None:
                break;
            case StatusType.Speaking:
                this.statusTime.Start(2f);
                break;
            case StatusType.AfterDelay:
                this.statusTime.Start(3f);
                break;
        }
    }

    
    private void StatusUpdate()
    {
        this.statusTime.Update();

        switch (this.status)
        {
            case StatusType.None:
                break;
            case StatusType.Speaking:

                if (this.statusTime.IsFinished)
                {
                    this.Say(SpeechData.SpeechID_None);
                    this.StatusChange(StatusType.AfterDelay);
                }

                break;
            case StatusType.AfterDelay:
                if (this.statusTime.IsFinished)
                {
                    this.StatusChange(StatusType.None);
                }
                break;
        }
    }



    public void Say(int speechID)
    {
        int starTotalCount = this.director.GetTotalStarCount();

        if (SpeechData.GetCanSpeech(speechID, starTotalCount))
        {
            //this.talkBalloonRender.sprite = SpeechData.GetSprite(speechID);


            if( speechID == SpeechData.SpeechID_None)
            {
                this.talkBalloonRender.sprite = null;
            }
            else if (speechID == SpeechData.SpeechID_Hello )
            {
                this.talkBalloonRender.sprite = Resources.Load("images/room/balloon/Speech_Hello", typeof(Sprite)) as Sprite;
                this.AnimationStart(this.animation_hash_talkballon_hello);
            }
            else if (speechID == SpeechData.SpeechID_Spleepy)
            {
                this.talkBalloonRender.sprite = Resources.Load("images/room/balloon/Speech_Spleepy", typeof(Sprite)) as Sprite;
                this.AnimationStart(this.animation_hash_talkballon_Spleepy);
            }
            else if (speechID == SpeechData.SpeechID_Zzz)
            {
                this.talkBalloonRender.sprite = Resources.Load("images/room/balloon/Speech_Zzz", typeof(Sprite)) as Sprite;
                this.AnimationStart(this.animation_hash_talkballon_Zzz);
            }
            else if (speechID == SpeechData.SpeechID_Hungry)
            {
                this.talkBalloonRender.sprite = Resources.Load("images/room/balloon/Speech_Hungry", typeof(Sprite)) as Sprite;
                this.AnimationStart(this.animation_hash_talkballon_Zzz);
            }
            else if (speechID == SpeechData.SpeechID_Bored)
            {
                this.talkBalloonRender.sprite = Resources.Load("images/room/balloon/Speech_Bored", typeof(Sprite)) as Sprite;
                this.AnimationStart(this.animation_hash_talkballon_Bored);
            }


            if (speechID == SpeechData.SpeechID_None)
                this.StatusChange(StatusType.None);
            else
                this.StatusChange(StatusType.Speaking);
        }
    }


    public void AnimationStart(int animationHash)
    {
        //if (this.activeAnimationHash != animationHash)
        {
            this.animator.CrossFade(animationHash, 0f, 0, 0.0f);
            this.activeAnimationHash = animationHash;
        }
    }


    public bool GetIsSaying()
    {
        return this.status != StatusType.None;
    }


}
