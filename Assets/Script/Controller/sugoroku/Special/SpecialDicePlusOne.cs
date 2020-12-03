using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDicePlusOne : MonoBehaviour
{

    private Animator animator;
    private int animation_hash_Appeal;
    private int animation_hash_In;

    private int activeAnimationHash;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.animation_hash_In = Animator.StringToHash("Base Layer.DicePlusOne_In");
        this.animation_hash_Appeal = Animator.StringToHash("Base Layer.DicePlusOne_Appeal");

    }

    // Update is called once per frame
    void Update()
    {
        if (this.activeAnimationHash == animation_hash_In && this.GetIsAnimationFinished())
            this.AnimationStart(this.animation_hash_Appeal);
        else if (this.activeAnimationHash == this.animation_hash_Appeal && SugorokuDirector.GetInstance().getGameStatus() != Const.GameStatus.special && SugorokuDirector.GetInstance().getGameStatus() != Const.GameStatus.special_effect)
            this.Close();

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

    public void Open()
    {
        this.gameObject.SetActive(true);
        this.AnimationStart( this.animation_hash_In);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
