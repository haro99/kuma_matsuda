using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNumberEffecter : MonoBehaviour
{
    private RectTransform rectTransform;

    public int PopLevel;
    private int popCount;

    private float baseY;
    private float baseScaleX, baseScaleY;

    private Image image;

    public enum StatusType
    {
        Active,
        Pop,
        PopReturn
    }

    public StatusType status;

    private LimitTimeCounter statusTime;
    private LimitTimeCounter colorChangeTime;

    private LimitTimeCounter scaleChangeTime;



    private bool isClear;



    public void Pop(bool isClear)
    {
        this.PopLevel += 3;

        if (this.PopLevel > 10)
            this.PopLevel = 10;

        this.isClear = isClear;
        this.colorChangeTime.Start(0.35f);
        this.scaleChangeTime.Start(0.5f);

        this.popCount = 0;
        this.StatusChange(StatusType.Pop);

        this.BlurRotate();
    }

    private void BlurRotate()
    {
        Vector3 worldAngle = this.rectTransform.localEulerAngles;

        float angleLevel = 0.04f * this.PopLevel;
        worldAngle.z = Random.Range(-angleLevel, angleLevel) * Mathf.Rad2Deg;
        transform.localEulerAngles = worldAngle;
    }


    private void SetRotate(float angle)
    {
        Vector3 worldAngle = this.rectTransform.localEulerAngles;
        worldAngle.z = angle * Mathf.Rad2Deg;
        transform.localEulerAngles = worldAngle;
    }

    private void StatusChange(StatusType status)
    {
        this.status = status;

        switch (this.status)
        {
            case StatusType.Active:
                this.SetRotate(0f);
                break;

            case StatusType.Pop:
                //this.statusTime.Start(0.1f + 0.05f * (float)popCount);
                this.statusTime.Start(0.05f + (float)Random.Range(0.01f, 0.05f));
                break;
            case StatusType.PopReturn:
                this.statusTime.Start(0.05f + (float)Random.Range(0.01f, 0.05f));
                break;
        }

    }


    private void StatusUpdate()
    {
        switch (this.status)
        {
            case StatusType.Active:
                break;

            case StatusType.Pop:

                this.statusTime.Update();

                if (this.statusTime.IsFinished)
                {
                    this.PopLevel--;
                    this.popCount++;

                    if (this.popCount >= 5)
                        this.popCount = 5;

                    this.StatusChange(StatusType.PopReturn);
                }
                else
                {
                    //float scale = 0.98f + ( 0.02f + 0.1f * (float)Mathf.Min(this.PopLevel,5) )* this.statusTime.Ratio;
                    //this.rectTransform.localScale = new Vector3(scale, scale, 1f);

                    Vector3 temp = this.rectTransform.transform.localPosition;
                    temp.y = this.baseY + 10f * (float)Mathf.Min(this.PopLevel, 5) * this.statusTime.Ratio;
                    this.rectTransform.transform.localPosition = temp;
                }

                break;
            case StatusType.PopReturn:

                this.statusTime.Update();

                if (this.statusTime.IsFinished)
                {
                    this.BlurRotate();

                    if (this.PopLevel <= 0)
                        this.StatusChange(StatusType.Active);
                    else
                        this.StatusChange(StatusType.Pop);
                }
                else
                {
                    //float scale = 0.98f + (0.02f + 0.1f * (float)Mathf.Min(this.PopLevel, 5)) * this.statusTime.RatioReverse;
                    //this.rectTransform.localScale = new Vector3(scale, scale, 1f);

                    Vector3 temp = this.rectTransform.transform.localPosition;
                    temp.y = this.baseY + 10f * (float)Mathf.Min(this.PopLevel, 5) * this.statusTime.RatioReverse;
                    this.rectTransform.transform.localPosition = temp;

                }

                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        this.rectTransform = this.GetComponent<RectTransform>();
        this.image = this.GetComponent<Image>();
        this.image.color = new Color(1, 0.9f, 0f, this.image.color.a);


        this.baseScaleX = this.rectTransform.transform.localScale.x;
        this.baseScaleY = this.rectTransform.transform.localScale.y;

        this.baseY = this.rectTransform.transform.localPosition.y;

        this.colorChangeTime = new LimitTimeCounter();
        this.scaleChangeTime = new LimitTimeCounter();

        this.statusTime = new LimitTimeCounter();
        this.StatusChange(StatusType.Active);

    }

    // Update is called once per frame
    void Update()
    {
        this.StatusUpdate();

        if (!this.colorChangeTime.IsFinished)
        {
            this.colorChangeTime.Update();

            if (this.isClear)
            {
                this.image.color = new Color(0.6f + 0.4f * this.colorChangeTime.Ratio, 1f, 1f, this.image.color.a);
            }
            else
            {
                this.image.color = new Color(1f, 0.9f + 0.1f * this.colorChangeTime.Ratio, 1f * this.colorChangeTime.Ratio, this.image.color.a);
            }
        }

        if (!this.scaleChangeTime.IsFinished)
        {
            this.scaleChangeTime.Update();

            Vector3 scale = this.rectTransform.localScale;

            scale.x = this.baseScaleX + 1f * this.scaleChangeTime.Ratio;
            scale.y = this.baseScaleY + 1f * this.scaleChangeTime.Ratio;

            this.rectTransform.localScale = scale;
        }
    }
}
