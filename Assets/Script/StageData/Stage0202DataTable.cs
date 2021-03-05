using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0202Data", menuName = "Stage0202Data")]
public class Stage0202DataTable : StageDataTable
{
    public Stage0202DataTable()
    {
        this.DiceCount = 20;

        this.stageNo1 = 2;
        this.stageNo2 = 2;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.meat;

        this.fieldDataYList = new List<FieldDataX>() 
        {

            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,114,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,114,  0,  0,  0,  0,115,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,115,  0,  0,  0,  0,  0,114,  0,  0,115,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,116,  0,  0,  0,114,  0,100,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){115,  0,  0,  0,  0,  0,  0,  0,  0,  0,  8,  8,  8,  8,  8,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  1,  1,  1,  1,  1, -1,  1,  1,  1,  1,  1,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,114,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,116,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,115}),
            new FieldDataX(new List<int>(){  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){115,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){114,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,  0,  0,  0,114,  0,  0,  0,  0,116,  0,  0,  0,115,  0,  0}),
            new FieldDataX(new List<int>(){  0,  0,  0,  0,115,  0,  0,  0,  0,  0,  0,  0,  0,  0,114,  0,  0,  0,  0}),
        };

    }
}
