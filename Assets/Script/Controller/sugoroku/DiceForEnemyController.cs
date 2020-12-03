using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceForEnemyController : MonoBehaviour
{

    private GameObject DiceObject;
    private GameObject ShadowObject;

    private SpriteRenderer diceRenderer;
    private SpriteRenderer shadowRenderer;

    [SerializeField]
    private GameObject CameraTarget;
    private Camera camera;

    private LimitTimeCounter activeTime;

    private float targetX, targetY;

    private GameObject enemyObject;

    private int lastDiceIndex;

    private int diceCount;

    private SugorokuDirector director;



    // Start is called before the first frame update
    void Start()
    {
        this.DiceObject = this.transform.Find("Dice").gameObject;
        this.ShadowObject = this.transform.Find("Shadow").gameObject;

        this.diceRenderer = this.DiceObject.GetComponent<SpriteRenderer>();
        this.shadowRenderer = this.ShadowObject.GetComponent<SpriteRenderer>();

        //this.camera = this.transform.parent.GetComponent<Camera>();
        this.camera = this.CameraTarget.GetComponent<Camera>();

        this.activeTime = new LimitTimeCounter();
        this.gameObject.SetActive(false);

        this.director = SugorokuDirector.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if( !this.activeTime.IsFinished )
        {
            float x = this.targetX + Mathf.Cos(2f) * 5f * this.activeTime.Ratio;
            float y = this.targetY + Mathf.Sin(2f) * 5f * this.activeTime.Ratio;

            this.DiceObject.transform.position = new Vector3(x,y, 0);
            this.ShadowObject.transform.position = new Vector3(x, y-0.5f, 0);

            int nextDiceIndex;
            do
            {
                nextDiceIndex = Random.Range(0, director.Resource.SpritesDiceArrayRotation.Length);
            }
            while (this.lastDiceIndex == nextDiceIndex);

            this.lastDiceIndex = nextDiceIndex;

            this.diceRenderer.sprite = director.Resource.SpritesDiceArrayRotation[this.lastDiceIndex];

            this.activeTime.Update();

            if( this.activeTime.IsFinished )
            {
                this.diceRenderer.sprite = director.Resource.SpritesDiceArray[this.diceCount];
            }
        }
    }

    public void Open( GameObject enemyObject )
    {

        this.diceCount = Random.Range(0, 6);

        this.enemyObject = enemyObject;
        CharaController chara = this.enemyObject.GetComponent<CharaController>();
        chara.setLimitValue(this.diceCount + 1);


        int dicePosXInMap = chara.posX-1;
        int dicePosYInMap = chara.posY-1;

        Debug.Log(" enemy " + chara.posX + " , " + chara.posY);

        this.diceRenderer.sortingOrder = SugorokuDirector.GetInstance().MapController.GetOrderInLayerForTile(dicePosXInMap, dicePosYInMap, MapController.MapObjectOrderIndex.Item);
        this.shadowRenderer.sortingOrder = SugorokuDirector.GetInstance().MapController.GetOrderInLayerForTile(dicePosXInMap, dicePosYInMap, MapController.MapObjectOrderIndex.Shadow);

        this.lastDiceIndex = -1;


        this.gameObject.SetActive(true);

        this.activeTime.Start(0.2f);

        //var worldPos = this.camera.ScreenToWorldPoint(new Vector3(0, 0, 10));
        //this.DiceObject.transform.position = worldPos;

        //Vector3 worldPos = this.camera.ScreenToWorldPoint(new Vector3(enemyObject.transform.position.x, enemyObject.transform.position.y, 10));
        //this.DiceObject.transform.position = worldPos;
        //this.DiceObject.transform.position = new Vector3(enemyObject.transform.position.x -1f , enemyObject.transform.position.y + 1f , 0);


        this.DiceObject.transform.position = new Vector3(dicePosXInMap, dicePosYInMap , 0);

        //this.targetX = enemyObject.transform.position.x - 1f;
        //this.targetY = enemyObject.transform.position.y + 1f;

        float tileHeight = 0f;

        if (dicePosXInMap >= 0 && dicePosYInMap >= 0)
        {
            int tileIndex = director.MapController.getMapData(dicePosXInMap, dicePosYInMap);
            if (tileIndex < 100 && tileIndex !=0) // 100以下はタイルがあるので高さ調整
                tileHeight = 0.3f;
        }


        Vector2 pos = MapController.calPos(dicePosXInMap, dicePosYInMap);
        this.targetX = pos.x;
        this.targetY = pos.y + tileHeight;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }


}
