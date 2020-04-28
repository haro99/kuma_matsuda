using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClearStageStarData
{

    [SerializeField]
    public List<int> clearStarList;

    public static readonly int StageNumber2Max = 3;

    public ClearStageStarData()
    {
        clearStarList = new List<int>();

        // 1-x (3Stage)
        clearStarList.Add(0); // TODO
        clearStarList.Add(0);
        clearStarList.Add(0);

    }

    public bool isClearStage(int stageNo)
    {
        if (!checkStage(stageNo))
        {
            return false;
        }

        return (clearStarList[stageNo - 1] > 0);
    }

    public bool canPlayStage(int stageNo)
    {
        if (!checkStage(stageNo))
        {
            return false;
        }

        // stage x-1 ならOK
        if (stageNo == 1)
        {
            return true;
        }

        // 現在のstageはクリア済み
        if (clearStarList[stageNo - 1] != 0) {
            return true;
        }

        // 1つ前のstageはクリア済みであること
        if (clearStarList[stageNo - 2] != 0)
        {
            // 現在のstageはクリア済みでない
            return (clearStarList[stageNo - 1] == 0);
        }

        return false;
    }

    private bool checkStage(int stageNo)
    {
        if (stageNo > clearStarList.Count)
        {
            return false;
        }

        return true;
    }

    public void setClearStarNum(int stageNo, int starNum)
    {
        //Debug.Log("stageNo :" + stageNo);
        if (!checkStage(stageNo))
        {
            Debug.Log("setClearStarNum: NG.");
            return;
        }

        //Debug.Log("setClearStarNum: [stageNo[" + (stageNo - 1) + "] : " + starNum);
        clearStarList[stageNo - 1] = starNum;

    }

    public int getNewestStageNumber()
    {

        bool isHit = false;

        int newestStageNumber = 0;

        for (int stageNumberCheck2 = this.clearStarList.Count - 1; stageNumberCheck2 >= 0 && !isHit; stageNumberCheck2--)
        {

            if ( this.canPlayStage(stageNumberCheck2 + 1))
            {
                newestStageNumber = stageNumberCheck2;
                isHit = true;
            }
        }

        return newestStageNumber;
    }
}
