using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour
{
    [SerializeField]
    float t = 0;

    [SerializeField]
    float total = 0;

    int x = 0;
    int starNum = 1;

    int count = 0;

    [SerializeField]
    bool finishFlg = false;


    private float autoNextTime = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (isFinish())
        {
            t = 0;
            return;
        }

        if (total > 1f)
        {
            if (!this.GetComponent<Image>().enabled)
            {
                //Debug.Log("Image : active.");
                this.GetComponent<Image>().enabled = true;

            }
            //Debug.Log("Image : not active." + this.GetComponent<Image>().gameObject);

        }


        if (total > 1f && t > 0.6f)
        {
            //Debug.Log("rotate1.");
            x += 90;
            // 0.3秒かけて、x軸を90度回転
            iTween.RotateTo(gameObject, iTween.Hash("x", x % 180, "time", 0.3f, "oncomplete", "OnCompletionHalfRoundHandler"));

        }

        if (t > 0.6f)
        {
            total += t;
            t = 0;
        }


        // 3秒後 自動的に カットインor終了
        if (this.autoNextTime < 3f )
        {
            this.autoNextTime += Time.deltaTime;

            if( this.autoNextTime>=3f )
            {
                SugorokuDirector.GetInstance().actionButtonObject.GetComponent<ActionButtonDirector>().StartCutInAnimation();
            }
        }

    }


    public void OnCompletionHalfRoundHandler()
    {
        //Debug.Log("rotate2.");
        x += 90;
        // 0.3秒かけて、x軸を90度回転
        iTween.RotateTo(gameObject, iTween.Hash("x", x % 180, "time", 0.3f));

        count++;
        if (count == 3)
        {
            Sprite starImage = Resources.Load("images/sugoroku/star/" + starNum, typeof(Sprite)) as Sprite;
            this.GetComponent<Image>().sprite = starImage;

            iTween.PunchPosition(gameObject, iTween.Hash("y", 100.0f, "delay", 0.0f, "time", 0.3f));

            this.finish();
            return;
        }
    }

    public bool isFinish()
    {
        return finishFlg;
    }


    public void finish()
    {
        finishFlg = true;
        t = 0;
    }

    public void setStarNum(int num)
    {
        starNum = num;
    }
}
