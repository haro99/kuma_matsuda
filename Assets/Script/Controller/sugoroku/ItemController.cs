using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{

    GameObject player;
    GameObject enemy;
    GameObject map;

    private bool destroyFlg = false;
    public int ItemCount { get; set; }

    // Use this for initialization
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.map = GameObject.Find("Map");
        this.enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        // 当たり判定
        Vector2 p1 = transform.position;

        // 位置補正
        CharaController playerController = this.player.gameObject.GetComponent<CharaController>();
        Vector2 p2 = new Vector2(this.player.transform.position.x - playerController.adjustPosition.x, this.player.transform.position.y - playerController.adjustPosition.y);

        // 位置出力
        //Debug.Log("p1:" + p1.x + "," + p1.y);
        //Debug.Log("p2:" + p2.x + "," + p2.y);

        if (CollisionDetection(p1, p2))
        {
            if (!destroyFlg)
            {
                // item get
                this.itemGetAnimation();

                // 加算 
                this.incrementItemCount();
                
                destroyFlg = true;


            }
        }

        foreach(Transform child in enemy.transform)
        {
            CharaController charaController = child.gameObject.GetComponent<CharaController>();
            Vector2 p3 = new Vector2(child.transform.position.x - charaController.adjustPosition.x, child.transform.position.y - charaController.adjustPosition.y);
            if (CollisionDetection(p1, p3))
            {
                if (!destroyFlg)
                {
                    // item get
                    this.itemGetAnimation();

                    destroyFlg = true;


                    Const.Item item = this.GetItem();
                    GameObject tileObject = this.transform.parent.gameObject;

                    SugorokuDirector.GetInstance().AddEnemyGetFood(new EnemyGetFoodData(tileObject, item));

                }
            }
        }

    }

    private bool CollisionDetection(Vector2 p1, Vector2 p2)
    {
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.1f;
        float r2 = 0.1f;

        if (d < r1 + r2)
        {
            return true;
        }
        return false;
    }

    private void itemGetAnimation()
    {
        // アイテム画像
        GameObject obj = new GameObject();
        obj.transform.parent = gameObject.transform;
        obj.transform.position = gameObject.transform.position;
        SpriteRenderer render = obj.AddComponent<SpriteRenderer>();
        render.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        render.sortingLayerName = "Default";
        render.sortingOrder = 1;

        // ゲット画像
        Const.Item item = this.GetItem();

        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        string itemGetFilepath = "images/sugoroku/item/get/" + item.ToString();
        //Debug.Log ("item_get:" + itemGetFilepath);
        Animator animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load(itemGetFilepath, typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        gameObject.transform.Translate(0, 1, 0);
        gameObject.AddComponent<GetItemController>();

        obj.transform.Translate(0, -1, 0);
        GameObject.Destroy(obj, 0.2f);

        for (int childID = 0; childID < this.transform.parent.childCount; childID++)
        {
            ItemIconx2 icon = this.transform.parent.GetChild(childID).GetComponent<ItemIconx2>();
            if( icon != null )
                icon.Out();
        }

    }

    private void incrementItemCount()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        MapController mapController = map.GetComponent<MapController>();

        Const.Item item = this.GetItem();
        int itemNumber = mapController.getClearItemNumber(item);
        GameObject iconObject = GameObject.Find("MapIcon" + itemNumber);

        // 所持数
        GameObject itemCountObject = iconObject.transform.Find("ItemCount").gameObject;

        int itemCount = playerController.incrementItemCount(itemNumber , this.ItemCount);
        //Debug.Log ("itemCount:" + itemCount);
        Sprite spriteItemCountImage = Resources.Load("images/text/number/yellow/" + itemCount, typeof(Sprite)) as Sprite;
        itemCountObject.GetComponent<Image>().sprite = spriteItemCountImage;

        // 動き
        Vector3 pos = itemCountObject.transform.position;
        Vector3[] path = new Vector3[2];
        path[0] = new Vector3(pos.x, pos.y + 50.0f, 0);
        path[1] = new Vector3(pos.x, pos.y, 0);
        iTween.MoveTo(itemCountObject, iTween.Hash("path", path, "delay", 0, "time", 0.5f));

    }

    private Const.Item GetItem()
    {
        string[] split = gameObject.name.Split('_');
        //Debug.Log ("split name:" + split[split.Length - 1]);
        string itemName = split[split.Length - 1];
        return (Const.Item)Enum.Parse(typeof(Const.Item), itemName);
    }
}
