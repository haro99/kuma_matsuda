using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugorokuMenuController : MonoBehaviour
{


    private readonly int nodeMax = 5;
    private GameObject[] nodes;

    [SerializeField] Toggle m_Toggle;

	// Use this for initialization
	void Start ()
    {

        ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());
        //ClearStageStarData clearStageStar = clearStar.clearStageStarList[0];

        this.nodes = new GameObject[this.nodeMax];
        for (int nodeId = 0; nodeId < this.nodeMax; nodeId++)
        {
            this.nodes[nodeId] = this.transform.Find("Node" + (nodeId + 1)).gameObject;
        }

        // --------------------
        // プレイ可能な一番上のステージを見つける
        // --------------------
        int newestStageNum1 = clearStar.clearStageStarList.Count-1;
        int newestStageNum2 = clearStar.clearStageStarList[newestStageNum1].getNewestStageNumber();

        /*
        bool isHit = false;

        //for( int stageNumberCheck1=0; stageNumberCheck1< clearStar.clearStageStarList.Count; stageNumberCheck1++)
        for (int stageNumberCheck1 = clearStar.clearStageStarList.Count-1 ; stageNumberCheck1 >=0 && !isHit; stageNumberCheck1--)
        {
            ClearStageStarData checkStageData = clearStar.clearStageStarList[stageNumberCheck1];

            for (int stageNumberCheck2 = checkStageData.clearStarList.Count-1; stageNumberCheck2>=0 && !isHit; stageNumberCheck2--)
            {
                if( checkStageData.canPlayStage(stageNumberCheck2+1) )
                {
                    stageNum1 = stageNumberCheck1;
                    stageNum2 = stageNumberCheck2;
                    isHit = true;
                }
            }
        }
        */



        // --------------------
        // Nodeに当てはめる
        // --------------------
        int printedNodeCount = 0;
        for( int printStageNum1 = newestStageNum1; printStageNum1 >=0 && printedNodeCount<this.nodeMax ; printStageNum1 -- )
        {
            ClearStageStarData clearStageStar = clearStar.clearStageStarList[printStageNum1];

            int topStage2Num =  ClearStageStarData.StageNumber2Max-1;

            if (printStageNum1 == newestStageNum1)
                topStage2Num = newestStageNum2;

            for (int printStageNum2 = topStage2Num; printStageNum2 >= 0 && printedNodeCount < this.nodeMax ; printStageNum2--)
            {
                GameObject frame = this.nodes[printedNodeCount].transform.Find("Frame").gameObject;
                this.nodes[printedNodeCount].transform.Find("StageNumber1").gameObject.GetComponent<Text>().text = (printStageNum1+1).ToString();
                this.nodes[printedNodeCount].transform.Find("StageNumber2").gameObject.GetComponent<Text>().text = (printStageNum2+1).ToString();

                StageDataTable stageData = HomeDirector.GetInstance().getStageDataTable(printStageNum1+1, printStageNum2+1);
                

                this.nodes[printedNodeCount].transform.Find("DiceAmount").gameObject.GetComponent<Text>().text = (stageData.DiceCount).ToString();

                this.nodes[printedNodeCount].SetActive(true);

                // 星
                int starNum = clearStageStar.clearStarList[printStageNum2];
                GameObject star = this.nodes[printedNodeCount].transform.Find("Star").gameObject;
                Sprite spriteStarImage = Resources.Load("images/room/menu/sugoroku/room_star" + starNum, typeof(Sprite)) as Sprite;
                star.GetComponent<Image>().sprite = spriteStarImage;

                printedNodeCount++;
            }
        }

        for( int printNodeID=printedNodeCount;printNodeID<this.nodeMax;printNodeID++)
        {
            this.nodes[printNodeID].SetActive(false);
        }




        /*

        for (int i = 0; i < clearStageStar.clearStarList.Count; i++)
        {
            int starNum = clearStageStar.clearStarList[i];
            //Debug.Log("i:" + i + ", starNum:" + starNum);

            GameObject node = gameObject.transform.Find("Node" + (i + 1)).gameObject;

            // フレーム
            //GameObject Frame = node.transform.Find("Frame").gameObject;
            //Frame.SetActive(clearStageStar.canPlayStage((i + 1)));

            // 星
            GameObject star = node.transform.Find("Star").gameObject;
            Sprite spriteStarImage = Resources.Load("images/room/menu/sugoroku/room_star" + starNum, typeof(Sprite)) as Sprite;
            star.GetComponent<Image>().sprite = spriteStarImage;
        }
        */
    }

    //==========================================================================
    // メニューを閉じる
    //==========================================================================
    public void CloseSugorokuMenu()
    {
        m_Toggle.isOn = false;
    }
}
