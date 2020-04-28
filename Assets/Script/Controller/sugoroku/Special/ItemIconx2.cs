using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconx2 : MonoBehaviour
{


    private Animator animator;

    private int activeAnimationHash;

    private int animation_hash_In;
    private int animation_hash_Active;
    private int animation_hash_Appeal;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();

        this.animation_hash_In = Animator.StringToHash("Base Layer.ItemIcon_In");
        this.animation_hash_Active = Animator.StringToHash("Base Layer.ItemIcon_Active");
        this.animation_hash_Appeal = Animator.StringToHash("Base Layer.ItemIcon_Appeal");

        this.AnimationStart(this.animation_hash_In);
    }

    // Update is called once per frame
    void Update()
    {
        if( this.activeAnimationHash == animation_hash_In && this.GetIsAnimationFinished() )
            this.AnimationStart(this.animation_hash_Appeal);
        if (this.activeAnimationHash == animation_hash_Appeal && (SugorokuDirector.GetInstance().getGameStatus() != Const.GameStatus.special && SugorokuDirector.GetInstance().getGameStatus() != Const.GameStatus.special_effect ))
            this.AnimationStart(this.animation_hash_Active);
    }

    private void AnimationStart(int animationHash)
    {
        if (this.activeAnimationHash != animationHash)
        {
            this.animator.CrossFade(animationHash, 0f, 0, 0.0f);
            this.activeAnimationHash = animationHash;
        }
    }

    private bool GetIsAnimationFinished()
    {
        return this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    public void Out()
    {
        GameObject.Destroy(this.gameObject);
    }
}
