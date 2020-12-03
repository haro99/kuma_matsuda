using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFrameController : MonoBehaviour
{
    bool choice;

    float mTime;

    bool flg;

    // Use this for initialization
    void Start()
    {
        choice = true;
        mTime = 0;

        flg = true;
    }

    // Update is called once per frame
    void Update()
    {
        mTime += Time.deltaTime;

        if (mTime > 0.5f)
        {
            if (!choice)
            {
                mTime = 0;
                return;
            }

            if (flg)
            {
                Sprite spriteFrameImage = Resources.Load("images/room/menu/sugoroku/room_flame2", typeof(Sprite)) as Sprite;
                gameObject.GetComponent<Image>().sprite = spriteFrameImage;
            }
            else
            {
                Sprite spriteFrameImage = Resources.Load("images/room/menu/sugoroku/room_flame3", typeof(Sprite)) as Sprite;
                gameObject.GetComponent<Image>().sprite = spriteFrameImage;
            }

            flg = !flg;
            mTime = 0;
        }
    }

    public void FrameOn()
    {
        //Debug.Log("FrameOn.");
        Sprite spriteFrameImage = Resources.Load("images/room/menu/sugoroku/room_flame2", typeof(Sprite)) as Sprite;
        gameObject.GetComponent<Image>().sprite = spriteFrameImage;

        gameObject.GetComponent<StageFrameController>().enabled = true;

        choice = true;
    }

    public void FrameOff()
    {
        //Debug.Log("FrameOff.");
        Sprite spriteFrameImage = Resources.Load("images/room/menu/sugoroku/room_flame4", typeof(Sprite)) as Sprite;
        gameObject.GetComponent<Image>().sprite = spriteFrameImage;

        gameObject.GetComponent<StageFrameController>().enabled = false;

        choice = false;
    }

    public bool Choice()
    {
        return choice;
    }
}
