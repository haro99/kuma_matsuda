using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class MapController : MonoBehaviour
{

    public GameObject sceneObject;
    private StageDataTable stageData;

    public GameObject mapTileObject;
    public GameObject enemyObject;


    private static float POS_X = 1.0f;
    private static float POS_Y = 0.5f;

    private static int HOME_X = 8;
    private static int HOME_Y = 9;
    //private int HOME_X = 8;
    //private int HOME_Y = 9;

    public int TileXMax { get; private set; }
    public int TileYMax { get; private set; }

    public static int TileID_RandomTile = 8;
    public static int TileID_Goal = -1;

    private List<FieldDataX> mapData;
    private TileDataIndex[,] tileDataItems;
    //private List<RandomMapTileAnchorRequest> anchorRequestList;

    private SugorokuDirector director;

    private int enemynumber;

    // Use this for initialization
    void Start()
    {
        this.init();

        StopwatchTimer timer = new StopwatchTimer();

        enemynumber = 1;

        timer.Start();
        createMap();
        timer.Stop();

        this.director.AddDebugText("マップ作製時間(ミリ秒)  " + timer.Time);

    }

    private void init()
    {
        //this.debugText = SugorokuDirector.GetInstance().DebugText;
        this.director = SugorokuDirector.GetInstance();

        this.stageData = sceneObject.GetComponent<SugorokuScene>().GetStageData();

        this.director.SetDiceCount(this.stageData.DiceCount);

        if( this.stageData.BackgroundPrefab != null )
        {
            GameObject mapCamera = GameObject.Find("CameraMap");
            
            GameObject mapBackground = Instantiate(this.stageData.BackgroundPrefab,new Vector3(0,0,0), Quaternion.identity);
            mapBackground.transform.parent = mapCamera.transform;
            
        }

        //this.clearRouteItemCount = this.stageData.ClearItemCount[0] + this.stageData.ClearItemCount[1] + this.stageData.ClearItemCount[2] + this.stageData.ClearItemCount[3] + 3;

        // 最大サイズを設定
        TileXMax = stageData.FieldDataYList[0].fieldDataXList.Count;
        TileYMax = stageData.FieldDataYList.Count;
        //Debug.Log ("MAX_X:" + MAX_X + " MAX_Y:" + MAX_Y);

        // ホームを設定
        //Debug.Log ("HOME_X:" + HOME_X + " HOME_Y:" + HOME_Y);

        this.director.AddDebugText("マップサイズ " + TileXMax + " x " + TileYMax);

        HOME_X = TileXMax / 2;
        HOME_Y = TileYMax / 2;

        this.tileDataItems = new TileDataIndex[TileXMax, TileYMax];

        for( int initX=0; initX<TileXMax; initX ++)
            for (int initY = 0; initY < TileYMax; initY++)
            {
                tileDataItems[initX, initY] = null;
            }



        int tryCount = 0;

        bool randomMapCreateSuccess = false;

        for ( tryCount = 0; tryCount < 30; tryCount ++ )
        {
            // データ複製
            this.mapData = new List<FieldDataX>();
            for (int lineID = 0; lineID < stageData.FieldDataYList.Count; lineID++)
            {
                List<int> xLine = new List<int>();
                for (int dataID = 0; dataID < stageData.FieldDataYList[0].fieldDataXList.Count; dataID++)
                {
                    xLine.Add(stageData.FieldDataYList[lineID].fieldDataXList[dataID]);

                }
                mapData.Add(new FieldDataX(xLine));
            }

            this.tileGoal = null;
            this.SearchGoal();

            if( this.tileGoal != null )
            {
                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.InitalizePosition(this.tileGoal.MapIndexX, this.tileGoal.MapIndexY);
            }



            if (this.CreateRandomMap())
            {
                randomMapCreateSuccess = true;
                break;
            }
        }

        if( randomMapCreateSuccess )
        {
            this.director.AddDebugText( "ランダムマップ作製 成功 Retry " + tryCount);
        }
        else
        {
            this.director.AddDebugText("ランダムマップ作製 失敗 Retry " + tryCount );
        }

    }

    private void createMap()
    {

        // MapIconを生成
        foreach (Const.Item item in stageData.ClearItem)
        {
            createMapIcon(item);
        }


        // MapTile
        for (int i = 0; i < TileXMax; ++i)
        {
            for (int j = 0; j < TileYMax; ++j)
            {
                int x = i;
                int y = j;
                int data = this.getMapData(x, y);

                if (data == 1)
                {
                    // for Tile
                    GameObject tileColorObject = this.createTile(x, y, 9); // 9=gray
                }
                else if (data >= 10 && data <= 99 )
                {
                    //Debug.Log ("X:" + x + " Y:" + y);

                    // for Tile
                    GameObject tileColorObject = this.createTile(x, y, data);

                    //if (data >= 1 && data <= 4)
                    
                    if( data%10 > 0 )
                    {
                        // for Item
                        this.CreateMapItem(x, y, tileColorObject, data);
                    }

                }
                else if (data == 9)
                {
                    // for Tile
                    GameObject tileColorObject = this.createTile(x, y, 1);
                    // for Item
                    this.CreateMapItem(x, y, tileColorObject, 10 + UnityEngine.Random.Range(1,this.stageData.RandomMapItemCountMaxInTile+1 ) );
                    tileColorObject.AddComponent<RandomItemController>();
                }
                else if (data == 7)
                {
                    // スペシャルマス

                    GameObject tileColorObject = new GameObject("work_tile_" + x + "_" + y);
                    tileColorObject.transform.parent = mapTileObject.transform;
                    Sprite spriteTileImage = Resources.Load("images/sugoroku/tile/tile_special", typeof(Sprite)) as Sprite;
                    tileColorObject.AddComponent<SpriteRenderer>().sprite = spriteTileImage;
                    tileColorObject.AddComponent<RectTransform>().anchoredPosition = new Vector3(calPosX(x, y), calPosY(x, y), 0);
                    //tileColorObject.GetComponent<SpriteRenderer>().sortingLayerName = "MapTile";
                    tileColorObject.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
                    tileColorObject.GetComponent<SpriteRenderer>().sortingOrder = GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Tile);

                    TileData tileData = tileColorObject.AddComponent<TileData>();
                    //tileData.SetMapIndex(x, y , data);
                    tileData.SetMapIndex(x,y,this.getMapData(x,y));
                }

                else if (data == 5)
                {
                    Debug.Log("敵生成");
                    Debug.Log(data);
                    // プレハブを取得、エネミーを生成
                    GameObject prefab = (GameObject)Resources.Load("prefabs/enemy/enemy" + enemynumber);
                    // プレハブからインスタンスを生成
                    //prefab.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
                    prefab.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), calPosY(x, y), 0), Quaternion.identity);
                    Object.transform.parent = enemyObject.transform;

                    CharaController chara = Object.GetComponent<CharaController>();
                    chara.setPos(x, y);

                    Vector3 vec = chara.transform.localPosition;
                    chara.transform.localPosition = new Vector3(vec.x + chara.adjustPosition.x, vec.y + chara.adjustPosition.y, vec.z);


                    // tile
                    this.createTile(x, y, 9); // 9=gray
                    //Debug.Log("X:" + x + " Y:" + y);
                    if(enemynumber<8)
                        enemynumber++;
                }
                else if (data == -2)
                {
                    // プレハブを取得
                    GameObject prefab = (GameObject)Resources.Load("prefabs/tile/start");
                    prefab.GetComponent<SpriteRenderer>().sortingOrder = GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Tile);
                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y)), 0), Quaternion.identity);
                    Object.transform.parent = mapTileObject.transform;
                }
                else if (data == -1)
                {
                    // プレハブを取得
                    GameObject prefab = (GameObject)Resources.Load("prefabs/tile/goal");
                    //prefab.GetComponent<SpriteRenderer>().sortingOrder = getOrderInLayerForTile(x, y , MapObjectOrderIndex.Tile);

                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y)), 0), Quaternion.identity);

                    SpriteRenderer render = Object.GetComponent<SpriteRenderer>();
                    render.sortingLayerName = "MapObject";
                    render.sortingOrder = GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Tile);


                    SpriteRenderer childRender = Object.transform.Find("goal").GetComponent<SpriteRenderer>();
                    childRender.sortingLayerName = "MapObject";
                    childRender.sortingOrder = GetOrderInLayerForTile(x, y, MapObjectOrderIndex.TilePattern);

                    TileData tileData = Object.AddComponent<TileData>();
                    tileData.SetMapIndex(x, y, data);
                    //tileData.SetGoalDirection(0, 0, 0);


                    // タイルの上のゴール画像も更新

                    Object.transform.parent = mapTileObject.transform;
                }
                else if (data == MapController.TileID_RandomTile )
                {

                }

                /*
                else if (data == 100)
                {
                    // 家

                    // プレハブを取得
                    GameObject prefab = (GameObject)Resources.Load("prefabs/tile/house");
                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y) - 0.25f), 0), Quaternion.identity);
                    Object.transform.parent = mapTileObject.transform;

                    SpriteRenderer render = Object.GetComponent<SpriteRenderer>();
                    render.sortingLayerName = "MapObject";
                    render.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Building);
                }
                else if (data == 110)
                {
                    // 木

                    // プレハブを取得
                    GameObject prefab = (GameObject)Resources.Load("prefabs/tile/tree");
                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y) - 0.25f), 0), Quaternion.identity);
                    Object.transform.parent = mapTileObject.transform;

                    SpriteRenderer render = Object.GetComponent<SpriteRenderer>();
                    render.sortingLayerName = "MapObject";
                    render.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Building);
                }
                else if (data == 111)
                {
                    // 木柵

                    // プレハブを取得
                    GameObject prefab = (GameObject)Resources.Load("prefabs/tile/wood_bar");
                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y) - 0.25f), 0), Quaternion.identity);
                    Object.transform.parent = mapTileObject.transform;

                    SpriteRenderer render = Object.GetComponent<SpriteRenderer>();
                    render.sortingLayerName = "MapObject";
                    render.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Building);
                }

                else if (data == 112)
                {
                    // かかし

                    // プレハブを取得
                    GameObject prefab = (GameObject)Resources.Load("prefabs/tile/kakasi");
                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y) - 0.25f), 0), Quaternion.identity);
                    Object.transform.parent = mapTileObject.transform;

                    SpriteRenderer render = Object.GetComponent<SpriteRenderer>();
                    render.sortingLayerName = "MapObject";
                    render.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Building);
                }
                else if (data == 113)
                {
                    // プレハブを取得
                    GameObject prefab = (GameObject)Resources.Load("prefabs/tile/leaf");
                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y) - 0.25f), 0), Quaternion.identity);
                    Object.transform.parent = mapTileObject.transform;

                    this.SetChildOrder(Object, this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Building));
                }
                */
                else if ( data >= 100 )
                {
                    GameObject prefab = this.director.Resource.GetMapObjectPrefab(data); ;
                    // プレハブからインスタンスを生成
                    GameObject Object = Instantiate(prefab, new Vector3(calPosX(x, y), (calPosY(x, y) - 0.25f), 0), Quaternion.identity);
                    Object.transform.parent = mapTileObject.transform;

                    SpriteRenderer render = Object.GetComponent<SpriteRenderer>();
                    render.sortingLayerName = "MapObject";
                    render.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Building);

                    this.SetChildOrder(Object, this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Building));
                }




                else
                {
                    Debug.Log("unknown id " + data + " " + x + " , " + y  );
                }
            }
        }
    }


    private void SetChildOrder(GameObject obj, int order)
    {
        SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
        render.sortingLayerName = "MapObject";
        render.sortingOrder = order;

        SpriteRenderer[] renders = obj.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer check in renders)
        {
            check.sortingLayerName = "MapObject";
            check.sortingOrder = order;
        }

    }

    private void createMapIcon(Const.Item item)
    {
        int num = this.getClearItemNumber(item);

        // アイコンの取得
        GameObject iconObject = GameObject.Find("MapIcon" + num);
        GameObject iconItemObject = iconObject.transform.Find("BackGround").gameObject;

        // アイコン
        string iconItemName = Enum.ToObject(typeof(Const.Item), item).ToString();
        Sprite spriteIconItemImage = Resources.Load("images/sugoroku/icon/icon_" + iconItemName, typeof(Sprite)) as Sprite;
        iconItemObject.GetComponent<Image>().sprite = spriteIconItemImage;

        // 所持数
        // GameObject itemCountObject = iconObject.transform.Find("ItemCount").gameObject;
        // Sprite spriteItemCountImage = Resources.Load("images/text/number/yellow/0", typeof(Sprite)) as Sprite;
        // itemCountObject.GetComponent<Image>().sprite = spriteItemCountImage;
        //this.SetItemCount( iconObject.transform.Find("Counter/Have").gameObject , "images/text/number/yellow/" , 0, false);
        // this.SetItemCount(iconObject.transform.Find("Counter/Have").gameObject, "images/text/number/white/", 0, false);

        // 必要数
        // GameObject itemNeedCountObject = iconObject.transform.Find("NeedCount").gameObject;
        // Sprite spriteItemNeedCountImage = Resources.Load("images/text/number/white/" + stageData.ClearItemCount[num], typeof(Sprite)) as Sprite;
        //itemNeedCountObject.GetComponent<Image>().sprite = spriteItemNeedCountImage;
        // this.SetItemCount(iconObject.transform.Find("Counter/Need").gameObject, "images/text/number/white/", stageData.ClearItemCount[num] , true );

        ItemCountUiPrinter printer = iconObject.GetComponent<ItemCountUiPrinter>();
        printer.Reset(stageData.ClearItemCount[num]);

    }

    /*
    private void SetItemCount( GameObject counter , string numberResoucePath , int count , bool isAlignLeft )
    {
        if (count > 99)
            count = 99;

        Image digit2 = counter.transform.Find("Digit2").gameObject.GetComponent<Image>();
        Image digit1 = counter.transform.Find("Digit1").gameObject.GetComponent<Image>();

        if (count < 10 )
        {
            if( isAlignLeft )
            {
                digit2.color = new Color(1f, 1f, 1f, 1f);
                digit1.color = new Color(1f, 1f, 1f, 0f);
                digit2.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;
            }
            else
            {
                digit2.color = new Color(1f, 1f, 1f, 0f);
                digit1.color = new Color(1f, 1f, 1f, 1f);
                digit2.sprite = Resources.Load(numberResoucePath + count / 10, typeof(Sprite)) as Sprite;
                digit1.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;
            }
        }
        else
        {
            digit2.color = new Color(1f, 1f, 1f, 1f);
            digit1.color = new Color(1f, 1f, 1f, 1f);
            digit2.sprite = Resources.Load(numberResoucePath + count / 10, typeof(Sprite)) as Sprite;
            digit1.sprite = Resources.Load(numberResoucePath + count % 10, typeof(Sprite)) as Sprite;
        
        }
    }
    */


    private GameObject createTile(int x, int y, int data)
    {
        string tileColorName;
        //string tileColorName = Enum.ToObject(typeof(Const.TileColor), data % 10).ToString();

        if( data<10)
            tileColorName = Enum.ToObject(typeof(Const.TileColor), data).ToString();
        else
            tileColorName = Enum.ToObject(typeof(Const.TileColor), data / 10 ).ToString();


        GameObject tileColorObject = new GameObject("work_tile_" + x + "_" + y);
        tileColorObject.transform.parent = mapTileObject.transform;
        Sprite spriteTileImage = Resources.Load("images/sugoroku/tile/tile_" + tileColorName, typeof(Sprite)) as Sprite;
        tileColorObject.AddComponent<SpriteRenderer>().sprite = spriteTileImage;
        tileColorObject.AddComponent<RectTransform>().anchoredPosition = new Vector3(calPosX(x, y), calPosY(x, y), 0);
        //tileColorObject.GetComponent<SpriteRenderer>().sortingLayerName = "MapTile";
        tileColorObject.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
        tileColorObject.GetComponent<SpriteRenderer>().sortingOrder = GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Tile);


        TileData tileData = tileColorObject.AddComponent<TileData>();
        tileData.SetMapIndex(x, y , this.getMapData(x,y) );

        if( this.anchorDataArray!=null)
            tileData.DebugNumber = this.anchorDataArray[x, y].RouteCount;

        TileDataIndex index = this.GetTileDataIndex(x, y);
        if (index != null)
            tileData.SetGoalDirection(index);


        if (this.isClearRouteDebug && this.clearRouteTiles != null )
        {
            foreach( TileDataIndex tileDataIndex in this.clearRouteTiles )
            { 
                if( x == tileDataIndex.MapIndexX && y==tileDataIndex.MapIndexY )
                {
                    GameObject effectObject = Instantiate(SugorokuDirector.GetInstance().Resource.PrefabTileEffect);
                    effectObject.transform.parent = tileColorObject.transform;
                    TileEffect tileEffect = effectObject.GetComponent<TileEffect>();
                    tileEffect.Initalize(tileDataIndex.ClearRouteIndex);
                    

                    tileEffect.SetSortingOrder(GetOrderInLayerForTile(x, y, MapObjectOrderIndex.TileEffect_Front), GetOrderInLayerForTile(x, y, MapObjectOrderIndex.TileEffect_Back ));

                }
                
            }
        }



        return tileColorObject;
    }

    public void CreateMapItem(int x, int y, GameObject tileColorObject, int data)
    {
        Const.Item? item = this.GetClearItem(data/10);
        if (item != null)
        {
            //Debug.Log("item " + data + " "+item.ToString());

            string itemName = item.ToString();

            string itemFilepath = "images/sugoroku/item/item_" + itemName;


            ///---------------
            GameObject itemObject = Instantiate(this.director.Resource.PrefabItemWorkBase);
            itemObject.name = "work_item_" + itemName;
            itemObject.transform.parent = tileColorObject.transform;
            itemObject.transform.localPosition = new Vector3(0, 0, 0);

            GameObject imageObject = itemObject.transform.Find("Image").gameObject;

            Sprite spriteItemImage = Resources.Load(itemFilepath, typeof(Sprite)) as Sprite;
            SpriteRenderer render = imageObject.GetComponent<SpriteRenderer>();
            render.sprite = spriteItemImage;
            render.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Item);
            itemObject.GetComponent<ItemController>().Initalize(x, y, data % 10);

            GameObject shadowObject = itemObject.transform.Find("Shadow").gameObject;
            SpriteRenderer shadowRender = shadowObject.GetComponent<SpriteRenderer>();
            shadowRender.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Shadow);

            GameObject itemCountPrintObject = itemObject.transform.Find("Image/ItemCount").gameObject;
            ItemCountPrinter itemCountPrinter = itemCountPrintObject.GetComponent<ItemCountPrinter>();
            itemCountPrinter.Initalize(data % 10);
            itemCountPrinter.SetSortingOrder(this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.ItemInformation));


            ///---------------
            /*
            GameObject itemObject = new GameObject("work_item_" + itemName);
            itemObject.transform.parent = tileColorObject.transform;
            Sprite spriteItemImage = Resources.Load(itemFilepath, typeof(Sprite)) as Sprite;

            SpriteRenderer render = itemObject.AddComponent<SpriteRenderer>();
            itemObject.AddComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            render.sprite = spriteItemImage;
            render.sortingLayerName = "MapObject";
            render.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Item);
            ItemController itemController = itemObject.AddComponent<ItemController>();
            itemController.Initalize(x, y, data % 10);

            GameObject shadowObject = Instantiate(this.director.Resource.PrefabItemShadow );
            shadowObject.transform.parent = itemObject.transform;
            shadowObject.transform.localPosition = new Vector3(0f, -0.3f, 0f);
            shadowObject.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
            SpriteRenderer shadowRender = shadowObject.GetComponent<SpriteRenderer>();
            shadowRender.sortingOrder = this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.Shadow);
            shadowRender.sortingLayerName = "MapObject";



            GameObject itemCountPrintObject = Instantiate(SugorokuDirector.GetInstance().Resource.PrefabItemCountPrinter  );
            itemCountPrintObject.transform.parent = itemObject.transform;
            itemCountPrintObject.transform.localPosition = new Vector3(0, 0, 0);
            ItemCountPrinter itemCountPrinter = itemCountPrintObject.GetComponent<ItemCountPrinter>();
            itemCountPrinter.Initalize(data % 10);
            itemCountPrinter.SetSortingOrder(this.GetOrderInLayerForTile(x, y, MapObjectOrderIndex.ItemInformation ) );
            */
            SugorokuDirector.GetInstance().AddItem(tileColorObject);

            
        }
        else
        {
            Debug.Log("item unknown " + data);
        }
    }


    public int getClearItemNumber(Const.Item item)
    {
        for (int i = 0; i < stageData.ClearItem.Length; ++i)
        {
            if (item == stageData.ClearItem[i])
            {
                return i;
            }
        }
        return -1;
    }

    public Const.Item? GetClearItem(int num)
    {
        if (num >= 1 && num <= 4)
        {
            return stageData.ClearItem[num - 1];
        }
        return null;
    }

    public bool isClear(int[] itemCount)
    {
        for (int i = 0; i < stageData.ClearItemCount.Length; ++i)
        {
            if (stageData.ClearItemCount[i] > itemCount[i])
            {
                // not clear
                return false;
            }
        }

        // clear
        return true;
    }

    public int getClearStartNum(int[] itemCount)
    {
        int starNum = itemCount[stageData.ExtraClearItemNo()] - stageData.ClearItemCount[stageData.ExtraClearItemNo()] + 1;
        if (starNum > 3)
        {
            return 3;
        }
        return starNum;
    }

    public int getMapData(int x, int y)
    {
        return this.mapData[y].fieldDataXList[x];
        //return stageData.FieldDataYList[y].fieldDataXList[x];
    }


    public void setMapData( int x , int y , int tileID )
    {
        this.mapData[y].fieldDataXList[x] = tileID;

        if( this.anchorDataArray != null )
        {
            this.anchorDataArray[x,y].PassableRefresh();
            if( x > 0 )
                this.anchorDataArray[x-1, y].PassableRefresh();
            if (x < TileXMax-1)
                this.anchorDataArray[x + 1, y].PassableRefresh();
            if( y > 0 )
                this.anchorDataArray[x , y-1].PassableRefresh();
            if (y < TileYMax-1)
                this.anchorDataArray[x, y + 1].PassableRefresh();
        }
        //stageData.FieldDataYList[y].fieldDataXList[x] = tileID;
    }

    private int GetMasu(int x, int y)
    {
        return GetMasu(x, y, 0, 0);
    }

    public int getMapData(int x, int y, Const.Direction dir)
    {
        if (Const.Direction.up == dir)
        {
            return GetMasu(x, y, 0, -1);
        }
        else if (Const.Direction.right == dir)
        {
            return GetMasu(x, y, 1, 0);
        }
        else if (Const.Direction.down == dir)
        {
            return GetMasu(x, y, 0, 1);
        }
        else if (Const.Direction.left == dir)
        {
            return GetMasu(x, y, -1, 0);
        }
        else
        {
            return GetMasu(x, y);
        }
    }

    private int GetMasu(int x, int y, int dx, int dy)
    {
        return getMapData((x + dx), (y + dy));
    }

    public static Vector2 calPos(int x, int y)
    {
        return new Vector2(calPosX(x, y), calPosY(x, y));
    }

    public static Vector2 calPosPlayer(int x, int y, Vector2 adjust)
    {
        return new Vector2(calPosX(x, y) + adjust.x, calPosY(x, y) + adjust.y);
    }

    public int GetOrderInLayerForTile(int x, int y, MapObjectOrderIndex ordeIndex)
    {
        //return (MAX_X - x) + y;
        return ((TileXMax - x) + y) * (int)MapObjectOrderIndex.Max + (int)ordeIndex;
    }

    private static float calPosX(int x, int y)
    {
        return ((x - HOME_X) * POS_X) + ((y - HOME_Y) * POS_X);
    }

    private static float calPosY(int x, int y)
    {
        return ((x - HOME_X) * POS_Y) + (-(y - HOME_Y) * POS_Y);
    }

    public enum MapObjectOrderIndex
    {
        Background = 0,
        Building = 1,
        TileEffect_Back = 2 ,
        Tile = 3,
        TilePattern = 4,
        Shadow = 5,

        Enemy = 6,
        Player = 7,
        Item = 8,
        ItemInformation = 9,
        TileEffect_Front  = 10,
        Max = 10,
    }


    public static bool GetIsRouteMasu( int masuID)
    {
        if (masuID == 0)
            return false;

        if (masuID >= 100)
            return false;

        if (masuID == MapController.TileID_RandomTile)
            return false;

        return true;
    }

    private TileDataIndex tileGoal;
    private TileDataIndex tileTop;
    private TileDataIndex tileLeft;
    private TileDataIndex tileBottom;
    private TileDataIndex tileRight;
    private List<TileDataIndex> randomTiles;

    private TileDataIndex clearRouteLastTile;
    private List<TileDataIndex> clearRouteTiles;


    private List<TileDataIndex> itemCandidateTilesClearRoute;
    private List<TileDataIndex> itemCandidateTilesLv1;
    private List<TileDataIndex> itemCandidateTilesLv2;
    private List<TileDataIndex> itemCandidateTilesLv3;

    private int itemCandidateTileIndexLv1;
    private int itemCandidateTileIndexLv2;
    private int itemCandidateTileIndexLv3;
    private int clearRouteTileIndex;

    private int clearRoutePutItemCount;

    private int moveRequestX, moveRequestY;

    private bool isRandomRotateLeft;

    private int randomTileLastCount = 50;

    // private int RandomTile_RandomItemCount = 3;
    // private int RandomTile_SpecialCount = 2;
    // private int RandomTile_Enemy = 1;

    //private int ItemCountMaxInTile = 5;

    private Boolean isClearRouteDebug = false;
    private RandomMapTileAnchorData[,] anchorDataArray;

    private void SetItemTileForClearRoute( int itemBaseIndex , int itemCount)
    {
        int countSum = 0;
        
        while( countSum < itemCount )
        {
            int count = UnityEngine.Random.Range(1, this.stageData.RandomMapItemCountMaxInTile + 1);
            countSum += count;

            this.SetTileForClearRoute(itemBaseIndex + count );
        }

    }

    private bool CreateRandomMap()
    {
        this.randomTiles = new List<TileDataIndex>();

        this.itemCandidateTilesClearRoute = new List<TileDataIndex>();
        this.itemCandidateTilesLv1 = new List<TileDataIndex>();
        this.itemCandidateTilesLv2 = new List<TileDataIndex>();
        this.itemCandidateTilesLv3 = new List<TileDataIndex>();

        // ランダム用のマスがあるか？
        SearchRandomTiles();

        if( this.randomTiles.Count <= 0  )
        {
            // ランダム用のタイルがないのでここで終了
            this.director.AddDebugText("ランダムマス無し");
            return true;
        }

        this.director.AddDebugText("ランダムマス " + this.randomTiles.Count);



        // データの中にゴールがあるか
        if ( !this.SearchGoal() )
        {
            this.CreateGoalForRandomTile();
            // ゴールがないので ランダムマスの中からゴールを作成

            this.director.AddDebugText("ランダムマスの中からゴールを作成 " + this.tileGoal.MapIndexX + " , " + this.tileGoal.MapIndexY );
        }


        //RandomMapTileAnchorData[,] anchorDataArray = new RandomMapTileAnchorData[this.TileXMax, this.TileYMax];
        this.anchorDataArray = new RandomMapTileAnchorData[this.TileXMax, this.TileYMax];

        for (int checkX = 0; checkX < this.TileXMax; checkX++)
        {
            for (int checkY = 0; checkY < this.TileYMax; checkY++)
            {
                anchorDataArray[checkX, checkY] = new RandomMapTileAnchorData(checkX, checkY, this);
                anchorDataArray[checkX, checkY].PassableRefresh();
            }
        }


        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.InitalizePosition(this.tileGoal.MapIndexX, this.tileGoal.MapIndexY);

        this.isRandomRotateLeft = true;

        int beforeCount = randomTileLastCount;

        int lastRetryCount = 5;


        //if (randomTileLastCount > 0)
        {

            List<RandomMapTileAnchorRequest> anchorRequestList = new List<RandomMapTileAnchorRequest>();

            while (randomTileLastCount > 0)
            {
                beforeCount = randomTileLastCount;

                anchorRequestList.Clear();

                // 四隅のタイルを探す
                this.SearchAnchorTile(anchorRequestList);

                this.ListRandomizer(anchorRequestList);

                for ( int reqID= 0; reqID<Mathf.Min(this.stageData.RandomMapBranchDensity, anchorRequestList.Count); reqID++)
                {
                    RandomMapTileAnchorRequest anchorRequest = anchorRequestList[reqID];
                    this.CreateRandomRoute(anchorRequest.DataIndex, anchorRequest.Direction);
                }


                /*
                foreach (RandomMapTileAnchorRequest anchorRequest in anchorRequestList)
                {
                    this.CreateRandomRoute(anchorRequest.DataIndex, anchorRequest.Direction);
                }
                */

                //aaa
                //randomTileLastCount = 0;
                /*
                this.CreateRandomRoute(this.tileTop, Const.Direction.up);
                this.CreateRandomRoute(this.tileBottom, Const.Direction.down);
                this.CreateRandomRoute(this.tileLeft, Const.Direction.left);
                this.CreateRandomRoute(this.tileRight, Const.Direction.right);
                */

                if ( beforeCount == randomTileLastCount)
                {
                    lastRetryCount--;
                    if (lastRetryCount <= 0)
                        break;
                }
            }
        }


        if ( !this.ClearRouteSearch() )
        {
            this.director.AddDebugText("クリアルート 探索失敗");
            //return false;
        }
        else
            this.director.AddDebugText("クリアルート " + this.clearRouteTiles.Count + "マス");


        if (this.isClearRouteDebug)
        {
            for (int clearRouteIndex = 0; clearRouteIndex < this.clearRouteTiles.Count; clearRouteIndex++)
            {
                this.clearRouteTiles[clearRouteIndex].ClearRouteIndex = clearRouteIndex + 1;
            }
        }


        this.TileDataIndexRandomizer(this.clearRouteTiles);

        //int count = 0;

        /*
        for (int id = 0; id < 3; id++)
            this.SetTileForClearRoute((this.stageData.ExtraClearItemNo() + 1) * 10);
        for (int id = 0; id < this.stageData.ClearItemCount[0]; id++)
            this.SetTileForClearRoute(10);
        for (int id = 0; id < this.stageData.ClearItemCount[1]; id++)
            this.SetTileForClearRoute(20);
        for (int id = 0; id < this.stageData.ClearItemCount[2]; id++)
            this.SetTileForClearRoute(30);
        for (int id = 0; id < this.stageData.ClearItemCount[3]; id++)
            this.SetTileForClearRoute(40);
        */


        List<TileDataIndex> tempList = null;

        for ( int clearRouteTileID=0; clearRouteTileID<this.clearRouteTiles.Count; clearRouteTileID++ )
        {
            /*
            bool isAlready = false;
            for( int checkID=0; checkID<this.itemCandidateTilesClearRoute.Count; checkID++)
            {
                if (this.itemCandidateTilesClearRoute[checkID].GetIsSameTile(this.clearRouteTiles[clearRouteTileID]))
                    isAlready = true;
            }

            if (!isAlready)
                this.itemCandidateTilesClearRoute.Add(this.clearRouteTiles[clearRouteTileID]);
            */

            bool isAlready = false;
            
            tempList = this.itemCandidateTilesLv1;
            for ( int checkID=tempList.Count-1; checkID>=0; checkID-- )
            {
                TileDataIndex tempData = tempList[checkID];

                if( tempData.GetIsSameTile(this.clearRouteTiles[clearRouteTileID]))
                {
                    tempList.RemoveAt(checkID);
                }
            }

            tempList = this.itemCandidateTilesLv2;
            for (int checkID = tempList.Count - 1; checkID >= 0; checkID--)
            {
                TileDataIndex tempData = tempList[checkID];

                if (tempData.GetIsSameTile(this.clearRouteTiles[clearRouteTileID]))
                {
                    tempList.RemoveAt(checkID);
                }
            }

            tempList = this.itemCandidateTilesLv3;
            for (int checkID = tempList.Count - 1; checkID >= 0; checkID--)
            {
                TileDataIndex tempData = tempList[checkID];

                if (tempData.GetIsSameTile(this.clearRouteTiles[clearRouteTileID]))
                {
                    tempList.RemoveAt(checkID);
                }
            }
            

        }

        this.TileDataIndexRandomizer(this.itemCandidateTilesClearRoute);
        this.TileDataIndexRandomizer(this.itemCandidateTilesLv1);
        this.TileDataIndexRandomizer(this.itemCandidateTilesLv2);
        this.TileDataIndexRandomizer(this.itemCandidateTilesLv3);


        this.clearRoutePutItemCount = 0;

        this.SetItemTileForClearRoute((this.stageData.ExtraClearItemNo() + 1) * 10, 3);
        this.SetItemTileForClearRoute(10, this.stageData.ClearItemCount[0]);
        this.SetItemTileForClearRoute(20, this.stageData.ClearItemCount[1]);
        this.SetItemTileForClearRoute(30, this.stageData.ClearItemCount[2]);
        this.SetItemTileForClearRoute(40, this.stageData.ClearItemCount[3]);


        this.director.AddDebugText("クリアルート内 アイテム設置数 " + this.clearRoutePutItemCount );
        this.director.AddDebugText("アイテム密度 " + (int)( (float)this.clearRoutePutItemCount / (float)this.itemCandidateTilesClearRoute.Count * 100f ) + "%" );



        this.itemCandidateTileIndexLv1 = 0;
        this.itemCandidateTileIndexLv2 = 0;
        this.itemCandidateTileIndexLv3 = 0;
        this.clearRouteTileIndex = 0;


        //for (int id = 0; id < RandomTile_SpecialCount; id++)
        for (int id = 0; id < this.stageData.RandomMapSpecailCount ; id++)
            this.SetTileForRandom(7);
        //for ( int id=0; id< RandomTile_RandomItemCount; id++ )
        for (int id = 0; id < this.stageData.RandomMapRandomItemCount; id++)
            this.SetTileForRandom( 9 );
        //for (int id = 0; id < RandomTile_Enemy; id++)
        for (int id = 0; id < this.stageData.RandomMapEnemyCount; id++)
            this.SetTileForRandom(5);



        /*
        for (int id = 0; id < 3 ; id++)
            this.SetTileForRandom(this.stageData.ExtraClearItemNo() + 1);
 
        for (int id = 0; id < this.stageData.ClearItemCount[0]; id++)
            this.SetTileForRandom(1);
        for (int id = 0; id < this.stageData.ClearItemCount[1]; id++)
            this.SetTileForRandom(2);
        for (int id = 0; id < this.stageData.ClearItemCount[2]; id++)
            this.SetTileForRandom(3);
        for (int id = 0; id < this.stageData.ClearItemCount[3]; id++)
            this.SetTileForRandom(4);
        */


        // 残っているところに適当にアイテムを埋める
        foreach ( TileDataIndex check in this.itemCandidateTilesLv3 )
        {
            //if (UnityEngine.Random.Range(0, this.clearRouteTiles.Count) > clearRouteItemCount)
            if (UnityEngine.Random.Range(0, this.itemCandidateTilesClearRoute.Count) > this.clearRoutePutItemCount)
                continue;

            //this.clearRouteTileMax

            int tileIndex = this.getMapData(check.MapIndexX, check.MapIndexY);
            if (tileIndex != 1 && tileIndex != 8)
                continue;

            //Debug.Log("abc " + itemCandidateTilesLv3.Count);

            int itemID = 1;
            int seed = UnityEngine.Random.Range(0, 10);

            int itemCount = UnityEngine.Random.Range(1, this.stageData.RandomMapItemCountMaxInTile + 1);

            if (seed == 1) itemID = 10 + itemCount;
            else if (seed == 2) itemID = 20 + itemCount;
            else if (seed == 3) itemID = 30 + itemCount;
            else if (seed == 4) itemID = 40 + itemCount;
            else if (seed == 5) itemID = 10 + itemCount;
            else if (seed == 6) itemID = 20 + itemCount;
            else if (seed == 7) itemID = 30 + itemCount;
            else if (seed == 8) itemID = 40 + itemCount;
            else if (seed == 9) itemID = 9;
            else
                continue;

            this.setMapData(check.MapIndexX, check.MapIndexY, itemID);
        }


        // タイルオブジェクトを配置
        List<TileDataIndex> freeTiles = new List<TileDataIndex>();

        for (int i = 0; i < TileXMax; ++i)
        {
            for (int j = 0; j < TileYMax; ++j)
            {
                int checkID = this.getMapData(i, j);

                if (checkID == MapController.TileID_RandomTile )
                    freeTiles.Add(new TileDataIndex(i, j, checkID));
            }
        }

        this.TileDataIndexRandomizer( freeTiles );

        int freeTileIndex = 0;

        foreach( MapObjectParameter param in this.stageData.RandomSpotsObjects )
        {
            for( int count=0; count<param.Count; count++ )
            {
                if (freeTileIndex >= freeTiles.Count)
                    break;

                this.setMapData(freeTiles[freeTileIndex].MapIndexX, freeTiles[freeTileIndex].MapIndexY, param.Code);

                freeTileIndex++;
            }
        }

        foreach (MapObjectClusterParameter cluster in this.stageData.RandomClusterObjects)
        {

            for( int count=0; count<cluster.Count; count++ )
            {

                if (freeTileIndex >= freeTiles.Count)
                    break;

                TileDataIndex centerTile = freeTiles[freeTileIndex ++ ];


                for( int indexX = Math.Max(0, centerTile.MapIndexX-cluster.Radius ); indexX <= Math.Min(centerTile.MapIndexX + cluster.Radius,TileXMax-1); indexX++ )
                { 
                    for (int indexY = Math.Max(0, centerTile.MapIndexY - cluster.Radius); indexY <= Math.Min(centerTile.MapIndexY + cluster.Radius, TileYMax-1); indexY++)
                    {
                        this.SetRandomMapClusterItem(cluster, indexX , indexY );
                    }
                }
            }
        }

        return true;
    }

    private int CalcScattering( int scattering )
    {
        return UnityEngine.Random.Range(0, scattering );
    }

    private void SetRandomMapClusterItem( MapObjectClusterParameter cluster , int indexX , int indexY )
    {

        if (indexX < 0 || indexX>=TileXMax || indexY < 0 || indexY >= TileYMax)
            return;

        if (this.getMapData(indexX, indexY) != 8)
            return;

        int ratioSeedSum = 0;

        foreach (MapObjectClusterItemParameter item in cluster.Items)
        {
            ratioSeedSum += item.Ratio;
        }

        int seed = UnityEngine.Random.Range(0, ratioSeedSum + 1);
        int selectedCode = 0;
        int checkRatio = 0;

        foreach (MapObjectClusterItemParameter item in cluster.Items)
        {
            checkRatio += item.Ratio;
            if (seed <= checkRatio)
            {
                selectedCode = item.Code;
                break;
            }
        }

        this.setMapData(indexX, indexY , selectedCode);

    }


    private void CreateRandomRoute(TileDataIndex dataIndex, Const.Direction initDir )
    {

        Const.Direction dir = initDir;

        /*
        int seed = UnityEngine.Random.Range(0, 4);
        if (seed == 0)
            dir = Const.Direction.up;
        else if (seed == 1)
            dir = Const.Direction.down;
        else if (seed == 2)
            dir = Const.Direction.left;
        else
            dir = Const.Direction.right;
        */

        if (UnityEngine.Random.Range(0, 2) == 0)
            this.isRandomRotateLeft = true;
        else
            this.isRandomRotateLeft = false;


        int rotateMax = UnityEngine.Random.Range(3, 6);

        for ( int rotateID = 1; rotateID <= rotateMax && this.randomTileLastCount>0; rotateID++)
        {
            int count;

            if(this.stageData.RandomMapStraightDistanceMin< this.stageData.RandomMapStraightDistanceMax )
                count = UnityEngine.Random.Range(this.stageData.RandomMapStraightDistanceMin, this.stageData.RandomMapStraightDistanceMax+1);
            else
                count = UnityEngine.Random.Range(this.stageData.RandomMapStraightDistanceMin, this.stageData.RandomMapStraightDistanceMin+1);

            /*
            if (UnityEngine.Random.Range(0, 8) == 0)
                count = 2;
            else
                count = UnityEngine.Random.Range(3, 5);
            */

            for (int createID = 0; createID < count && this.randomTileLastCount > 0; createID++)
            {
                if (!this.CreateRandomTile(dataIndex, dir))
                    break;

            }

            if (!this.isRandomRotateLeft)
            {
                switch (dir)
                {
                    case Const.Direction.up: dir = Const.Direction.left; break;
                    case Const.Direction.left: dir = Const.Direction.down; break;
                    case Const.Direction.down: dir = Const.Direction.right; break;
                    case Const.Direction.right: dir = Const.Direction.up; break;
                }
            }
            else
            {
                switch (dir)
                {
                    case Const.Direction.up: dir = Const.Direction.right; break;
                    case Const.Direction.right: dir = Const.Direction.down; break;
                    case Const.Direction.down: dir = Const.Direction.left; break;
                    case Const.Direction.left: dir = Const.Direction.up; break;
                }
            }

            this.itemCandidateTilesLv2.Add(new TileDataIndex(dataIndex.MapIndexX, dataIndex.MapIndexY));

        }


        this.itemCandidateTilesLv1.Add(new TileDataIndex(dataIndex.MapIndexX, dataIndex.MapIndexY));


        if (UnityEngine.Random.Range(0, 2) == 0)
            this.isRandomRotateLeft = !this.isRandomRotateLeft;
    }


    private bool SearchGoal()
    {
        for (int x = 0; x < TileXMax; ++x)
        {
            for (int y = 0; y < TileYMax; ++y)
            {
                if (this.getMapData(x, y) == -1)
                {

                    //this.tileGoal = new TileDataIndex( x,y);
                    this.tileGoal = this.GetTileDataIndex(x, y);
                    return true;
                }
            }
        }
        return false;
    }
    
    private void SearchAnchorTile(List<RandomMapTileAnchorRequest> anchorRequestList )
    {
        // this.tileTop = new TileDataIndex(this.tileGoal);
        // this.tileLeft = new TileDataIndex(this.tileGoal);
        // this.tileBottom = new TileDataIndex(this.tileGoal);
        // this.tileRight = new TileDataIndex(this.tileGoal);

        this.tileTop = null;
        this.tileLeft = null;
        this.tileBottom = null;
        this.tileRight = null;


        RandomMapTileAnchorData tempAnchor;

        //if( UnityEngine.Random.Range(0,2)==0 )
        { 
            tempAnchor = null;
            for (int y = 0; y < TileYMax && tempAnchor == null; y++)
            {
                for (int x = 0; x <= this.tileGoal.MapIndexX  ; x++)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y))) 
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if( check.IsPassableTop )
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if( check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2 )
                                tempAnchor = check;
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY),Const.Direction.up));
            }


            tempAnchor = null;
            for (int y = 0; y < TileYMax && tempAnchor == null; y++)
            {
                for (int x = TileXMax-1; x > this.tileGoal.MapIndexX; x--)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y)))
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if (check.IsPassableTop)
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if (check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2)
                                tempAnchor = check;
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY), Const.Direction.up));
            }

            tempAnchor = null;
            for (int y = TileYMax-1; y >=0 && tempAnchor == null; y--)
            {
                for (int x = 0; x <= this.tileGoal.MapIndexX ; x++)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y)))
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if (check.IsPassableBottom)
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if (check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2)
                                tempAnchor = check;
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY), Const.Direction.down));
            }

            tempAnchor = null;
            for (int y = TileYMax - 1; y >= 0 && tempAnchor == null; y--)
            {
                for (int x = TileXMax - 1; x > this.tileGoal.MapIndexX; x--)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y)))
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if (check.IsPassableBottom)
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if (check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2)
                                tempAnchor = check;
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY), Const.Direction.down));
            }

        }
        //else
        { 
            tempAnchor = null;
            for (int x = 0; x < TileXMax && tempAnchor == null; x++) 
            {
                for (int y = 0; y <= this.tileGoal.MapIndexY ; y++)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y)))
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if (check.IsPassableLeft)
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if (check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2)
                                tempAnchor = check;
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY), Const.Direction.left));
            }


            tempAnchor = null;
            for (int x = 0; x < TileXMax && tempAnchor == null; x++)
            {
                for (int y = TileYMax-1 ; y > this.tileGoal.MapIndexY ; y--)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y)))
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if (check.IsPassableLeft)
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if (check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2)
                                tempAnchor = check;
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY), Const.Direction.left));
            }


            tempAnchor = null;
            for (int x = TileXMax-1; x >=0  && tempAnchor == null; x--)
            {
                for (int y = 0; y < this.tileGoal.MapIndexY; y++)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y)))
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if (check.IsPassableRight)
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if (check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2)
                            {
                                tempAnchor = check;
                            }
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY), Const.Direction.right));
            }


            tempAnchor = null;
            for (int x = TileXMax - 1; x >= 0 && tempAnchor == null; x--)
            {
                for (int y = TileYMax - 1; y > this.tileGoal.MapIndexY; y--)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(x, y)))
                    {
                        RandomMapTileAnchorData check = this.anchorDataArray[x, y];

                        if (check.IsPassableRight)
                        {
                            if (tempAnchor == null)
                                tempAnchor = check;
                            else if (check.RouteCount < tempAnchor.RouteCount && check.RouteCount != 2)
                            {
                                tempAnchor = check;
                            }
                        }
                    }
                }
            }
            if (tempAnchor != null)
            {
                anchorRequestList.Add(new RandomMapTileAnchorRequest(new TileDataIndex(tempAnchor.IndexX, tempAnchor.IndexY), Const.Direction.right));
            }
        }


        /*
        for (int x = 0; x < TileXMax; ++x)
        {
            for (int y = 0; y < TileYMax; ++y)
            {
                if( MapController.GetIsRouteMasu(this.getMapData(x, y)))
                {
                    if (this.tileTop.MapIndexY >= y )
                    {
                        this.tileTop.MapIndexY = y;
                        this.tileTop.MapIndexX = x;
                    }

                    if (this.tileLeft.MapIndexX >= x)
                    {
                        this.tileLeft.MapIndexX = x;
                        this.tileLeft.MapIndexY = y;
                    }

                    if (this.tileBottom.MapIndexY <= y )
                    {
                        this.tileBottom.MapIndexY = y;
                        this.tileBottom.MapIndexX = x;
                    }

                    if (this.tileRight.MapIndexX <= x)
                    {
                        this.tileRight.MapIndexY = y;
                        this.tileRight.MapIndexX = x;
                    }
                }
            }
        }
        */

    }

    private void SearchRandomTiles()
    {
        for (int x = 0; x < TileXMax; ++x)
        {
            for (int y = 0; y < TileYMax; ++y)
            {
                if (this.getMapData(x, y) == MapController.TileID_RandomTile)
                {
                    this.randomTiles.Add(new TileDataIndex(x, y));
                }
            }
        }
    }

    private void CreateGoalForRandomTile()
    {
        TileDataIndex goalTile = this.randomTiles[UnityEngine.Random.Range(0, this.randomTiles.Count)];

        for ( int retryCount= 0; retryCount < 100; retryCount++)
        {

            if (goalTile.MapIndexX <= 2 || goalTile.MapIndexX >= TileXMax - 3 || goalTile.MapIndexY <= 2 || goalTile.MapIndexY >= TileYMax - 3)
                goalTile = this.randomTiles[UnityEngine.Random.Range(0, this.randomTiles.Count)];
            else
                break;
        }


        int x = goalTile.MapIndexX;
        int y = goalTile.MapIndexY;

        this.tileGoal = new TileDataIndex(x, y);
        this.setMapData(x, y , -1);

    }

    private bool CreateRandomTile( TileDataIndex dataIndex , Const.Direction dir )
    {
        int nextX = dataIndex.MapIndexX;
        int nextY = dataIndex.MapIndexY;

        switch( dir )
        {
            case Const.Direction.up:
                nextY--;
                break;
            case Const.Direction.down:
                nextY++;
                break;
            case Const.Direction.left:
                nextX--;
                break;
            case Const.Direction.right:
                nextX++;
                break;
        }

        if ( nextX<TileXMax-2 && nextX>=2 && nextY<TileYMax-2 && nextY>=2 && this.getMapData(nextX, nextY) == MapController.TileID_RandomTile)
        {
            bool isCanCreate = true;

            if( dir == Const.Direction.left || dir == Const.Direction.right )
            {
                for (int check = 1; check <= 1; check++)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(nextX, nextY - check)))
                        isCanCreate = false;
                    if (MapController.GetIsRouteMasu(this.getMapData(nextX, nextY + check)))
                        isCanCreate = false;
                }
            }
            else if (dir == Const.Direction.up || dir == Const.Direction.down)
            {
                for (int check = 1; check <= 1; check++)
                {
                    if (MapController.GetIsRouteMasu(this.getMapData(nextX - check, nextY)))
                        isCanCreate = false;
                    if (MapController.GetIsRouteMasu(this.getMapData(nextX + check, nextY)))
                        isCanCreate = false;
                }
            }

            if( isCanCreate )
            {
                this.setMapData(nextX, nextY , 1);
                dataIndex.SetMapIndex(nextX, nextY);
                this.randomTileLastCount --;

                this.itemCandidateTilesLv3.Add(new TileDataIndex(dataIndex.MapIndexX, dataIndex.MapIndexY));

                return true;
            }
        }

        return false;
    }


    private void SetTileForRandom( int id )
    {
        TileDataIndex check = null;

        do
        {
            check = null;
            if (this.itemCandidateTileIndexLv1 < this.itemCandidateTilesLv1.Count)
            {
                check = this.itemCandidateTilesLv1[this.itemCandidateTileIndexLv1];

                this.itemCandidateTileIndexLv1++;

            }
            else if (this.itemCandidateTileIndexLv2 < this.itemCandidateTilesLv2.Count)
            {
                check = this.itemCandidateTilesLv2[this.itemCandidateTileIndexLv2];

                this.itemCandidateTileIndexLv2++;

            }
            else if (this.itemCandidateTileIndexLv3 < this.itemCandidateTilesLv3.Count)
            {
                check = this.itemCandidateTilesLv3[this.itemCandidateTileIndexLv3];

                this.itemCandidateTileIndexLv3++;

            }

            if (check != null)
            {
                int tileIndex = this.getMapData(check.MapIndexX, check.MapIndexY);
                if (tileIndex != 1 && tileIndex != MapController.TileID_RandomTile )
                    continue;
                else
                    break;
            }
        } while (check != null);

        if (check != null)
            this.setMapData(check.MapIndexX, check.MapIndexY, id);

    }


    private void SetTileForClearRoute(int id)
    {
        TileDataIndex check = null;

        do
        {
            check = null;

            if (this.clearRouteTileIndex < this.itemCandidateTilesClearRoute.Count)
            {
                check = this.itemCandidateTilesClearRoute[this.clearRouteTileIndex];

                this.clearRouteTileIndex++;

            }

            if (check != null)
            {
                int tileIndex = this.getMapData(check.MapIndexX, check.MapIndexY);
                if (tileIndex != 1 && tileIndex != MapController.TileID_RandomTile)
                    continue;
                else
                    break;
            }
        } while (check != null);

        if (check != null)
        {
            this.setMapData(check.MapIndexX, check.MapIndexY, id);
            this.clearRoutePutItemCount++;
        }

    }

    private void TileDataIndexRandomizer(List<TileDataIndex> items)
    {
        for (int id = items.Count - 1; id >= 0; id--)
        {
            int moveIndex = UnityEngine.Random.Range(0, items.Count);
            TileDataIndex temp = items[moveIndex];
            items[moveIndex] = items[id];
            items[id] = temp;
        }
    }

    /*
    private void AnchorRequestRandomizer(List<RandomMapTileAnchorRequest> items)
    {
        for (int id = items.Count - 1; id >= 0; id--)
        {
            int moveIndex = UnityEngine.Random.Range(0, items.Count);
            RandomMapTileAnchorRequest temp = items[moveIndex];
            items[moveIndex] = items[id];
            items[id] = temp;
        }
    }
    */

    public void ListRandomizer<Type>(List<Type> items) 
    {
        for (int id = items.Count - 1; id >= 0; id--)
        {
            int moveIndex = UnityEngine.Random.Range(0, items.Count);
            Type temp = items[moveIndex];
            items[moveIndex] = items[id];
            items[id] = temp;
        }
    }

    //private int clearRouteTileMax = 35;

    //private int clearRouteTileMax = 50;
    //private int clearRouteItemCount = 10; //15

    private bool CheckNextClearRoute( int nextX , int nextY )
    {
        if (nextX < 0 || nextX >= TileXMax)
            return false;
        if (nextY < 0 || nextY >= TileYMax)
            return false;

        /*
        if( this.clearRouteLastTile!=null)
        {
            if (this.clearRouteLastTile.MapIndexX == nextX && this.clearRouteLastTile.MapIndexY == nextY)
                return false;
        }
        */

        if (!MapController.GetIsRouteMasu(this.getMapData(nextX, nextY)))
            return false;

        return true;
    }




    private bool ClearRouteSearch()
    {
        int checkCount = 0;

        int clearRouteTileCount = 0;

        while (checkCount < 1000)
        {
            clearRouteTileCount = 0;

            this.clearRouteTiles = new List<TileDataIndex>();
            this.itemCandidateTilesClearRoute.Clear();



            TileDataIndex tileNow = new TileDataIndex(this.tileGoal);
            tileNow.SetGoalDirection(0, 0, 0);


            this.clearRouteLastTile = new TileDataIndex(tileNow);

            TileDataIndex temp = this.GetTileDataIndex(tileNow.MapIndexX, tileNow.MapIndexY);
            temp.SetGoalDirection(0, 0, 0);

            TileDataIndex tileNext = new TileDataIndex( this.ClearRouteSearchDirection(tileNow));

            temp = this.GetTileDataIndex(tileNext.MapIndexX, tileNext.MapIndexY);
            temp.GoalDistance = 1;
            temp.GoalDirectionX = tileNow.MapIndexX - tileNext.MapIndexX;
            temp.GoalDirectionY = tileNow.MapIndexY - tileNext.MapIndexY;



            if (tileNext == null)
                return false;

            checkCount++;


            //while (clearRouteTileCount < this.clearRouteTileMax)
            while (clearRouteTileCount < this.stageData.RandomMapClearRouteDistanceMax )
            {

                this.clearRouteLastTile.SetMapIndex(tileNow);
                tileNow.SetMapIndex(tileNext);



                //---
                // 同じところをグルグル回っていたら その部分を削除
                /*
                for( int routeTileID=this.clearRouteTiles.Count-1;routeTileID>=0;routeTileID--)
                {
                    TileDataIndex checkRouteTile = this.clearRouteTiles[routeTileID];

                    if (checkRouteTile.MapIndexX == tileNow.MapIndexX && checkRouteTile.MapIndexY == tileNow.MapIndexY)
                    {
                        if (this.stageData.RandomMapClearRouteTileCountMin < routeTileID)
                        {
                            for (int removeID = this.clearRouteTiles.Count - 1; removeID >= routeTileID; removeID--)
                            { 
                                this.clearRouteTiles.RemoveAt(removeID);
                                clearRouteTileCount--;
                            }
                        }
                        break;
                    }
                }
                */

                /*
                bool isAlreadyAdd = false;
                for (int routeTileID = this.clearRouteTiles.Count - 1; routeTileID >= 0; routeTileID--)
                {
                    TileDataIndex checkRouteTile = this.clearRouteTiles[routeTileID];
                    if (checkRouteTile.GetIsSameTile(tileNow))
                        isAlreadyAdd = true;
                }

                if ( !isAlreadyAdd )
                {
                    this.clearRouteTiles.Add(new TileDataIndex(tileNow));
                }
                */
                clearRouteTileCount++;
                this.clearRouteTiles.Add(new TileDataIndex(tileNow));

                bool isAlready = false;
                for (int checkID = 0; checkID < this.itemCandidateTilesClearRoute.Count; checkID++)
                {
                    if (this.itemCandidateTilesClearRoute[checkID].GetIsSameTile(tileNow))
                        isAlready = true;
                }

                if (!isAlready)
                    this.itemCandidateTilesClearRoute.Add(new TileDataIndex(tileNow));


                //-----

                //if (this.clearRouteItemCount < this.clearRouteTiles.Count && tileNow.GetIsSameTile(this.tileGoal))
                if (this.stageData.RandomMapClearRouteTileCountMin < this.itemCandidateTilesClearRoute.Count && tileNow.GetIsSameTile(this.tileGoal))
                {
                    Debug.Log("clear route goal! " + tileGoal.MapIndexX + "," + tileGoal.MapIndexY);

                    this.director.AddDebugText("クリア探索リトライ回数" + checkCount);
                    this.director.AddDebugText("クリアルート距離 " + this.clearRouteTiles.Count);
                    this.director.AddDebugText("クリアルートタイル " + this.itemCandidateTilesClearRoute.Count);
                    return true;
                }
                else
                {
                    Debug.Log("clear route search ... check " + checkCount + " routeTile " + clearRouteTileCount + " useTile " + this.clearRouteTiles.Count + "  | " + tileNow.MapIndexX + "," + tileNow.MapIndexY);
                }

                int nextX = tileNow.MapIndexX + tileNow.MapIndexX - this.clearRouteLastTile.MapIndexX;
                int nextY = tileNow.MapIndexY + tileNow.MapIndexY - this.clearRouteLastTile.MapIndexY;

                TileDataIndex dataNow = this.GetTileDataIndex(tileNow.MapIndexX, tileNow.MapIndexY);

                // todo 直進もできるけど分岐できる場合 aaa
                /*
                if (this.CheckNextClearRoute(nextX, nextY))
                {
                    // 直進
                    tileNext.SetMapIndex(nextX, nextY);
                }
                else
                {
                    if(this.stageData.RandomMapClearRouteTileCountMin <= this.clearRouteTiles.Count && (tileNow.GoalDirectionX != 0 || tileNow.GoalDirectionY != 0) )
                    {
                        nextX = tileNow.MapIndexX + tileNow.GoalDirectionX;
                        nextY = tileNow.MapIndexY + tileNow.GoalDirectionY;
                        tileNext.SetMapIndex(nextX, nextY);
                        this.director.AddDebugText("ゴール戻り機能使用　" + tileNow.MapIndexX + " , " + tileNow.MapIndexY);
                    }
                    else
                    {
                        tileNext.SetMapIndex( this.ClearRouteSearchDirection(tileNow));
                    }

                }
                */

                /*
                if (this.stageData.RandomMapClearRouteTileCountMin <= this.clearRouteTiles.Count && (dataNow.GoalDirectionX != 0 || dataNow.GoalDirectionY != 0))
                {
                    nextX = dataNow.MapIndexX + dataNow.GoalDirectionX;
                    nextY = dataNow.MapIndexY + dataNow.GoalDirectionY;
                    tileNext.SetMapIndex(nextX, nextY);
                    this.director.AddDebugText("ゴール戻り機能使用　" + dataNow.MapIndexX + " , " + dataNow.MapIndexY);
                }
                else
                {
                    tileNext.SetMapIndex(this.ClearRouteSearchDirection(tileNow));
                }
                */
                tileNext.SetMapIndex(this.ClearRouteSearchDirection(dataNow));




                //TileDataIndex dataNow = this.GetTileDataIndex(tileNow.MapIndexX, tileNow.MapIndexY);
                TileDataIndex dataNext = this.GetTileDataIndex(tileNext.MapIndexX, tileNext.MapIndexY);

                if (dataNext.GoalDistance > dataNow.GoalDistance )
                { 
                    dataNext.GoalDistance = dataNow.GoalDistance+1;
                    
                    dataNext.GoalDirectionX = dataNow.MapIndexX - dataNext.MapIndexX;
                    dataNext.GoalDirectionY = dataNow.MapIndexY - dataNext.MapIndexY;
                }
            }

            Debug.Log("clear route search retry.");
        }

        




        return false;
    }



    private TileDataIndex ClearRouteSearchDirection( TileDataIndex tileNow )
    {
        List<TileDataIndex> nextTiles = new List<TileDataIndex>();

        int nowX = tileNow.MapIndexX;
        int nowY = tileNow.MapIndexY;

        int checkX, checkY;

        checkX = nowX + 1;
        checkY = nowY;
        if (this.CheckNextClearRoute(checkX, checkY))
        {
            //nextTiles.Add(new TileDataIndex(checkX, checkY));
            nextTiles.Add(this.GetTileDataIndex(checkX, checkY));
        }

        checkX = nowX;
        checkY = nowY + 1;
        if (this.CheckNextClearRoute(checkX, checkY))
        {
            //nextTiles.Add(new TileDataIndex(checkX, checkY));
            nextTiles.Add(this.GetTileDataIndex(checkX, checkY));
        }

        checkX = nowX - 1;
        checkY = nowY;
        if (this.CheckNextClearRoute(checkX, checkY))
        {
            //nextTiles.Add(new TileDataIndex(checkX, checkY));
            nextTiles.Add(this.GetTileDataIndex(checkX, checkY));
        }

        checkX = nowX;
        checkY = nowY - 1;
        if (this.CheckNextClearRoute(checkX, checkY))
        {
            //nextTiles.Add(new TileDataIndex(checkX, checkY));
            nextTiles.Add(this.GetTileDataIndex(checkX, checkY));
        }

        if (nextTiles.Count == 0)
            return null;

        if( nextTiles.Count >= 2 && this.clearRouteLastTile != null )
        {
            // 後戻り禁止
            for( int checkID=nextTiles.Count-1; checkID>=0; checkID-- )
            {
                if( nextTiles[checkID].GetIsSameTile(this.clearRouteLastTile) )
                {
                    nextTiles.RemoveAt(checkID);
                }
            }                
        }


        if( nextTiles.Count >= 2 )
        {
            TileDataIndex dataNow = this.GetTileDataIndex(tileNow.MapIndexX, tileNow.MapIndexY);

            // 必要なマス数ルートを作成していて分岐にぶつかったときはゴールに向かって戻る
            if (this.stageData.RandomMapClearRouteTileCountMin <= this.itemCandidateTilesClearRoute.Count && (dataNow.GoalDirectionX != 0 || dataNow.GoalDirectionY != 0))
            {
                int nextX = dataNow.MapIndexX + dataNow.GoalDirectionX;
                int nextY = dataNow.MapIndexY + dataNow.GoalDirectionY;

                TileDataIndex returnTile = this.GetTileDataIndex(nextX, nextY);
                
                if( !returnTile.GetIsSameTile(this.clearRouteLastTile) )
                {
                    this.director.AddDebugText("ゴール戻り機能使用　" + dataNow.MapIndexX + " , " + dataNow.MapIndexY);
                    return returnTile;
                }
            }

        }



        return nextTiles[UnityEngine.Random.Range(0, nextTiles.Count)];            
    }


    private TileDataIndex GetTileDataIndex( int x , int y)
    {
        TileDataIndex data = this.tileDataItems[x, y];

        if( data == null )
        {
            data = this.tileDataItems[x, y] = new TileDataIndex(x,y);
        }

        return data;
    }

    private void DumpMapData()
    {

        for (int i = 0; i < TileXMax; ++i)
        {
            string line = "";
            for (int j = 0; j < TileYMax; ++j)
            {
                int x = i;
                int y = j;
                int data = this.getMapData(x, y);
                line += String.Format("{0, 4} ", data);
            }

            this.director.AddDebugText(line);
        }

    }
}
