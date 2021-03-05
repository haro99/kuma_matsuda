using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0301Data", menuName = "Stage0301Data")]
public class Stage0301DataTable : StageDataTable
{
    public Stage0301DataTable()
    {
        this.DiceCount = 20;

        this.stageNo1 = 3;
        this.stageNo2 = 1;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.vegetables;

        this.fieldDataYList = new List<FieldDataX>() 
        {

            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,117,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  8,  0,  8,  0,  8,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,118,  0,  0,  8,  7,  1,  1,  1,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  8,  0,  8,  0,  1,  0,  0,118}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,117,  0,  0,  0,  0,  0,  0,  0,119,  0,  1,  0,119,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,100,  0,  0,  0,  0,  1,  0,  0,117}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  8,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,117,  0,  0,  0,  0,  0,  0,  8, -1,  1,  1,  1,  1,  1,  1,  8,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  8,  0,  0,  0,  0,  0,  1,  8,  0,  8,  0,  8,  8,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  8,  7,  8,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,117,  0,  0,  1,  0,  0,117,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  8,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  8,  0,  0,  0,  8,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){117,  0,  8,  1,  1,  1,  1,  1,  1,  1,  8,  0,  0,  0,  8,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  8,  8,  8,  0,  0,  0,  0,  0,  0,  0,  8,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,119,  0,  0,  0,  0,  0,  0,  0,119,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,119,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,118,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,117,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
        };

    }
}
