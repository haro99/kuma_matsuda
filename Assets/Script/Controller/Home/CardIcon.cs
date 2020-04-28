using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
using UnityEngine.Events;



public class CardIcon : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private EventTrigger.Entry entry;
    private int cardID;

    private HomeDirector director;

    private CardMenuController controller;

    // Use this for initialization
    void Awake ()
    {
        this.director = HomeDirector.GetInstance();

        //　EventTriggerコンポーネントを取り付ける
        this.eventTrigger = this.gameObject.AddComponent<EventTrigger>();
        //　イベントトリガーのEntryクラスを作成
        entry = new EventTrigger.Entry();
        //　イベントの種類を設定
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener(data => this.PointDown());

        this.controller = null;
        this.cardID = 0;

        //　イベントトリガーリストに作成したイベントを追加
        eventTrigger.triggers.Add(entry);
    }

    public void Initalize( int cardID , CardMenuController controller )
    {
        this.cardID = cardID;
        this.controller = controller;
        //Sprite spriteCardImage = Resources.Load("images/room/card/" + i, typeof(Sprite)) as Sprite;
        this.GetComponent<Image>().sprite = this.director.Resource.SpriteCardIconArray[cardID];
    }

    private void PointDown()
    {
        if (this.cardID == 0)
            return;
        if (this.controller == null)
            return;

        this.controller.ShowDetail(this.cardID);
    }
	// Update is called once per frame
	void Update ()
    {
		
	}
}
