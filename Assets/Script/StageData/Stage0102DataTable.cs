using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0102Data", menuName = "Stage0102Data")]
public class Stage0102DataTable : StageDataTable
{
    public Stage0102DataTable()
    {
        this.DiceCount = 20;

        this.stageNo1 = 1;
        this.stageNo2 = 2;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.egg;

        this.fieldDataYList = new List<FieldDataX>() 
        {

            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  0,112,112,112,  0,  1,  8,113,113,113,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  0,112,112,112,  0,  1,  8,113,100,113,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  0,112,112,112,  0,  1,  8,113,113,113,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  1,  1,  1,  1,  1, -1,  1,  1,  1,  1,  1,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,113,113,113,  8,  1,  0,112,112,112,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,113,113,113,  8,  1,  0,112,112,112,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,113,113,113,  8,  1,  0,112,112,112,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),


        };

    }
}
