using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialTimeNumber : MonoBehaviour
{
    private Image image;

    private LimitTimeCounter effectTime;

    private Animator animator;
    private int animation_hash_Hop;
    private int animation_hash_Appeal;
    private int animation_hash_Active;

    private int activeAnimationHash;

    private bool isAlert;
    private bool isAlertVisible;

    // Start is called before the first frame update
    void Start()
    {
        this.image = this.GetComponent<Image>();

        this.effectTime = new LimitTimeCounter();
        this.isAlert = false;
        this.isAlertVisible = true;


        this.animator = this.GetComponent<Animator>();
        this.animation_hash_Hop = Animator.StringToHash("Base Layer.DiceFreeNumber_Hop");
        this.animation_hash_Appeal = Animator.StringToHash("Base Layer.DiceFreeNumber_Appeal");
        this.animation_hash_Active = Animator.StringToHash("Base Layer.DiceFreeNumber_Active");

    }

    // Update is called once per frame
    void Update()
    {
        if( this.activeAnimationHash == this.animation_hash_Appeal && SugorokuDirector.GetInstance().getGameStatus()!=Const.GameStatus.special && SugorokuDirector.GetInstance().getGameStatus() != Const.GameStatus.special_effect)
            this.AnimationStart(this.animation_hash_Active);


        if (this.isAlert)
        {
            this.effectTime.Update();

            if (this.effectTime.IsFinished)
            {
                this.effectTime.Start(0.1f);

                this.isAlertVisible = !this.isAlertVisible;
            }
        }
    }

    private void LateUpdate()
    {
        if (this.isAlert)
        {
            if (this.isAlertVisible)
            {
                this.image.color = new Color(1f, 1f, 1f, 1f);

            }
            else
            {
                this.image.color = new Color(1f, 1f, 1f, 0.5f);
            }

        }


    }

    public void ChangeNumber( int number )
    {
        this.image.sprite = SugorokuDirector.GetInstance().Resource.SpritesNumberParts[ number ];
    }

    public void HopStart()
    {
        //this.effectTime.Start(0.2f);
        this.AnimationStart(this.animation_hash_Hop);
    }

    public void Open()
    {
        this.AnimationStart(this.animation_hash_Appeal);
    }

    public void Alert()
    {
        this.isAlert = true;
    }


    private void ChangeScale( float scale )
    {
        Vector3 temp = this.transform.localScale;
        temp.x = scale;
        temp.y = scale;
        this.transform.localScale = temp;
    }

    private void AnimationStart(int animationHash)
    {
        this.activeAnimationHash = animationHash;
        this.animator.CrossFade(animationHash, 0f, 0, 0.0f);
    }
}
