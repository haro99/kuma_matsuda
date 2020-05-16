using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataTable : ScriptableObject
{
    [SerializeField]
    protected int stageNo1;
    [SerializeField]
    protected int stageNo2;
    [SerializeField]
    protected Const.Item[] clearItem;
    [SerializeField]
    protected int[] clearItemCount;
    [SerializeField]
    protected Const.Item extraClearItem;

    [SerializeField]
    protected List<FieldDataX> fieldDataYList;

    public int StageNo1 { get { return stageNo1; } }

    public int StageNo2 { get { return stageNo2; } }

    public Const.Item[] ClearItem { get { return clearItem; } }

    public int[] ClearItemCount { get { return clearItemCount; } }

    public Const.Item ExtraClearItem { get { return extraClearItem; } }

    public List<FieldDataX> FieldDataYList { get { return fieldDataYList; } }

    //public int DiceCount;

    [SerializeField]
    public GameObject BackgroundPrefab;

    [SerializeField]

    public int RandomMapClearRouteTileCountMin;
    public int RandomMapClearRouteDistanceMax;
    public int RandomMapItemCountMaxInTile;

    public int RandomMapEnemyCount;
    public int RandomMapSpecailCount;
    public int RandomMapRandomItemCount;

    public List<MapObjectParameter> RandomSpotsObjects;
    public List<MapObjectClusterParameter> RandomClusterObjects;

    public StageDataTable()
    {
        //this.DiceCount = 10;
        this.BackgroundPrefab = null;
        this.RandomMapClearRouteTileCountMin = 20;
        this.RandomMapClearRouteDistanceMax = 50;
        this.RandomMapItemCountMaxInTile = 3;

        this.RandomMapRandomItemCount = 1;
        this.RandomMapEnemyCount = 1;
        this.RandomMapSpecailCount = 3;

        this.RandomSpotsObjects = new List<MapObjectParameter>();
    }


    public int[] GetStartPos()
    {
        // start
        for (int i = 0; i < FieldDataYList.Count; i++)
        {
            for (int j = 0; j < FieldDataYList[i].fieldDataXList.Count; j++)
            {
                if (FieldDataYList[i].fieldDataXList[j] == -2)
                {
                    return new int[] { j, i };
                }
            }
        }

        // goal
        for (int i = 0; i < FieldDataYList.Count; i++)
        {
            for (int j = 0; j < FieldDataYList[i].fieldDataXList.Count; j++)
            {
                if (FieldDataYList[i].fieldDataXList[j] == -1)
                {
                    return new int[] { j, i };
                }
            }
        }
        return new int[] { 8, 9 };
    }

    public int ExtraClearItemNo()
    {
        for (int i = 0; i < ClearItem.Length; i++)
        {
            if (extraClearItem == ClearItem[i]) {
                return i;
            } 
        }

        Debug.Log("error");
        return -1;
    }

}
