using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpecialTitle : MonoBehaviour
{

    private LimitTimeCounter activeTime;

    private float angle;
    private float angleSeed;

    private Image image;


    private void Awake()
    {
        EventTrigger currentTrigger = this.gameObject.AddComponent<EventTrigger>();
        currentTrigger.triggers = new List<EventTrigger.Entry>();
        //↑ここでAddComponentしているので一応、初期化しています。

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter; //PointerClickの部分は追加したいEventによって変更してね
        entry.callback.AddListener(this.PointEnter);  //ラムダ式の右側は追加するメソッドです。

        currentTrigger.triggers.Add(entry);
    }

    void Start()
    {
        this.activeTime = new LimitTimeCounter();

        this.image = this.GetComponent<Image>();

        this.angleSeed = 0;
        this.angle = 20f;
    }

    public void Open()
    {

        this.angle = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        this.angleSeed += 10f * Time.deltaTime;
        this.angle *= (1f - 0.5f * Time.deltaTime);
        Vector3 temp = this.image.transform.eulerAngles;
        temp.z = 20 + Mathf.Cos(angleSeed) * angle;
        this.image.transform.eulerAngles = temp;
    }

    void PointEnter(BaseEventData data)
    {
        this.angle = 20f;

    }
}
