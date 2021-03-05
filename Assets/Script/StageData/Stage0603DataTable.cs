using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0603Data", menuName = "Stage0603Data")]
public class Stage0603DataTable : StageDataTable
{
    public Stage0603DataTable()
    {
        this.DiceCount = 20;

        this.stageNo1 = 6;
        this.stageNo2 = 3;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.egg
        ;

        this.fieldDataYList = new List<FieldDataX>() 
        {

            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  7,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,100,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  8,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  7,  1,  1,  1,  1,  1,  1,  1,  1, -1,  1,  1,  1,  1,  1,  1,  1,  1,  7,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  8,  1,  8,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  5,  1,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  7,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
        };

    }
}
