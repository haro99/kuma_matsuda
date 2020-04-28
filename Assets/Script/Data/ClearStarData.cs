using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClearStarData
{
    [SerializeField]
    public List<ClearStageStarData> clearStageStarList;

    public static readonly int StageNumber1Max = 9;
    public ClearStarData()
    {
        clearStageStarList = new List<ClearStageStarData>();

        // 1-x (4Stage)
        clearStageStarList.Add(new ClearStageStarData());
    }

    public int GetTotalStarCount()
    {
        int starCount = 0;

        foreach( ClearStageStarData stageGroup in this.clearStageStarList )
        {
            foreach( int stageStarCount in stageGroup.clearStarList )
            {
                starCount += stageStarCount;
            }
        }

        return starCount;
    }
}
