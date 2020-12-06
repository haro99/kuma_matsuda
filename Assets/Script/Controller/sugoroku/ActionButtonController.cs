using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonController : MonoBehaviour
{
    public GameObject pushObject;

    private static int MAX_COUNT = 13;
    private static float CHANGE_TIME = 0.3f;

    private int count;
    private float mTime;

    private bool startCount;

    [SerializeField] SugorokuDirector m_Director;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        mTime += Time.deltaTime;
        if (mTime > CHANGE_TIME)
        {
            if (!startCount)
            {
                mTime = 0;
                this.count = 1;
                return;
            }

            ++count;

            // メーター画像変更
            Sprite spriteMeterImage = Resources.Load("images/sugoroku/meter/map_parts_meter" + count, typeof(Sprite)) as Sprite;
            gameObject.GetComponent<Image>().sprite = spriteMeterImage;

            if (count > MAX_COUNT)
            {
                gameObject.GetComponent<ActionButtonDirector>().stopDice();
                //サイコロのサウンド停止
                m_Director.GetSaiStartSound().Stop();
                this.StopCount();
                return;
            }

            mTime = 0;
        }
    }

    private void Init()
    {
        StopCount();

        // PUSH画像をつける
        this.PushOn();

    }

    public void StartCount()
    {
        this.count = 1;
        this.mTime = 0;

        this.startCount = true;

        // メーター画像変更
        Sprite spriteMeterImage = Resources.Load("images/sugoroku/meter/map_parts_meter" + count, typeof(Sprite)) as Sprite;
        gameObject.GetComponent<Image>().sprite = spriteMeterImage;

        // PUSH画像をつける
        this.PushOn();
    }

    public void StopCount()
    {
        this.count = MAX_COUNT;
        this.mTime = 0;

        this.startCount = false;

        // メーター画像変更
        Sprite spriteMeterImage = Resources.Load("images/sugoroku/meter/map_parts_meter" + count, typeof(Sprite)) as Sprite;
        gameObject.GetComponent<Image>().sprite = spriteMeterImage;

        // PUSH画像を消す
        this.PushOff();
    }

    public void PushOn()
    {
        // PUSH画像をつける
        pushObject.SetActive(true);
    }

    public void PushOff()
    {
        if (m_Director.GetSaiStartSound() != null)
        {
            //サイコロのサウンド停止
            m_Director.GetSaiStartSound().Stop();
        }
        // PUSH画像を消す
        pushObject.SetActive(false);
    }
}
