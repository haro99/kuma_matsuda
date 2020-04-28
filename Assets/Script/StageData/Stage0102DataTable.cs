using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0102Data", menuName = "Stage0102Data")]
public class Stage0102DataTable : StageDataTable
{
    public Stage0102DataTable()
    {
        this.stageNo1 = 1;
        this.stageNo2 = 2;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.egg;

        this.fieldDataYList = new List<FieldDataX>() {
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 11,  9,  9,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  9,  0,  9,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  4,  9,  3,  9,  2,  9,  1,  9,  9, 11,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  4,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  0,  0,  0,  0,  0,  0,  0,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  3,  0,  0,  0,  0,  0,  0,  0,  2,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  0,  0, 10,  0, 10,  0,  0,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  2,  0,  0,  0,  0,  0,  0,  0,  3,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  0,  0,  0,  0,  0,  0,  0,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  4,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0, -1,  4,  9,  3,  9,  2,  9,  1,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0})
        };

    }
}
