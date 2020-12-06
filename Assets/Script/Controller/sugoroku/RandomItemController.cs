using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemController : MonoBehaviour {

    SugorokuDirector director;
    StageDataTable StageData;

    float[] defaultViewTimes = { 0.3f, 0.3f, 0.3f, 0.3f };
    Const.Item[] viewItems;

    [SerializeField]
    float mTime;
    [SerializeField]
    int count;

    // Use this for initialization
    void Start () {
        this.director = GameObject.Find("SugorokuDirector").gameObject.GetComponent<SugorokuDirector>();
        this.StageData = GameObject.Find("SugorokuScene").GetComponent<SugorokuScene>().GetStageData();
        viewItems = StageData.ClearItem;
        mTime = 0;
        count = UnityEngine.Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update () {
        mTime += Time.deltaTime;

        if (mTime > defaultViewTimes[count]) {
            
            if (!this.director.isWaitDiceClick())
            {
                mTime = 0;
                return;
            }

            // Item
            GameObject itemObject = null;
            foreach (Transform child in gameObject.transform)
            {
                foreach(Const.Item item in viewItems)
                {
                    if (child.name == "work_item_" + item.ToString())
                    {
                        itemObject = child.gameObject;
                    }
                }
            }

            if (itemObject == null)
            {
                GetComponent<RandomItemController>().enabled = false;
                return;
            }

            count++;
            count %= viewItems.Length;

            // Tile
            string tileColorName = Enum.ToObject(typeof(Const.TileColor), (count + 1) % 10).ToString();
            Sprite spriteTileImage = Resources.Load("images/sugoroku/tile/tile_" + tileColorName, typeof(Sprite)) as Sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteTileImage;

            // Item
            //GameObject itemObject = gameObject.transform.Find("work_item_" + viesItems[beforeCount].ToString()).gameObject;
            string itemName = viewItems[count].ToString();
            //Debug.Log("itemName : " + itemName);
            itemObject.name = "work_item_" + itemName;
            string itemFilepath = "images/sugoroku/item/item_" + itemName;
            Sprite spriteItemImage = Resources.Load(itemFilepath, typeof(Sprite)) as Sprite;
            itemObject.transform.Find("Image").gameObject.GetComponent<SpriteRenderer>().sprite = spriteItemImage;

            mTime = 0;
        }
	}
}
