using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDiceFree15SecCounter : MonoBehaviour
{

    private readonly int TimeMax = 15*100;

    private SpecialTimeNumber numberSec1;
    private SpecialTimeNumber numberSec2;
    private SpecialTimeNumber numberColon;
    private SpecialTimeNumber numberMillSec1;
    private SpecialTimeNumber numberMillSec2;

    //private SpecialDiceFreeTitleFlush flush;


    private AudioSource audioCount = null;
    private AudioSource audioCountLast5 = null;

    private int lastSec;

    // Start is called before the first frame update
    void Start()
    {
        this.numberSec1 = this.transform.Find("Digit_Sec1").GetComponent<SpecialTimeNumber>();
        this.numberSec2 = this.transform.Find("Digit_Sec2").GetComponent<SpecialTimeNumber>();
        this.numberColon = this.transform.Find("Digit_Colon").GetComponent<SpecialTimeNumber>();
        this.numberMillSec1 = this.transform.Find("Digit_MillSec1").GetComponent<SpecialTimeNumber>();
        this.numberMillSec2 = this.transform.Find("Digit_MillSec2").GetComponent<SpecialTimeNumber>();

        //this.flush = this.transform.Find("Title_Flush").GetComponent<SpecialDiceFreeTitleFlush>();



        this.lastSec = this.TimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        this.numberSec1.Open();
        this.numberSec2.Open();
        this.numberColon.Open();
        this.numberMillSec1.Open();
        this.numberMillSec2.Open();


        if (this.audioCount == null)
        {
            this.audioCount = this.gameObject.AddComponent<AudioSource>();
            this.audioCount.playOnAwake = false;
            this.audioCount.clip = SugorokuDirector.GetInstance().Resource.AudioSpecialDiceFreeCount;
        }

        if (this.audioCountLast5 == null)
        {
            this.audioCountLast5 = this.gameObject.AddComponent<AudioSource>();
            this.audioCountLast5.playOnAwake = false;
            this.audioCountLast5.clip = SugorokuDirector.GetInstance().Resource.AudioSpecialDiceFreeCountLast5;
        }


    }

    public void TimeUpdate( float ratio )
    {
        int timeNow = (int)( (float)TimeMax * ratio );

        int sec = timeNow / 100;

        int sec1 = sec / 10;

        if (sec1 == 0)
            this.numberSec1.ChangeNumber(10);
        else
            this.numberSec1.ChangeNumber(sec1);


        if( sec != this.lastSec )
        {
            if (sec <= 5)
            {
                this.numberSec1.Alert();
                this.numberSec2.Alert();
                this.numberColon.Alert();
                this.numberMillSec1.Alert();
                this.numberMillSec2.Alert();

                SugorokuDirector.GetInstance().SoundPlay(this.audioCountLast5);
            }
            else
                SugorokuDirector.GetInstance().SoundPlay(this.audioCount);


            if ( this.lastSec > 10 )
                this.numberSec1.HopStart();

            this.numberSec2.HopStart();
            //this.flush.Flush();
            this.lastSec = sec;
        }


        int sec2 = sec % 10;

        this.numberSec2.ChangeNumber(sec2);

        int millSecNow = timeNow % 100;

        int millSec1 = millSecNow / 10;
        this.numberMillSec1.ChangeNumber(millSec1);


        int millSec2 = millSecNow % 10;
        this.numberMillSec2.ChangeNumber(millSec2);

    }
}
