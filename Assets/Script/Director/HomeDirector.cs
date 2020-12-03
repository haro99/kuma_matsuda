using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeDirector : MonoBehaviour , IDebugCommandRecver
{
    public GameObject SugorokuMenu;
    public GameObject CardMenu;
    public GameObject PlayConfirmMenu;
    public GameObject CardDetailMenu;

    private ClearStarData clearStar;

    private LimitTimeCounter selectedWaitTime;
    private int selectedStage1;
    private int selectedStage2;

    private static HomeDirector homeDirector = null;
    public static HomeDirector GetInstance()
    {
        if (HomeDirector.homeDirector == null)
            HomeDirector.homeDirector = GameObject.Find("HomeDirector").GetComponent<HomeDirector>();

        
        return HomeDirector.homeDirector;
    }

    public int RoomID_Next { get; set; }
    public int RoomID_Now { get; private set; }
    public int RoomID_Last { get; private set; }

    private Room room;
    private FadeController fade;

    RoomDataList roomDataList;
    List<RoomData> unlockedRooms;
    public HomeResource Resource { get; private set; }


    private enum RoomStatus
    {
        Active ,
        Moving ,
    }
    private RoomStatus roomStatus;
    private LimitTimeCounter roomStatusCounter;

    private SoundData soundData;
    private AudioSource audioSourceSelect;
    private AudioSource audioSourceBGM;


    void Awake()
    {
        this.Resource = new HomeResource();
        this.selectedWaitTime = new LimitTimeCounter();
    }



    // Use this for initialization
    void Start()
    {

        this.selectedWaitTime = new LimitTimeCounter();
        //this.clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());

        this.soundData = SaveData.GetClass<SoundData>("soundData", new SoundData());
        this.audioSourceSelect = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceSelect.clip = this.Resource.AudioSelect;

        this.audioSourceBGM = this.gameObject.AddComponent<AudioSource>();
        this.audioSourceBGM.clip = this.Resource.AudioBGM;
        this.audioSourceBGM.loop = true;

        this.soundData.PlayBGM(this.audioSourceBGM);


        this.DataRefresh();

        this.fade = GameObject.Find("FadeScreen").GetComponent<FadeController>();



        this.roomDataList = SaveData.GetClass<RoomDataList>("roomDataList", new RoomDataList());
        roomDataList.Rooms[this.RoomID_Now].Use();
        this.SaveRoomDataList();


        // 解放済みの部屋のリストを取得
        this.unlockedRooms = new List<RoomData>();
        foreach (RoomData checkRoom in this.roomDataList.Rooms)
        {
            if ( checkRoom.IsUnlocked )
                unlockedRooms.Add(checkRoom);
        }


        this.RoomID_Now = RoomData.RoomID_None;
        this.RoomID_Next = RoomData.RoomID_Entrance;
        this.RoomID_Last = RoomData.RoomID_None;

        this.CreateRoom();
    }


    public void DataRefresh()
    {
        this.clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());


        RoomDataList roomDataList = SaveData.GetClass<RoomDataList>("roomDataList", new RoomDataList());

        for (int roomID = 0; roomID < RoomData.RoomID_Max; roomID++)
        {
            roomDataList.Rooms[roomID].RoomID = roomID;

            if (RoomData.GetCanUnlock( roomID , this.clearStar.GetTotalStarCount() ) )
            {
                roomDataList.Rooms[roomID].Unlock();
            }
            else
            {
                roomDataList.Rooms[roomID].IsUnlocked = false;
            }
        }

        SaveData.SetClass<RoomDataList>("roomDataList", roomDataList);
        SaveData.Save();

    }

    public int GetTotalStarCount()
    {
        // TODO 毎回ロードする必要はないはず・・・
        this.clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());

        return this.clearStar.GetTotalStarCount();
    }

    private void SaveRoomDataList()
    {
        SaveData.SetClass<RoomDataList>("roomDataList", this.roomDataList);
        SaveData.Save();
    }


    // Update is called once per frame
    void Update()
    {
        if( this.room != null )
            this.room.RoomUpdate();


        if( this.roomStatus == RoomStatus.Moving && this.fade.IsBlackOut )
        {
            this.roomStatus = RoomStatus.Active;
            this.CreateRoom();
        }


        if( !this.selectedWaitTime.IsFinished )
        {
            this.selectedWaitTime.Update();

            if (this.selectedWaitTime.IsFinished)
            {
                this.OpenPlayConfirmMenu();
            }
        }
    }


    private void FixedUpdate()
    {
        if (this.room != null)
            this.room.RoomUpdateFinished();
    }


    public void SugorokuBar(GameObject menuBarObject)
    {

        if (this.SugorokuMenu.activeSelf || this.PlayConfirmMenu.activeSelf)
        {
            this.SugorokuMenu.SetActive(false);
            this.PlayConfirmMenu.SetActive(false);
            this.changeOnLabel(menuBarObject, "");
        }
        else
        {
            this.SugorokuMenu.SetActive(true);
            this.SugorokuMenu.GetComponent<SugorokuMenuController>().OpenSugorokuMenu();
            this.PlayConfirmMenu.SetActive(false);
            this.changeOnLabel(menuBarObject, "Sugoroku");
        }


        this.CardMenu.SetActive(false);
        this.CardDetailMenu.SetActive(false);
    }


    private void OpenPlayConfirmMenu()
    {
        
        this.SugorokuMenu.SetActive(false);
        this.PlayConfirmMenu.SetActive(true);
        this.PlayConfirmMenu.GetComponent<StageConfirmController>().Initalize(this.selectedStage1, this.selectedStage2);

        //this.SoundPlaySelect();
    }

    public void SugorokuScene(GameObject node)
    {
        // フレーム
        GameObject frame = node.transform.Find("Frame").gameObject;
        int stageNumber1 = int.Parse(node.transform.Find("StageNumber1").gameObject.GetComponent<Text>().text);
        int stageNumber2 = int.Parse(node.transform.Find("StageNumber2").gameObject.GetComponent<Text>().text);
        //Debug.Log("stage : " + stageNumber1 + "-" + stageNumber2);


        if (frame.GetComponent<StageFrameController>().Choice())
        {
            /*
            this.SugorokuMenu.SetActive(false);
            this.PlayConfirmMenu.SetActive(true);
            this.PlayConfirmMenu.GetComponent<StageConfirmController>().Initalize(stageNumber1, stageNumber2);

            this.SoundPlaySelect();
            */
        }
        else
        {
            ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());
            ClearStageStarData clearStageStar = clearStar.clearStageStarList[stageNumber1 - 1];

            // フレーム
            if (clearStageStar.canPlayStage(stageNumber2))
            {
                foreach (Transform child in node.transform.parent.transform)
                {
                    //Debug.Log("child:" + child.name);
                    if (child.name.Substring(0, 4) != "Node")
                    {
                        continue;
                    }

                    GameObject nodeFrame = child.transform.Find("Frame").gameObject;
                    nodeFrame.GetComponent<StageFrameController>().FrameOff();
                }

                //node.transform.Find("Frame").gameObject.SetActive(true);
                frame.GetComponent<StageFrameController>().FrameOn();

                // clear条件表示
                GameObject clearFrame = node.transform.parent.Find("Clear").gameObject;
                clearFrame.transform.Find("Title").gameObject.GetComponent<Text>().text = stageNumber1 + "-" + stageNumber2;

                StageDataTable stageData = getStageDataTable(stageNumber1, stageNumber2);
                for (int i = 1; i <= 3; i++)
                {
                    GameObject clearNode = clearFrame.transform.Find("Node" + i).gameObject;

                    Sprite spriteItemImage = Resources.Load("images/sugoroku/item/item_" + stageData.ExtraClearItem.ToString(), typeof(Sprite)) as Sprite;
                    clearNode.transform.Find("item").gameObject.GetComponent<Image>().sprite = spriteItemImage;

                    clearNode.transform.Find("amount").gameObject.GetComponent<Text>().text = (stageData.ClearItemCount[stageData.ExtraClearItemNo()] + i - 1).ToString();
                }

                this.SoundPlaySelect();

                this.selectedStage1 = stageNumber1;
                this.selectedStage2 = stageNumber2;
                this.selectedWaitTime.Start(0.3f);
            }
            else
            {
                Debug.Log("not active.");
            }

        }
    }

    public void CardBar(GameObject menuBarObject)
    {
        if (this.CardMenu.activeSelf || this.CardDetailMenu.activeSelf)
        {
            this.CardMenu.SetActive(false);
            this.CardDetailMenu.SetActive(false);
            this.changeOnLabel(menuBarObject, "");
        }
        else
        {
            this.CardMenu.SetActive(true);
            this.CardDetailMenu.SetActive(false);
            this.changeOnLabel(menuBarObject, "Card");
        }

        this.SugorokuMenu.SetActive(false);
        this.PlayConfirmMenu.SetActive(false);
    }

    public void ShopBar(GameObject menuBarObject)
    {
        changeOnLabel(menuBarObject, "Shop");
    }

    public void MenuBar(GameObject menuBarObject)
    {
        changeOnLabel(menuBarObject, "Menu");
    }

    private void changeOnLabel(GameObject menuBarObject, string menuName)
    {
        //Debug.Log("menuName:" + menuName);
        foreach (Transform child in menuBarObject.transform)
        {
            //Debug.Log("child:" + child.name);
            GameObject Label = child.transform.Find("Label").gameObject;
            //child is your child transform
            if (child.name == menuName)
            {
                //Debug.Log("true : " + Label.GetComponent<Text>().text);
                Label.GetComponent<Text>().color = new Color(242f / 255f, 255f / 255f, 227f / 255f);
            }
            else
            {
                //Debug.Log("false : " + Label.GetComponent<Text>().text);
                Label.GetComponent<Text>().color = new Color(56f / 255f, 156f / 255f, 116f / 255f);
            }
        }

    }

    public StageDataTable getStageDataTable(int stageNo1, int stageNo2)
    {
        return Resources.Load("data/StageData/Stage" + stageNo1.ToString().PadLeft(2, '0') + stageNo2.ToString().PadLeft(2, '0') + "Data") as StageDataTable;
    }

    public int ThinkRoomTarget()
    {
        return this.unlockedRooms[Random.Range(0, this.unlockedRooms.Count)].RoomID;
    }

    public void MoveRoom( int nextRoomID )
    {
        this.fade.FadeOut();
        this.roomStatus = RoomStatus.Moving;
        this.RoomID_Next = nextRoomID;
    }

    private void CreateRoom()
    {
        if( this.room != null )
        {
            this.room.RoomDestroyed();
        }

        switch( this.RoomID_Next )
        {
            case RoomData.RoomID_Entrance:
                this.room = new RoomEntrance();
                break;
            case RoomData.RoomID_Living:
                this.room = new RoomLiving();
                break;
            case RoomData.RoomID_Bedroom:
                this.room = new RoomBedroom();
                break;
        }

        this.RoomID_Last = this.RoomID_Now;
        this.RoomID_Now = this.RoomID_Next;
        this.RoomID_Next = RoomData.RoomID_None;

        this.room.RoomCreated();

        this.fade.FadeIn();

    }

    public bool RunDebugCommand(string[] cmds, IDebugCommandListener commandListener )
    {



        if (cmds.Length >= 2 && cmds[0].Equals("show") && cmds[1].Equals("status"))
        {
            string message = "";

            ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());


            RoomDataList roomDataList = SaveData.GetClass<RoomDataList>("roomDataList", new RoomDataList());
            message += "\n=== ルームリスト ===\n";

            for( int roomID= 0; roomID< RoomData.RoomID_Max; roomID++)
            {
                if( roomDataList.Rooms[roomID].IsUnlocked )
                    message += " " + roomID;
            }

            message += "\n\n";

            message += "\n=== ステージリスト ===\n";
            int newestStageNum1 = clearStar.clearStageStarList.Count - 1;
            int newestStageNum2 = clearStar.clearStageStarList[newestStageNum1].getNewestStageNumber();

            for (int stageNum1 = newestStageNum1; stageNum1 >= 0; stageNum1--)
            {
                ClearStageStarData clearStageStar = clearStar.clearStageStarList[stageNum1];

                int checkStageNum2 = newestStageNum2;
                if (stageNum1 < clearStar.clearStageStarList.Count - 1)
                    checkStageNum2 = ClearStageStarData.StageNumber2Max-1;

                for (int stageNum2 = checkStageNum2; stageNum2 >= 0; stageNum2--)
                {
                    message += (stageNum1 + 1).ToString() + " - " + (stageNum2 + 1).ToString() + " ";

                    int starNum = clearStageStar.clearStarList[stageNum2];

                    for (int starId = 1; starId <= starNum; starId++)
                        message += "★ ";

                    message += "\n";
                }
            }

            message += "\n";

            message +="★総得数 " + clearStar.GetTotalStarCount() + "\n";
            message += "\n";


            message += "\n=== 所有カードリスト ===\n";

            CardData cardData = SaveData.GetClass<CardData>("card", new CardData());

            string cardNums = "";
            int cardCount = 0;
            foreach (var v in cardData.CardNoSet)
            {
                cardNums += v.ToString() + " , ";
                cardCount++;
            }

            message += cardNums;
            message += "合計 " + cardCount + " 枚 \n\n";


            commandListener.Message(message);

            if (this.room != null)
            {
                this.room.RunDebugStatus(commandListener);
                commandListener.Message("\n=== ルーム状態 ===\n");
            }

            commandListener.Success(cmds);

            return true;
        }
        else if (cmds.Length >= 3 && cmds[0].Equals("get") && cmds[1].Equals("card"))
        {
            CardData cardData = SaveData.GetClass<CardData>("card", new CardData());


            string message = "";

            for (int id = 2; id < cmds.Length; id++)
            {

                if (cmds[id].Equals("all"))
                {
                    for (int check = 1; check < CardData.CardID_Max; check++)
                    {
                        if (cardData.CardNoSet.Contains(check))
                        {
                            message += "\tカードID " + check + " 既に所有しています。\n";
                        }
                        else
                        {
                            cardData.CardNoSet.Add(check);
                            message += "\tカードID " + check + " 取得しました。\n";
                        }
                    }
                }
                else
                {
                    int check;

                    try
                    {
                        check = int.Parse(cmds[id]);
                    }
                    catch
                    {
                        message += "\tカードID " + cmds[id] + " ID(数字)を指定してください。\n";
                        continue;
                    }

                    if (cardData.CardNoSet.Contains(check))
                    {
                        message += "\tカードID " + cmds[id] + " 既に所有しています。\n";
                    }
                    else
                    {
                        cardData.CardNoSet.Add(check);
                        message += "\tカードID " + cmds[id] + " 取得しました。\n";
                    }
                }
            }

            SaveData.SetClass<CardData>("card", cardData);

            commandListener.Message(message);
            commandListener.Success(cmds);

            return true;
        }

        else if (cmds.Length >= 3 && cmds[0].Equals("lost") && cmds[1].Equals("card"))
        {
            CardData cardData = SaveData.GetClass<CardData>("card", new CardData());


            string message = "";

            for (int id = 2; id < cmds.Length; id++)
            {

                if (cmds[id].Equals("all"))
                {
                    for (int check = 1; check < CardData.CardID_Max; check++)
                    {
                        if (!cardData.CardNoSet.Contains(check))
                        {
                            message += "\tカードID " + check + " 所有していません。\n";
                        }
                        else
                        {
                            cardData.CardNoSet.Remove(check);
                            message += "\tカードID " + check + " 失いました。\n";
                        }
                    }
                }
                else
                {
                    int check;

                    try
                    {
                        check = int.Parse(cmds[id]);
                    }
                    catch
                    {
                        message += "\tカードID " + cmds[id] + " ID(数字)を指定してください。\n";
                        continue;
                    }

                    if (!cardData.CardNoSet.Contains(check))
                    {
                        message += "\tカードID " + cmds[id] + " 所有していません。\n";
                    }
                    else
                    {
                        cardData.CardNoSet.Remove(check);
                        message += "\tカードID " + cmds[id] + " 失いました。\n";
                    }
                }
            }


            SaveData.SetClass<CardData>("card", cardData);

            commandListener.Message(message);
            commandListener.Success(cmds);

            return true;

        }
        else if (cmds.Length >= 3 && cmds[0].Equals("clear") && cmds[1].Equals("stage"))
        {
            int starCount = 1;
            if (cmds.Length >= 4)
            {
                if (cmds[3].Equals("*")) starCount = 1;
                else if (cmds[3].Equals("**")) starCount = 2;
                else if (cmds[3].Equals("***")) starCount = 3;
                else
                {
                    commandListener.Error(cmds, "★の数の指定が正しくありません。 " + cmds[3] + " * / ** / *** で指定してください。");
                    return true;
                }
            }

            ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());

            if (cmds[2].Equals("all"))
            {
                clearStar.clearStageStarList.Clear();
                for (int stageNum1 = 0; stageNum1 < ClearStarData.StageNumber1Max; stageNum1++)
                {
                    ClearStageStarData clearStageStar = new ClearStageStarData();

                    for (int stageNum2 = 0; stageNum2 < ClearStageStarData.StageNumber2Max; stageNum2++)
                    {
                        clearStageStar.clearStarList[stageNum2] = starCount;
                    }

                    clearStar.clearStageStarList.Add(clearStageStar);
                }
            }
            else
            {
                string[] stageNumStrs = cmds[2].Split('-');

                if (stageNumStrs.Length == 1)
                {
                    int stageNum1;
                    try
                    {
                        stageNum1 = int.Parse(stageNumStrs[0]);
                    }
                    catch
                    {
                        commandListener.Error(cmds, "ステージ番号を指定して下さい。 " + cmds[2] + " 2-3 / 2 / all");
                        return true;
                    }

                    if (ClearStarData.StageNumber1Max < stageNum1)
                    {
                        commandListener.Error(cmds, "ステージの最大は " + ClearStarData.StageNumber1Max + "です。");
                        return true;
                    }

                    if (stageNum1 >= clearStar.clearStageStarList.Count - 1)
                    {
                        ClearStageStarData clearStageStar = clearStar.clearStageStarList[clearStar.clearStageStarList.Count - 1];
                        for (int stageNum2Index = 0; stageNum2Index < ClearStageStarData.StageNumber2Max; stageNum2Index++)
                        {
                            if (clearStageStar.clearStarList[stageNum2Index] == 0)
                                clearStageStar.clearStarList[stageNum2Index] = starCount;
                        }
                    }

                    for (int stageNum1Index = clearStar.clearStageStarList.Count; stageNum1Index < stageNum1; stageNum1Index++)
                    {
                        ClearStageStarData clearStageStar = new ClearStageStarData();

                        for (int stageNum2Index = 0; stageNum2Index < ClearStageStarData.StageNumber2Max; stageNum2Index++)
                        {
                            clearStageStar.clearStarList[stageNum2Index] = starCount;
                        }

                        clearStar.clearStageStarList.Add(clearStageStar);
                    }
                }
                else if (stageNumStrs.Length == 2)
                {
                    int stageNum1, stageNum2;
                    try
                    {
                        stageNum1 = int.Parse(stageNumStrs[0]);
                        stageNum2 = int.Parse(stageNumStrs[1]);
                    }
                    catch
                    {
                        commandListener.Error(cmds, "ステージ番号を指定して下さい。 " + cmds[2] + " 2-3 / 2 / all");
                        return true;
                    }

                    if (ClearStarData.StageNumber1Max < stageNum1)
                    {
                        commandListener.Error(cmds, "ステージの最大は " + ClearStarData.StageNumber1Max + "です。");
                        return true;
                    }

                    if (ClearStageStarData.StageNumber2Max < stageNum2 || stageNum1 == 0 || stageNum2 == 0)
                    {
                        commandListener.Error(cmds, "ステージの指定に誤りがあります。" + cmds[2]);
                        return true;
                    }





                    //---------------------
                    //if (stageNum1  >= clearStar.clearStageStarList.Count-1)
                    {
                        ClearStageStarData clearStageStar;

                        if (stageNum1 == clearStar.clearStageStarList.Count)
                        {
                            clearStageStar = clearStar.clearStageStarList[clearStar.clearStageStarList.Count - 1];
                            for (int stageNum2Index = 0; stageNum2Index < stageNum2; stageNum2Index++)
                            {
                                if (clearStageStar.clearStarList[stageNum2Index] == 0)
                                    clearStageStar.clearStarList[stageNum2Index] = starCount;
                            }
                        }
                        else if (stageNum1 > clearStar.clearStageStarList.Count )
                        {
                            clearStageStar = clearStar.clearStageStarList[clearStar.clearStageStarList.Count - 1];
                            for (int stageNum2Index = 0; stageNum2Index < ClearStageStarData.StageNumber2Max; stageNum2Index++)
                            {
                                if (clearStageStar.clearStarList[stageNum2Index] == 0)
                                    clearStageStar.clearStarList[stageNum2Index] = starCount;
                            }
                        }

                        for (int stageNum1Index = clearStar.clearStageStarList.Count; stageNum1Index < stageNum1 ; stageNum1Index++)
                        {
                            clearStageStar = new ClearStageStarData();


                            if (stageNum1Index == stageNum1 - 1)
                            {
                                for (int stageNum2Index = 0; stageNum2Index < stageNum2; stageNum2Index++)
                                {
                                    clearStageStar.clearStarList[stageNum2Index] = starCount;
                                }
                            }
                            else
                            {
                                for (int stageNum2Index = 0; stageNum2Index < ClearStageStarData.StageNumber2Max; stageNum2Index++)
                                {
                                    clearStageStar.clearStarList[stageNum2Index] = starCount;
                                }
                            }
                            clearStar.clearStageStarList.Add(clearStageStar);
                        }

                        clearStageStar = clearStar.clearStageStarList[stageNum1 - 1];
                        clearStageStar.clearStarList[stageNum2-1] = starCount;
                    }

                    /*
                    if (stageNum1 > clearStar.clearStageStarList.Count)
                    {
                        ClearStageStarData clearStageStar = new ClearStageStarData();

                        for (int stageNum2Index = 0; stageNum2Index < stageNum2; stageNum2Index++)
                        {
                            clearStageStar.clearStarList[stageNum2Index] = starCount;
                        }

                        clearStar.clearStageStarList.Add(clearStageStar);
                    }
                    */

                }
                else
                {
                    commandListener.Error(cmds, "ステージ番号を指定して下さい。 " + cmds[2] + " 2-3 / 2 / all");
                    return true;
                }
            }

            if (clearStar.clearStageStarList.Count < ClearStarData.StageNumber1Max)
            {
                ClearStageStarData newestClearStageStar = clearStar.clearStageStarList[clearStar.clearStageStarList.Count - 1];
                if (newestClearStageStar.clearStarList[ClearStageStarData.StageNumber2Max - 1] != 0)
                    clearStar.clearStageStarList.Add(new ClearStageStarData());
            }

            SaveData.SetClass<ClearStarData>("clearStar", clearStar);
            commandListener.Success(cmds);

            return true;
        }
        else if (cmds.Length >= 3 && cmds[0].Equals("reset") && cmds[1].Equals("stage"))
        {
            ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());

            if (cmds[2].Equals("all"))
            {
                clearStar.clearStageStarList.Clear();
                clearStar.clearStageStarList.Add(new ClearStageStarData());
            }
            else
            {
                string[] stageNumStrs = cmds[2].Split('-');

                if( stageNumStrs.Length == 1 )
                {
                    int stageNum1;
                    try
                    {
                        stageNum1 = int.Parse(stageNumStrs[0]);
                    }
                    catch
                    {
                        commandListener.Error(cmds, "ステージ番号を指定して下さい。 " + cmds[2] + " 2-3 / 2 / all");
                        return true;
                    }

                    if (ClearStarData.StageNumber1Max <= stageNum1)
                    {
                        commandListener.Error(cmds, "ステージの最大は " + ClearStarData.StageNumber1Max + "です。");
                        return true;
                    }

                    while( clearStar.clearStageStarList.Count >= stageNum1 )
                    {
                        clearStar.clearStageStarList.RemoveAt(clearStar.clearStageStarList.Count-1);
                    }
                }
                else if ( stageNumStrs.Length == 2 )
                {
                    int stageNum1, stageNum2;
                    try
                    {
                        stageNum1 = int.Parse(stageNumStrs[0]);
                        stageNum2 = int.Parse(stageNumStrs[1]);
                    }
                    catch
                    {
                        commandListener.Error(cmds, "ステージ番号を指定して下さい。 " + cmds[2] + " 2-3 / 2 / all");
                        return true;
                    }

                    if (ClearStarData.StageNumber1Max < stageNum1)
                    {
                        commandListener.Error(cmds, "ステージの最大は " + ClearStarData.StageNumber1Max + "です。");
                        return true;
                    }

                    if (ClearStageStarData.StageNumber2Max < stageNum2 || stageNum1==0 || stageNum2==0)
                    {
                        commandListener.Error(cmds, "ステージの指定に誤りがあります。" + cmds[2]);
                        return true;
                    }

                    while (clearStar.clearStageStarList.Count > stageNum1)
                    {
                        clearStar.clearStageStarList.RemoveAt(clearStar.clearStageStarList.Count - 1);
                    }

                    if(clearStar.clearStageStarList.Count == stageNum1)
                    {
                        ClearStageStarData clearStageStar = clearStar.clearStageStarList[stageNum1 - 1];

                        for (int stageNum2Index = stageNum2 -1 ; stageNum2Index < ClearStageStarData.StageNumber2Max; stageNum2Index++)
                        {
                            clearStageStar.clearStarList[stageNum2Index] = 0;
                        }

                    }
                }
                else
                {
                    commandListener.Error(cmds, "ステージ番号を指定して下さい。 " + cmds[2] + " 2-3 / 2 / all");
                    return true;
                }
            }

            if (clearStar.clearStageStarList.Count < ClearStarData.StageNumber1Max)
            {
                ClearStageStarData newestClearStageStar = clearStar.clearStageStarList[clearStar.clearStageStarList.Count - 1];
                if (newestClearStageStar.clearStarList[ClearStageStarData.StageNumber2Max - 1] != 0)
                    clearStar.clearStageStarList.Add(new ClearStageStarData());
            }
            SaveData.SetClass<ClearStarData>("clearStar", clearStar);

            commandListener.Success(cmds);

            return true;

        }
        else if (cmds.Length >= 2 && cmds[0].Equals("say") )
        {
            int speechID;

            try
            {
                speechID = int.Parse(cmds[1]);
            }
            catch
            {
                commandListener.Error(cmds, "スピーチ番号を指定して下さい。 " + cmds[1] );
                return true;
            }


            ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());
            if (SpeechData.GetCanSpeech(speechID, clearStar.GetTotalStarCount()))
            {
                KumaForRoom kuma = KumaForRoom.GetInstance();
                kuma.Say(speechID);
                commandListener.Message("スピーチ番号 " + speechID.ToString() + " を話しました。");
            }
            else
            {
                commandListener.Message("スピーチ番号 " + speechID.ToString() + " を話すためには ★が足りません。");
            }

            commandListener.Success(cmds);

            return true;
        }
        else if(cmds[0].Equals("restart") )
        {
            SceneManager.LoadScene("HomeScene");

            commandListener.Success(cmds);

            return true;
        }
        else if (this.room != null && this.room.RunDebugCommand(cmds, commandListener) )
        {
            return true;
        }

        return false;
    }


    public GameObject InstantiateRoomObject(GameObject prehub , Vector3 pos )
    {
        return Instantiate(prehub,pos, Quaternion.identity);
    }

    public void SoundPlaySelect()
    {
        this.soundData.PlaySound(this.audioSourceSelect);
    }

}
