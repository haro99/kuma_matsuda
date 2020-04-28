using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpecialSelecter : MonoBehaviour
{
    private SugorokuDirector director;
    private Image selectedImage;


    private enum StatusType
    {
        In ,
        RollFullSpeed ,
        RollDown,
        Selected ,

        None ,
    }

    private StatusType status;


    private LimitTimeCounter statusTime;

    private int selectedID;

    private float rollCoolTime;

    private int rollIndex;

    private int rollLastCount;

    private LimitTimeCounter shakeTime;

    private float basePositionX, basePositionY;

    private GameObject selectedItemObject;

    private readonly float RollSpeedMin = 1.5f;

    private SpecialMessage message;
    private SpecialTitle title;
    private SpecialFlush flush;
    private SpecialFlush noise;

    private AudioSource audioSourceRoll;
    private AudioSource audioSourceSelected;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        this.statusTime = new LimitTimeCounter();
        this.shakeTime = new LimitTimeCounter();

    }

    public void Open()
    {
        this.director = SugorokuDirector.GetInstance();

        if (this.audioSourceRoll == null)
        {
            this.audioSourceRoll = this.gameObject.AddComponent<AudioSource>();
            this.audioSourceRoll.playOnAwake = false;
            this.audioSourceRoll.clip = SugorokuDirector.GetInstance().Resource.AudioSpecialRoll;
        }

        if (this.audioSourceSelected == null)
        {
            this.audioSourceSelected = this.gameObject.AddComponent<AudioSource>();
            this.audioSourceSelected.playOnAwake = false;
            this.audioSourceSelected.clip = SugorokuDirector.GetInstance().Resource.AudioSpecialSelected;
        }


        this.gameObject.SetActive(true);


        this.selectedItemObject = this.transform.Find("SelectedItem").gameObject;
        this.selectedImage = selectedItemObject.GetComponent<Image>();

        GameObject messageObject = this.transform.Find("Message").gameObject;
        this.message = messageObject.GetComponent<SpecialMessage>();
        this.message.Open();

        GameObject titleObject = this.transform.Find("Title").gameObject;
        this.title = titleObject.GetComponent<SpecialTitle>();
        this.title.Open();

        GameObject flushObject = this.transform.Find("Flush").gameObject;
        this.flush = flushObject.GetComponent<SpecialFlush>();

        GameObject noiseObject = this.transform.Find("Noise").gameObject;
        this.noise = noiseObject.GetComponent<SpecialFlush>();

        this.basePositionX = this.selectedItemObject.transform.position.x;
        this.basePositionY = this.selectedItemObject.transform.position.y;


        this.statusTime = new LimitTimeCounter();
        this.shakeTime = new LimitTimeCounter();

        //this.selectedID = (int)Const.Special.Item_x2;
        //this.selectedID = UnityEngine.Random.Range(0, 3);
        int seed = UnityEngine.Random.Range(0, 10);

        if( seed>=8 )
            this.selectedID = (int)Const.Special.Dice_Free_15Sec;
        else if (seed >= 4)
            this.selectedID = (int)Const.Special.Item_x2;
        else
            this.selectedID = (int)Const.Special.Dice_Plus_One;

        //aaa
        this.selectedID = (int)Const.Special.Item_x2;


        this.rollIndex = 0;

        this.rollLastCount = 10;
        //this.selectedImage.sprite = this.director.Resource.SpriteSpecialFrame[ this.rollIndex ];
        this.RollChange(this.rollIndex);

        this.StatusChange(StatusType.RollFullSpeed);

    }

    private void RollChange( int index )
    {
        this.selectedImage.sprite = this.director.Resource.SpritesSpecialFrame[index];

        this.noise.Flush();

        if( this.status == StatusType.RollDown )
            this.shakeTime.Start(0.2f);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.StatusUpdate();


        if( !this.shakeTime.IsFinished )
        {
            this.shakeTime.Update();


            if (this.rollCoolTime > 0f)
            {
                float shakeRatio = 1f - ((RollSpeedMin - rollCoolTime) / RollSpeedMin);

                Vector3 temp = this.selectedItemObject.transform.position;
                temp.x = this.basePositionX + UnityEngine.Random.Range(-10f, 10f) * this.shakeTime.Ratio * shakeRatio;
                temp.y = this.basePositionY + UnityEngine.Random.Range(-10f, 10f) * this.shakeTime.Ratio * shakeRatio;
                this.selectedItemObject.transform.position = temp;
            }
        }


    }


    private void StatusChange( StatusType status )
    {
        this.status = status;

        switch( this.status )
        {
            case StatusType.RollFullSpeed:
                this.rollCoolTime = 0.1f;
                this.statusTime.Start(this.rollCoolTime);

                this.rollLastCount = UnityEngine.Random.Range(5, 10);

                break;

            case StatusType.Selected:
                this.message.SpecialSelected(this.selectedID);
                this.statusTime.Start(3f);
                this.director.SoundPlay(this.audioSourceSelected);
                this.flush.Flush(5);

                break;
        }
    }

    private void StatusUpdate()
    {

        this.statusTime.Update();

        switch (this.status)
        {
            case StatusType.RollFullSpeed:

                if( this.statusTime.IsFinished )
                {
                    this.director.SoundPlay(this.audioSourceRoll);
                    this.rollIndex++;

                    if (this.rollIndex > 2)
                        this.rollIndex = 0;

                    this.RollChange(this.rollIndex);
                    this.statusTime.Start(this.rollCoolTime);

                    this.rollLastCount--;

                    if (this.rollLastCount <= 0)
                        this.StatusChange(StatusType.RollDown);
                }

                break;

            case StatusType.RollDown:

                if (this.statusTime.IsFinished)
                {
                    this.director.SoundPlay(this.audioSourceRoll);

                    this.rollIndex++;

                    if (this.rollIndex > 2)
                        this.rollIndex = 0;

                    this.RollChange(this.rollIndex);
                    this.statusTime.Start(this.rollCoolTime);

                    if( this.rollCoolTime >= RollSpeedMin)
                        this.rollCoolTime += 0.05f;
                    else
                        this.rollCoolTime *=1.2f;



                    if (this.rollCoolTime> RollSpeedMin && this.rollIndex == this.selectedID )
                        this.StatusChange(StatusType.Selected);
                }

                break;

            case StatusType.Selected:
                
                if( this.statusTime.IsFinished )
                {
                    this.StatusChange(StatusType.None);


                    this.director.SpecialSelected( (Const.Special)Enum.ToObject(typeof(Const.Special), this.selectedID)  );
                }
                    
                break;
        }
    }




}
