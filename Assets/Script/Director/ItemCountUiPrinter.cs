using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountUiPrinter : MonoBehaviour
{

    private GameObject haveCounter;
    private GameObject needCounter;

    private ItemNumberEffecter digitEffect1;
    private ItemNumberEffecter digitEffect2;

    private int haveCount;

    private RectTransform rectTransform;

    //private Camera camera;

    private RectTransform flyTarget;

    private ItemIconEffecter icon;

    private int needCount;

    // Start is called before the first frame update
    void Start()
    {
        //this.camera = SugorokuDirector.GetInstance().Camera;

        this.needCount = 0;

        this.rectTransform = this.GetComponent<RectTransform>();

        this.haveCounter = this.transform.Find("Counter/Have").gameObject;
        this.needCounter = this.transform.Find("Counter/Need").gameObject;


        this.digitEffect2 = this.haveCounter.transform.Find("Digit2").gameObject.GetComponent<ItemNumberEffecter>();
        this.digitEffect1 = this.haveCounter.transform.Find("Digit1").gameObject.GetComponent<ItemNumberEffecter>();

        this.flyTarget = this.transform.Find("FlyTargetPoint").gameObject.GetComponent<RectTransform>();

        this.icon = this.transform.Find("BackGround").gameObject.GetComponent<ItemIconEffecter>();
        this.haveCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset( int need )
    {
        this.needCount = need;
        this.SetItemCount( this.haveCounter, "images/text/number/white/", 0, false);
        this.SetItemCount(this.needCounter, "images/text/number/white/", need, true);
    }

    public void CountForce( int have )
    {
        this.SetItemCount(this.haveCounter, "images/text/number/white/", have , false);
    }

    public void AddHaveCount()
    {
        this.haveCount++;

        this.SetItemCount(this.haveCounter, "images/text/number/white/", haveCount , false);
        this.icon.Pop();

        bool isClear = (this.haveCount >= this.needCount);

        this.digitEffect1.Pop( isClear );
        this.digitEffect2.Pop(isClear);


        SugorokuDirector.GetInstance().SoundPlayItemGet(); 
    }

    private void SetItemCount(GameObject counter, string numberResoucePath, int count, bool isAlignLeft)
    {
        if (count > 99)
            count = 99;

        Image digit2 = counter.transform.Find("Digit2").gameObject.GetComponent<Image>();
        Image digit1 = counter.transform.Find("Digit1").gameObject.GetComponent<Image>();

        if (count < 10)
        {
            if (isAlignLeft)
            {
                digit2.color = new Color(1f, 0.9f, 0f, 1f);
                digit1.color = new Color(1f, 0.9f, 0f, 0f);
                digit2.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;
            }
            else
            {
                digit2.color = new Color(1f, 0.9f, 0f, 0f);
                digit1.color = new Color(1f, 0.9f, 0f, 1f);
                digit2.sprite = Resources.Load(numberResoucePath + count / 10, typeof(Sprite)) as Sprite;
                digit1.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;
            }
        }
        else
        {
            digit2.color = new Color(1f, 0.9f, 0f, 1f);
            digit1.color = new Color(1f, 0.9f, 0f, 1f);
            digit2.sprite = Resources.Load(numberResoucePath + count / 10, typeof(Sprite)) as Sprite;
            digit1.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;

        }
        /*
        if (count < 10)
        {
            digit2.color = new Color(1f, 1f, 1f, 0.3f);
            digit1.color = new Color(1f, 1f, 1f, 1f);
            digit2.sprite = Resources.Load(numberResoucePath + count / 10, typeof(Sprite)) as Sprite;
            digit1.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;
        }
        else
        {
            digit2.color = new Color(1f, 1f, 1f, 1f);
            digit1.color = new Color(1f, 1f, 1f, 1f);
            digit2.sprite = Resources.Load(numberResoucePath + count / 10, typeof(Sprite)) as Sprite;
            digit1.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;
        }
        */
    }


    public Vector3 GetWorldPosition()
    {
        //UI座標からスクリーン座標に変換
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(this.GetComponent<Camera>(), this.rectTransform.position);

        //ワールド座標
        Vector3 result = Vector3.zero;

        //スクリーン座標→ワールド座標に変換
        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, screenPos, this.GetComponent<Camera>(), out result);

        return result;
    }

    public Vector3 GetScreenPosition()
    {
        //UI座標からスクリーン座標に変換
        //return RectTransformUtility.WorldToScreenPoint(this.camera, this.rectTransform.position);
        //return this.rectTransform.position;
        return this.flyTarget.position;
        
    }
}
