using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugorokuDirector : MonoBehaviour
{

    private static SugorokuDirector director = null;
    public static SugorokuDirector GetInstance()
    {
        if (SugorokuDirector.director == null)
            SugorokuDirector.director = GameObject.Find("SugorokuDirector").GetComponent<SugorokuDirector>();

        return SugorokuDirector.director;
    }

    [SerializeField]
    Const.GameStatus gameStatus;         // 0:wait 1:waitDiceClick 2:diceWait 3:playerMenuSelectWait 4:playerMove 5:userWaySelect 6:clear 7:gameOver

    [SerializeField]
    public DevelopersOption DevelopersOption;

    public GameObject mainCamera;

    public GameObject sceneObject;
    private StageDataTable stageData;

    public GameObject map;
    public GameObject player;
    public GameObject enemy;

    //今回の追加
    public GameObject View;
    public PlayerController playerController;
    public ItemCountUiPrinter[] ItemCountUiPrinters;
    public Animator Playeranimator, Cameraanimaotr, Cutnumberanimator, Cutnumber99animator;
    public Image cutNumbersprite;
    public Sprite[] pinknumbers;
    public AudioClip[] StageBGMs;
    public int startx, starty;

    //-------------------- UIまわり ------------------
    //int limitValue; // 実際に動ける残りの数
    public GameObject allows;  // →
    public GameObject remainingDice;
    public GameObject remainingAmount;

    public GameObject darkObject;
    public GameObject specialFrame;
    public GameObject tutorialFrame;

    //-------------------- ステージ -----------------

    //-------------------- さいころ ------------------
    public GameObject dice;  // さいころ
    public GameObject actionButtonObject;

    //-------------------- クリア -----------------
    public GameObject star;
    public GameObject clearFlash;

    //-------------------- Message ----------------
    public GameObject startMessage;
    public GameObject clearMessage;
    public GameObject gameOverMessage;

    public MapController MapController
    {
        get;
        private set;
    }
    
    //サウンド全般
    public SugorokuResource Resource { get; private set; }
    private SoundData soundData;
    private AudioSource audioSourceSaiStart;
    private AudioSource audioSourceSaiStop;
    private AudioSource audioSourceCardGet;
    private AudioSource audioSourceSugorokuMove;
    private AudioSource audioSourceItemGet;

    private AudioSource audioSourceStageClear;
    private AudioSource audioSourceGameOver;
    private AudioSource audioSourceBGM;

    // ------------------ スペシャルマス ---------
    private SpecialSelecter specialSelecter;
    private Const.Special selectedSpecial;
    private LimitTimeCounter specialEffectTime;
    private SpecialDiceFree15SecCounter specialDiceFreeCounter;
    private SpecialDicePlusOne specialDicePlusOne;

    private LimitTimeCounter specialDiceFreeTime;

    private List<GameObject> usedSpecialTiles;

    private List<GetFoodData> enemyGetFoods;
    private List<GetFoodData> playerGetFoods;


    public Camera Camera
    {
        get; private set;
    }

    private bool IsDiceFreeMode
    {
        get
        {
            return !this.specialDiceFreeTime.IsFinished;
        }
    }

    private GameObject DiceObjectRemaining;
    private GameObject DiceObjectSpecial15Sec;

    private List<GameObject> Items { get; set; }

    public bool IsItemX2Mode { get; private set; }

    private UnityEngine.UI.InputField DebugText;

    private void Awake()
    {
        this.Camera = this.mainCamera.GetComponent<Camera>();

        SugorokuDirector.director = this;
        this.specialEffectTime = new LimitTimeCounter();
        this.specialDiceFreeTime = new LimitTimeCounter();
        this.Items = new List<GameObject>();

        this.Resource = new SugorokuResource();

        //this.DebugText = GameObject.Find("DebugCanvas/Panel/Text").GetComponent<Text>();
        //this.DebugText = GameObject.Find("DebugCanvas/Panel/InputField").GetComponent<InputField>();

        GameObject debugObject = GameObject.Find("DebugCanvas/Panel/InputField");
        if (debugObject != null)
            this.DebugText = debugObject.GetComponent<InputField>();
        else
            this.DebugText = null;

    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Start: start");
        Init();

        StageCreate();

        StartCoroutine(Manager());

        //playerCreate();

        View.transform.position = player.transform.position;
        startx = playerController.posX;
        starty = playerController.posY;
    }

    //==========================================================================
    // サイコロスタート音の取得
    //==========================================================================
    public AudioSource GetSaiStartSound()
    {
        return this.audioSourceSaiStart;
    }

    private void Init()
    {
        this.Camera = this.mainCamera.GetComponent<Camera>();

        this.usedSpecialTiles = new List<GameObject>();

        this.enemyGetFoods = new List<GetFoodData>();
        this.playerGetFoods = new List<GetFoodData>();

        //this.Resource = new SugorokuResource();
        this.soundData = SaveData.GetClass<SoundData>("soundData", new SoundData());

        this.audioSourceSaiStart = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceSaiStart.clip = this.Resource.AudioSaiStart;

        this.audioSourceSaiStop = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceSaiStop.clip = this.Resource.AudioSaiStop;

        this.audioSourceCardGet = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceCardGet.clip = this.Resource.AudioCardGet;

        this.audioSourceSugorokuMove = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceSugorokuMove.clip = this.Resource.AudioSugorokuMove;

        this.audioSourceItemGet = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceItemGet.clip = this.Resource.AudioItemGet;

        this.audioSourceStageClear = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceStageClear.clip = this.Resource.AudioStageClear;

        this.audioSourceGameOver = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceGameOver.clip = this.Resource.AudioGameOver;

        this.audioSourceBGM = this.gameObject.AddComponent<AudioSource>();

        //this.audioSourceBGM.clip = this.Resource.AudioBGM;

        //BGMはStageDateの後ろで設定


        this.specialSelecter = this.specialFrame.GetComponent<SpecialSelecter>();



        this.DiceObjectRemaining= GameObject.Find("Canvas/UIParts/RemainingDice");
        this.DiceObjectSpecial15Sec = GameObject.Find("Canvas/UIParts/Special15SecDice");
        this.DiceObjectSpecial15Sec.SetActive(false);
        this.specialDiceFreeCounter = this.DiceObjectSpecial15Sec.GetComponent<SpecialDiceFree15SecCounter>();


        this.specialDicePlusOne = this.DiceObjectRemaining.transform.Find("SpecialPlusOne").GetComponent<SpecialDicePlusOne>(); ;
        this.specialDicePlusOne.Close();

        this.IsItemX2Mode = false;

        this.MapController = map.GetComponent<MapController>();


        stageData = sceneObject.GetComponent<SugorokuScene>().GetStageData();



        ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());
        if( clearStar.clearStageStarList[0].clearStarList[0]<=0 )
            this.ChangeGameStatus(Const.GameStatus.tutorial);
        else
            this.ChangeGameStatus(Const.GameStatus.wait);

        //BGM設定
        this.audioSourceBGM.clip = StageBGMs[stageData.BGMnumber];

        this.audioSourceBGM.loop = true;

        this.soundData.PlayBGM(this.audioSourceBGM);

    }

    public void ClosedTutorial()
    {
        this.ChangeGameStatus(Const.GameStatus.wait);
    }

    private IEnumerator Manager()
    {
        while (true)
        {
            yield return StartCoroutine(this.DiceManager());
        }
    }

    private IEnumerator DiceManager()
    {
        //Debug.Log("DiceManager: start.");

        // playerターン
        while (player.GetComponent<CharaController>().getLimitValue() > 0)
        {
            //yield return new WaitForSeconds(0.25f);
            yield return new WaitForSeconds(this.DevelopersOption.PlayerMoveStepWait);
            yield return this.CharaMove(player);

            //foreach (Transform child in enemy.transform)
            //    Debug.Log(Vector2.Distance(player.transform.position, child.transform.position));
        }

        // enemyターン
        if (this.gameStatus == Const.GameStatus.enemyMove)
        {
            foreach (Transform child in enemy.transform)
            {
                bool first = true;
                while (child.GetComponent<CharaController>().getLimitValue() > 0)
                {
                    if (first)
                    {
                        yield return new WaitForSeconds(0.2f);
                        mainCamera.GetComponent<MainCameraController>().setCharaObject(child.gameObject);
                        //yield return new WaitForSeconds(1f);
                        yield return new WaitForSeconds( this.DevelopersOption.EnemyMoveStartWait );
                        first = false;
                    }

                    //yield return new WaitForSeconds(0.25f);
                    yield return new WaitForSeconds(this.DevelopersOption.EnemyMoveStepWait);
                    yield return this.CharaMove(child.gameObject);
                    //Debug.Log(Vector2.Distance(player.transform.position, child.transform.position));
                }

                // 移動終了待ち
                yield return new WaitForSeconds(this.DevelopersOption.EnemyMoveFinishWait);

            }
        }


        if (!isFinish())
        {
            if (this.gameStatus == Const.GameStatus.enemyMove)
            {
                // playerターン
                yield return new WaitForSeconds(0.2f);
                mainCamera.GetComponent<MainCameraController>().setCharaObject(player);
                yield return new WaitForSeconds(0.2f);
                ChangeGameStatus(Const.GameStatus.waitDiceClick);
            }
        }

        while (gameStatus == Const.GameStatus.diceWait)
        {
            //yield return new WaitForSeconds(2.0f);
            yield return new WaitForSeconds(this.DevelopersOption.DiceStopWait);
            // サイコロが止まるのを待つ
            if (gameStatus == Const.GameStatus.diceWait)
            {
                //Debug.Log("GameStatus: " + gameStatus);
                ChangeGameStatus(Const.GameStatus.diceWait);
            }
        }

        //Debug.Log("GameStatus: " + gameStatus);
        //Debug.Log("DiceManager: end.");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update: start");



        if( !this.specialDiceFreeTime.IsFinished )
        {
            if( this.gameStatus == Const.GameStatus.diceWait || this.gameStatus == Const.GameStatus.waitDiceClick || this.gameStatus == Const.GameStatus.playerMove || this.gameStatus == Const.GameStatus.userWaySelect )
                this.specialDiceFreeTime.Update();

            this.specialDiceFreeCounter.TimeUpdate(this.specialDiceFreeTime.Ratio);

            if( this.specialDiceFreeTime.IsFinished )
            {
                this.DiceObjectRemaining.SetActive(true);
                this.DiceObjectSpecial15Sec.SetActive(false);
            }
        }

        if (this.gameStatus == Const.GameStatus.waitDiceClick)
        {
            Debug.Log("スクロール");
            mainCamera.GetComponent<MainCameraController>().setCharaObject(View);
            if (Input.GetMouseButton(0))
            {
                float mouse_x_delta = Input.GetAxis("Mouse X");
                float mouse_y_delta = Input.GetAxis("Mouse Y");
                View.transform.position += new Vector3(-mouse_x_delta, -mouse_y_delta);
                //Debug.Log(mouse_x_delta + " " + mouse_y_delta);

                //範囲制限
                Vector3 nowPos = View.transform.position;
                if (nowPos.x > 10)
                    nowPos.x = 10;
                if (nowPos.x < -10)
                    nowPos.x = -10;

                if (nowPos.y > 10)
                    nowPos.y = 10;
                if (nowPos.y < -10)
                    nowPos.y = -10;

                View.transform.position = nowPos;
            }
        }
        else
        {
            View.transform.position = player.transform.position;
        }


        if ( !this.specialEffectTime.IsFinished )
        {
            this.specialEffectTime.Update();

            if(this.specialEffectTime.IsFinished)
            {
                if( this.selectedSpecial == Const.Special.Dice_Free_15Sec )
                {
                    this.specialDiceFreeTime.Start(15f);
                }
                else if (this.selectedSpecial == Const.Special.Dice_Plus_One)
                {
                    this.specialDicePlusOne.Close();
                    RemainingDiceController remainingDiceController = remainingDice.GetComponent<RemainingDiceController>();
                    remainingDiceController.appendDiceCount(1);

                    RemainingAmountController remainingAmountController = remainingAmount.GetComponent<RemainingAmountController>();
                    remainingAmountController.appendDiceCount(1);

                }

                //this.IsItemX2Mode = true;
                CharaController charaController = player.GetComponent<CharaController>();

                if (!isFinish())
                {
                    if (charaController.getLimitValue() <= 0)
                        this.EnemyMoveStart();
                    else
                        this.ChangeGameStatus(Const.GameStatus.playerMove);
                }
            }

        }


    }


    public void CreateItemX2Icon()
    {
        if (this.IsItemX2Mode)
        {
            foreach (GameObject itemObj in this.Items)
            {
                if (itemObj.transform.Find("Special_x2Icon"))
                    continue;

                if (itemObj.transform.Find("work_item_meat") || itemObj.transform.Find("work_item_egg") || itemObj.transform.Find("work_item_fish") || itemObj.transform.Find("work_item_vegetables"))
                {

                    GameObject x2Icon = Instantiate(SugorokuDirector.GetInstance().Resource.PrefabIconItemx2);
                    x2Icon.name = "Special_x2Icon";

                    x2Icon.transform.parent = itemObj.transform;

                    Vector3 temp = x2Icon.transform.localPosition;
                    temp.x = 0f;
                    temp.y = 0f;
                    temp.z = 0f;
                    x2Icon.transform.localPosition = temp;

                    TileData tileData = itemObj.GetComponent<TileData>();


                    SpriteRenderer render = x2Icon.transform.Find("Image").GetComponent<SpriteRenderer>();
                    render.sortingLayerName = "MapObject";
                    render.sortingOrder = this.MapController.GetOrderInLayerForTile(tileData.MapIndexX, tileData.MapIndexY, MapController.MapObjectOrderIndex.ItemInformation);
                }
            }
        }
    }

    void ChangeGameStatus(Const.GameStatus status)
    {
        if ( this.gameStatus == Const.GameStatus.special_effect)
        {
         
            CharaController charaController = player.GetComponent<CharaController>();

            string targetName = "work_tile_" + charaController.posX + "_" + charaController.posY;

            GameObject obj = GameObject.Find("CameraMap/MapTile/" + targetName );


            Sprite spriteTileImage = Resources.Load("images/sugoroku/tile/tile_special_end", typeof(Sprite)) as Sprite;

            obj.GetComponent<SpriteRenderer>().sprite = spriteTileImage;
        }

        //Debug.Log("ChangeGameStatus: start..." + status);
        gameStatus = status;

        if (status == Const.GameStatus.wait)
        {
        }


        if( status == Const.GameStatus.tutorial )
        {
            this.tutorialFrame.SetActive(true);
        }


        if( status == Const.GameStatus.special)
        {
            //this.specialFrame.SetActive(true);
            this.specialSelecter.Open();

        }
        else
        {
            //this.specialFrame.SetActive(false);
            this.specialSelecter.Close();
        }


        if( status == Const.GameStatus.special_effect )
        {
            this.specialEffectTime.Start(1.2f);


            if (this.selectedSpecial == Const.Special.Item_x2)
            {

                    /*
                    if (!this.IsItemX2Mode)
                    {
                        foreach (GameObject itemObj in this.Items)
                        {
                            if (itemObj.transform.Find("work_item_meat") || itemObj.transform.Find("work_item_egg") || itemObj.transform.Find("work_item_fish") || itemObj.transform.Find("work_item_vegetables"))
                            {
                                GameObject x2Icon = Instantiate(SugorokuDirector.GetInstance().Resource.PrefabIconItemx2);
                                x2Icon.transform.parent = itemObj.transform;

                                Vector3 temp = x2Icon.transform.localPosition;
                                temp.x = 0f;
                                temp.y = 0f;
                                temp.z = 0f;
                                x2Icon.transform.localPosition = temp;

                                TileData tileData = itemObj.GetComponent<TileData>();


                                SpriteRenderer render = x2Icon.transform.Find("Image").GetComponent<SpriteRenderer>();
                                render.sortingLayerName = "MapObject";
                                render.sortingOrder = this.MapController.GetOrderInLayerForTile(tileData.MapIndexX, tileData.MapIndexY, MapController.MapObjectOrderIndex.ItemInformation);
                            }
                        }
                    }
                    */
                    this.IsItemX2Mode = true;
                    this.CreateItemX2Icon();
                }
                else if (this.selectedSpecial == Const.Special.Dice_Free_15Sec)
            {
                this.specialDiceFreeCounter.Open();
                this.specialDiceFreeCounter.TimeUpdate(1f);

                this.DiceObjectRemaining.SetActive(false);
                this.DiceObjectSpecial15Sec.SetActive(true);
            }
            else if (this.selectedSpecial == Const.Special.Dice_Plus_One)
            {
                this.specialDicePlusOne.Open();
            }
        }


        if (status == Const.GameStatus.waitDiceClick || status == Const.GameStatus.diceWait)
        {
            DiceController diceObject = dice.GetComponent<DiceController>();
            if (status == Const.GameStatus.waitDiceClick)
            {
                //Debug.Log("waitDiceClick: start");
                diceObject.StartDice();
                dice.SetActive(true);

                actionButtonObject.GetComponent<ActionButtonController>().StartCount();

                this.SoundPlaySaiStart();
            }
            if (status == Const.GameStatus.diceWait)
            {
                //Debug.Log ("diceWait: start");
                if (diceObject.isStop())
                {
                    //Debug.Log ("dice: isStop");
                    dice.SetActive(false);
                    dice.transform.localPosition = new Vector3(2.5f, -3, 10);
                    player.GetComponent<CharaController>().setLimitValue(diceObject.getValue());

                    //Debug.Log("残り" + limitValue + "マス");
                    RemainingAmountController remainingAmountController = remainingAmount.GetComponent<RemainingAmountController>();
                    remainingAmountController.initDiceCount(diceObject.getValue());

                    RemainingDiceController remainingDiceController = remainingDice.GetComponent<RemainingDiceController>();

                    if( !this.IsDiceFreeMode )
                        remainingDiceController.decreaseDiceCount();

                    ChangeGameStatus(Const.GameStatus.playerMove);
                }
            }
            //diceCamera.SetActive(true);
        }
        else
        {
            //dice.SetActive (false);
            //diceCamera.SetActive(false);
        }

        if (status == Const.GameStatus.playerMenuSelectWait)
        {
            //Debug.Log("playerMenuSelectWait: start");
            //menuObject.SetActive(true);
        }
        else
        {
            //menuObject.SetActive(false);
        }

        if (status == Const.GameStatus.enemyMove)
        {
            //Debug.Log("enemyMove: start");
            //menuObject.SetActive(true);
        }
        else
        {
            //menuObject.SetActive(false);
        }

        if (status == Const.GameStatus.playerMove || status == Const.GameStatus.userWaySelect)
        {
            //Debug.Log("playerMove: start");
        }
        else
        {

        }

        if (status == Const.GameStatus.userWaySelect)
        {
            allows.SetActive(true);
        }
        else
        {
            allows.SetActive(false);
        }

        if (isFinish())
        {
            remainingAmount.SetActive(false);
            actionButtonObject.GetComponent<ActionButtonController>().PushOn();

            if (status == Const.GameStatus.clear)
            {
                clearMessage.SetActive(true);
                clearFlash.SetActive(true);
                darkObject.SetActive(true);
                player.GetComponent<CharaController>().changeBrightLayer();

                StarController starController = star.GetComponent<StarController>();
                int starNum = map.GetComponent<MapController>().getClearStartNum(player.GetComponent<PlayerController>().getItemCount());
                starController.setStarNum(starNum);
                star.SetActive(true);

                // SaveStar
                ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());
                clearStar.clearStageStarList[stageData.StageNo1 - 1].setClearStarNum(stageData.StageNo2, starNum);

                
                // 次の 3ステージまでクリアしていたら次のステージを追加
                if (stageData.StageNo1 == clearStar.clearStageStarList.Count && stageData.StageNo2 == ClearStageStarData.StageNumber2Max)
                    clearStar.clearStageStarList.Add(new ClearStageStarData());
                    

                SaveData.SetClass<ClearStarData>("clearStar", clearStar);
                SaveData.Save();

                this.actionButtonObject.GetComponent<ActionButtonDirector>().StageClear();

                this.SoundPlayStageClear();

            }
            else if (status == Const.GameStatus.gameOver)
            {                
                gameOverMessage.SetActive(true);
                darkObject.SetActive(true);
                player.GetComponent<CharaController>().changeBrightLayer();

                this.SoundPlayGameOver();
            }

            DiceController diceController = dice.GetComponent<DiceController>();
            diceController.changeState(DiceController.State.none);
        }
        else
        {
            clearMessage.SetActive(false);
            gameOverMessage.SetActive(false);

            star.SetActive(false);
        }

    }

    // キャラクター移動
    private IEnumerator CharaMove(GameObject charaObject)
    {
        //Debug.Log("CharaMove: start");
        CharaController charaController = charaObject.GetComponent<CharaController>();


        if (this.gameStatus == Const.GameStatus.special || this.gameStatus == Const.GameStatus.special_effect)
            yield break;

        if (charaController.getLimitValue() <= 0)
        {
            yield break;
        }

        // 現在位置から自分が来た方向以外の方向のますをチェック
        List<Const.Direction> nextDirList = new List<Const.Direction>();
        if (charaController.choiceDirection == Const.Direction.none)
        {
            if (charaController.direction != Const.Direction.left
                //&& GetMasu(charaController.posX, charaController.posY, Const.Direction.right) != 0)
                && this.GetIsRouteMasu(charaController.posX, charaController.posY, Const.Direction.right) )
            {
                nextDirList.Add(Const.Direction.right);
                //Debug.Log("右にいける");
            }

            if (charaController.direction != Const.Direction.right
                //&& GetMasu(charaController.posX, charaController.posY, Const.Direction.left) != 0)
                && this.GetIsRouteMasu(charaController.posX, charaController.posY, Const.Direction.left))
            {
                nextDirList.Add(Const.Direction.left);
                //Debug.Log("左にいける");
            }

            if (charaController.direction != Const.Direction.down
                //&& GetMasu(charaController.posX, charaController.posY, Const.Direction.up) != 0)
                && this.GetIsRouteMasu(charaController.posX, charaController.posY, Const.Direction.up))
            {
                nextDirList.Add(Const.Direction.up);
                //Debug.Log("上にいける");
            }

            if (charaController.direction != Const.Direction.up
                //&& GetMasu(charaController.posX, charaController.posY, Const.Direction.down) != 0)
                && this.GetIsRouteMasu(charaController.posX, charaController.posY, Const.Direction.down))
            {
                nextDirList.Add(Const.Direction.down);
                //Debug.Log("下にいける");
            }

            // この段階でいけるところがない（＝来た道を戻るしかない）

            if (nextDirList.Count == 0)
            {
                //nextDirList.Add(charaController.direction);
                switch (charaController.direction)
                {
                    case Const.Direction.left:
                        nextDirList.Add(Const.Direction.right);
                        break;
                    case Const.Direction.right:
                        nextDirList.Add(Const.Direction.left);
                        break;
                    case Const.Direction.up:
                        nextDirList.Add(Const.Direction.down);
                        break;
                    case Const.Direction.down:
                        nextDirList.Add(Const.Direction.up);
                        break;
                }
            }
        }
        else
        {
            // 分岐を選択した時
            nextDirList.Add(charaController.choiceDirection);
            charaController.choiceDirection = Const.Direction.none;
        }

        if ("Player" != charaObject.name)
        {
            // Enemy
            int choiceDirection = Random.Range(0, nextDirList.Count);
            charaController.Move(nextDirList[choiceDirection]);
            CharaMoveNow(charaObject, MapController.calPosPlayer(charaController.posX, charaController.posY, charaController.adjustPosition));
            yield break;
        }

        // Player
        if (charaObject.GetComponent<PlayerController>().isJumping())
        {
            yield break;
        }

        
        // 自動で進む
        if (nextDirList.Count == 1)
        {
            //Debug.Log("nextDirection: " + nextDirList[0].ToString());
            charaController.Move(nextDirList[0]);
            CharaMoveNow(charaObject, MapController.calPosPlayer(charaController.posX, charaController.posY, charaController.adjustPosition));
        }
        else
        {
            //Debug.Log("分岐");
            allows.GetComponent<ArrowController>().SetDir(nextDirList);
            ChangeGameStatus(Const.GameStatus.userWaySelect);
        }

    }
    //移動処理
    private void CharaMoveNow(GameObject charaObject, Vector2 nextPos)
    {
        //Debug.Log("CharaMoveNow : " + charaObject.name);
        CharaController charaController = charaObject.GetComponent<CharaController>();

        charaController.decreaseLimitValue();
        charaController.ChangeMoveAnimation();

        if ("Player" == charaObject.name)
        {
            RemainingAmountController remainingAmountController = remainingAmount.GetComponent<RemainingAmountController>();

            if( !this.IsDiceFreeMode )
                remainingAmountController.decreaseDiceCount();

            charaController.MoveChara(gameObject, nextPos);
            Debug.Log("プレイヤー");
            //
            //aaa
            //プレイヤーからの衝突
            foreach (Transform child in enemy.transform)
            {
                EnemyController enemyController = child.GetComponent<EnemyController>();

                if (playerController.posX == enemyController.posX && playerController.posY == enemyController.posY)
                {
                    Debug.Log("接触したよ");
                    Playeranimator.SetTrigger("Damage");
                    Cameraanimaotr.SetTrigger("Shock");
                    int number = EnemyCheck(enemyController);
                    Debug.Log(number);
                    StartCoroutine(CutnumberMessage(number));
                    RandomCut(number);
                }
            }
        }
        else
        {
            charaController.MoveChara(gameObject, nextPos);
            Debug.Log(charaObject.name);
            //
            //aaa
            EnemyController enemyController = charaObject.GetComponent<EnemyController>();
            if (playerController.posX == enemyController.posX && playerController.posY == enemyController.posY)
            {
                Debug.Log("敵が接触したよ");
                //ItemCountUiPrinters[Random.Range(0, 4)].RemveHaveCount(4);
                EnemyCheck(enemyController);
                Playeranimator.SetTrigger("Damage");
                Cameraanimaotr.SetTrigger("Shock");
                int number = EnemyCheck(enemyController);
                Debug.Log(number);
                StartCoroutine(CutnumberMessage(number));
                RandomCut(number);
            }
        }

    }

    void PlayerMoved()
    {
        Debug.Log("PlayerMoved: start.");

        CharaController charaController = player.GetComponent<CharaController>();

        //Debug.Log(playerController.posX + "," + playerController.posY);

        //int posX = playerController.posX;
        //int posY = playerController.posY;

        if (charaController.getLimitValue() <= 0)
        {
            //Debug.Log ("ChangeAnimationForwardStand: 2.");
            charaController.ChangeAnimationForwardStand();

            //Debug.Log("move end.");
            Vector2 nextPos = MapController.calPosPlayer(charaController.posX, charaController.posY, charaController.adjustPosition);
            iTween.MoveTo(player, iTween.Hash("x", nextPos.x, "y", nextPos.y, "delay", 0, "time", 0.2f));

            RemainingAmountController remainingAmountController = remainingAmount.GetComponent<RemainingAmountController>();
            remainingAmountController.clearDisp();
        }

        //プレイヤーからの衝突
        //foreach (Transform child in enemy.transform)
        //{
        //    EnemyController enemyController = child.GetComponent<EnemyController>();

        //    if (playerController.posX == enemyController.posX && playerController.posY == enemyController.posY)
        //    {
        //        Debug.Log("接触したよ");
        //        ItemCountUiPrinters[Random.Range(0, 4)].RemveHaveCount();
        //    }
        //}


            // 現在のマスがゴールでゴール条件をクリアしているか
            //Debug.Log("masu[" + posX + "," + posY + "] : " + GetMasu(posX, posY));
            if (GetMasu(charaController.posX, charaController.posY) == -1)
        {
            if (this.IsStageClear())
            {
                //Debug.Log("Clear!");
                charaController.setLimitValue(0);
                this.StageClear();
                ChangeGameStatus(Const.GameStatus.clear);
                return;
            }
            else
            {

                foreach (GetFoodData foodData in this.playerGetFoods)
                {
                    TileData tileData = foodData.tileObject.GetComponent<TileData>();
                    int mapData = this.MapController.getMapData(tileData.MapIndexX, tileData.MapIndexY);

                    // アイテムを作成
                    this.MapController.CreateMapItem(tileData.MapIndexX, tileData.MapIndexY, foodData.tileObject, mapData);
                }

                foreach ( GetFoodData foodData in this.enemyGetFoods )
                {
                    TileData tileData = foodData.tileObject.GetComponent<TileData>();
                    int mapData = this.MapController.getMapData(tileData.MapIndexX, tileData.MapIndexY);

                    // アイテムを作成
                    this.MapController.CreateMapItem( tileData.MapIndexX , tileData.MapIndexY , foodData.tileObject , mapData);
                }

                if( this.IsItemX2Mode )
                    this.CreateItemX2Icon();

                //Debug.Log("Not Clear");
            }
        }
        else if (GetMasu(charaController.posX, charaController.posY) == 7)
        {

            string targetName = "work_tile_" + charaController.posX + "_" + charaController.posY;
            GameObject target = GameObject.Find("CameraMap/MapTile/" + targetName);

            foreach( GameObject check in this.usedSpecialTiles )
            {
                if (check == target)
                {
                    if (charaController.getLimitValue() <= 0)
                        this.EnemyMoveStart();
                    else
                        this.ChangeGameStatus(Const.GameStatus.playerMove);

                    return;
                }
            }

            this.usedSpecialTiles.Add(target);

            ChangeGameStatus(Const.GameStatus.special);
            return;
        }
        else if (remainingDice.GetComponent<RemainingDiceController>().getDiceCount() == 0)
        {
            // サイコロの残数が0で、残り移動数が0ならゲームオーバー
            if (charaController.getLimitValue() <= 0)
            {
                this.GameOver();
                ChangeGameStatus(Const.GameStatus.gameOver);
                return;
            }
        }


        // 敵の移動
        if (!isFinish())
        {
            if (charaController.getLimitValue() <= 0)
            {
                EnemyMoveStart();
                return;
            }
        }
    }

    private void EnemyMoveStart()
    {
        foreach (Transform child in enemy.transform)
        {
            int value = Random.Range(1, 4);
            child.GetComponent<CharaController>().setLimitValue(value);
        }

        ChangeGameStatus(Const.GameStatus.enemyMove);

    }

    bool IsStageClear()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        MapController mapObject = map.GetComponent<MapController>();

        if (mapObject.isClear(playerController.getItemCount()))
        {
            return true;
        }
        return false;
    }

    void StageClear()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.ChangeAnimationForwardStand();
        playerController.ChangeAnimationLaugh();



        RemainingAmountController remainingAmountController = remainingAmount.GetComponent<RemainingAmountController>();
        remainingAmountController.clearDisp();

    }

    void GameOver()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.ChangeAnimationForwardStand();
        //playerController.ChangeAnimationSad();
        playerController.ChangeAnimationAnger();

        RemainingAmountController remainingAmountController = remainingAmount.GetComponent<RemainingAmountController>();
        remainingAmountController.clearDisp();
    }

    public void ChangeWaitDiceClick()
    {
        this.ChangeGameStatus(Const.GameStatus.waitDiceClick);
    }

    public void ChangeDiceWait()
    {
        this.ChangeGameStatus(Const.GameStatus.diceWait);
    }

    // ステージの配置
    void StageCreate()
    {
        //Debug.Log ("StageCreate: start.");
        //RemainingDiceController remainingDiceController = remainingDice.GetComponent<RemainingDiceController>();
        //remainingDiceController.initDiceCount(10);

        startMessage.SetActive(true);
        dice.SetActive(false);
    }


    public void SetDiceCount( int count )
    {
        RemainingDiceController remainingDiceController = remainingDice.GetComponent<RemainingDiceController>();
        remainingDiceController.initDiceCount(count);
    }


    /*
    private void SetMasu( int x, int y , int tileID )
    {
        MapController mapController = map.GetComponent<MapController>();
        mapController.setMapData(x, y, tileID);
    }
    */

    private int GetMasu(int x, int y)
    {
        return GetMasu(x, y, 0, 0);
    }


    private bool GetIsRouteMasu( int x, int y, Const.Direction dir)
    {
        int masuID = GetMasu(x, y, dir);

        return MapController.GetIsRouteMasu( masuID );
        /*
        if (masuID == 0)
            return false;

        if (masuID == 10)
            return false;

        if (masuID >= 30 && masuID <= 50)
            return false;
        return true;
            */

    }



    private int GetMasu(int x, int y, Const.Direction dir)
    {
        MapController mapController = map.GetComponent<MapController>();
        return mapController.getMapData(x, y, dir);
    }

    private int GetMasu(int x, int y, int dx, int dy)
    {
        MapController mapController = map.GetComponent<MapController>();
        return mapController.getMapData((x + dx), (y + dy));
    }

    public void UserSelectArrow(Const.Direction direction)
    {
        CharaController playerController = player.GetComponent<CharaController>();
        playerController.choiceDirection = direction;

        ChangeGameStatus(Const.GameStatus.playerMove);

    }

    public Const.GameStatus getGameStatus()
    {
        return this.gameStatus;
    }

    public bool isWait()
    {
        return (Const.GameStatus.wait == this.gameStatus);
    }

    public bool isWaitDiceClick()
    {
        return (Const.GameStatus.waitDiceClick == this.gameStatus);
    }

    public bool isPlayerMove()
    {
        return (Const.GameStatus.playerMove == this.gameStatus);
    }

    public bool isClear()
    {
        return (Const.GameStatus.clear == this.gameStatus);
    }

    public bool isGameOver()
    {
        return (Const.GameStatus.gameOver == this.gameStatus);
    }

    public bool isSpecialSelecting()
    {
        return (Const.GameStatus.special == this.gameStatus);
    }

    public bool isSpecialSelected()
    {
        return (Const.GameStatus.special_effect == this.gameStatus);
    }

    public bool isFinish()
    {
        return isClear() || isGameOver();
    }


    public void SoundPlaySaiStart()
    {
        this.soundData.PlaySound(this.audioSourceSaiStart);
    }

    public void SoundPlaySaiStop()
    {
        this.soundData.PlaySound(this.audioSourceSaiStop);
    }

    public void SoundPlayCardGet()
    {
        this.soundData.PlaySound(this.audioSourceCardGet);
    }

    public void SoundPlayCardSugorokuMove()
    {
        this.soundData.PlaySound(this.audioSourceSugorokuMove);
    }

    public void SoundPlayItemGet()
    {
        this.soundData.PlaySound(this.audioSourceItemGet);
    }

    public void SoundPlayStageClear()
    {
        this.soundData.PlaySound(this.audioSourceStageClear);
    }


    public void SoundPlayGameOver()
    {
        this.soundData.PlaySound(this.audioSourceGameOver);
    }

    public void SoundPlay( AudioSource source )
    {
        this.soundData.PlaySound(source);
    }


    public void SpecialSelected( Const.Special special )
    {

        /*
        CharaController charaController = player.GetComponent<CharaController>();

        if (!isFinish())
        {
            if (charaController.getLimitValue() <= 0)
            {
                EnemyMoveStart();
            }
            else
                this.ChangeGameStatus(Const.GameStatus.playerMove);
        }
        */

        this.selectedSpecial = special;

        this.ChangeGameStatus(Const.GameStatus.special_effect);
    }

    public void SpecialSelectSkipRequest()
    {
        this.specialSelecter.Skip();
    }


    public void AddItem( GameObject obj )
    {
        this.Items.Add(obj);
    }

    public void AddEnemyGetFood( GetFoodData foodData )
    {
        this.enemyGetFoods.Add(foodData);
    }


    public void AddPlayerGetFood(GetFoodData foodData)
    {
        this.playerGetFoods.Add(foodData);
    }


    public void AddDebugText( string text )
    {
        if (this.DebugText == null)
            return;

        this.DebugText.text += text + "\n";
    }

    /// <summary>
    /// 敵の判別処理
    /// </summary>
    /// <param name="enemyController"></param>
    private int EnemyCheck(EnemyController enemyController)
    {
        int foodnumber = Random.Range(0, 4), cutnumber;

        switch (enemyController.gameObject.tag)
        {
            case "enemy1":
                cutnumber = 1;
                break;

            case "enemy2":
                cutnumber = Random.Range(1, 2);
                break;

            case "enemy3":
                cutnumber = Random.Range(1, 3);
                break;

            case "enemy4":
                cutnumber = Random.Range(1, 4);
                break;

            case "enemy5":
                cutnumber = Random.Range(1, 5);
                break;

            case "enemy6":
                cutnumber = Random.Range(1, 6);
                break;

            case "enemy7":
                cutnumber = Random.Range(1, 7);
                break;

            default:
                cutnumber = 99;
                break;
        }

        return cutnumber;
    }

    IEnumerator CutnumberMessage(int cutnumber)
    {
        if (cutnumber < 10)
        {
            cutNumbersprite.sprite = pinknumbers[cutnumber];
            Cutnumberanimator.SetTrigger("Active");
            yield return new WaitForSeconds(2);
        }
        else
        {
            Cutnumber99animator.SetTrigger("Active");
            yield return new WaitForSeconds(2);
        }
    }
    private void RandomCut(int cutnumber)
    {
        while(0<cutnumber)
        {
            ItemCountUiPrinters[Random.Range(0, 4)].RemveHaveCount(1);
            cutnumber--;
        }
    }
}
