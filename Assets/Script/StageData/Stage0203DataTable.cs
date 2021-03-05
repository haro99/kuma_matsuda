using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0203Data", menuName = "Stage0203Data")]
public class Stage0203DataTable : StageDataTable
{
    public Stage0203DataTable()
    {
        this.DiceCount = 20;

        this.stageNo1 = 2;
        this.stageNo2 = 3;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.egg;

        this.fieldDataYList = new List<FieldDataX>() 
        {

            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,115,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,115,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,114,114,  0,100,  0,  0,  0,  0,115,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,114,  0,  8,  0,  0,  0,  0,  0,  0,  0,  8,  0,  0,116,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  8,  1,  1,  1,  1, -1,  1,  1,  1,  1,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,116,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,114,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,115,  0,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,115,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  1,  0,  1,  0,  7,  5,  1,  0,  1,  0,114,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,114,  0,  0,  1,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  1,  0,  1,  1,  1,  1,  1,  1,  1,  8,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,115,  0,  8,  7,  0,  0,  0,  0,  0,  0,  0,  8,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  8,  0,  0,  0,  0,  0,  0,  0,  0,115,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,114,  0,  0,  0,  0,114,116,  0,  0,114,114,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
        };

    }
}
