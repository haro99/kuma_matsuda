using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMessage : MonoBehaviour
{

    private Image image;

    private float timeSeed;

    private int MessageID;

    private LimitTimeCounter shakeTime;

    private float basePositionX, basePositionY;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (this.MessageID == 0)
        {
            this.timeSeed += Time.timeScale * 0.02f;
            this.image.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Cos(this.timeSeed) * 0.4f + 0.6f);
        }
        else
        {
            this.timeSeed += Time.timeScale * 0.1f;
            //this.image.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Cos(this.timeSeed) * 0.1f + 0.9f);


            this.shakeTime.Update();

            float shakeRatio =  this.shakeTime.Ratio;

            Vector3 temp = this.image.transform.position;
            temp.x = this.basePositionX + Random.Range(-20f, 20f) * this.shakeTime.Ratio * shakeRatio;
            temp.y = this.basePositionY + Random.Range(-20f, 20f) * this.shakeTime.Ratio * shakeRatio;
            this.image.transform.position = temp;

            temp = this.image.transform.eulerAngles;
            temp.z = Mathf.Cos(timeSeed) * shakeRatio*1.5f;
            this.image.transform.eulerAngles = temp;


        }
    }

    public void Open()
    {
        this.MessageID = 0;

        this.image = this.GetComponent<Image>();
        this.timeSeed = 0f;
        this.MessageID = 0;
        this.shakeTime = new LimitTimeCounter();

        this.basePositionX = this.image.transform.position.x;
        this.basePositionY = this.image.transform.position.y;

        this.image.sprite = SugorokuDirector.GetInstance().Resource.SpritesSpecialMessage[0];
    }

    public void SpecialSelected( int selectedID )
    {
        this.MessageID = selectedID + 1;
        this.image.sprite = SugorokuDirector.GetInstance().Resource.SpritesSpecialMessage[this.MessageID];
        this.shakeTime.Start(1f);

        this.image.color = new Color(1.0f, 1.0f, 1.0f, 1f);

    }
}
