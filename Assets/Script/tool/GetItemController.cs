using System;
using UnityEngine;
using System.Collections;

public class GetItemController : MonoBehaviour
{

    private float mLength;
    private float mCur;


    // Use this for initialization
    void Start()
    {
        Animator animOne = GetComponent<Animator>();
        AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
        mLength = infAnim.length;
        //Debug.Log("mLength : " + mLength);
        mCur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mCur += Time.deltaTime;
        if (mCur > mLength)
        {
            //Debug.Log("Destroy : " + mCur);
            GameObject.Destroy(gameObject);
        }
    }
}

