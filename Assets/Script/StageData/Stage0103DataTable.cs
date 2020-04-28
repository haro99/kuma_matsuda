using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0103Data", menuName = "Stage0103Data")]
public class Stage0103DataTable : StageDataTable
{
    public Stage0103DataTable()
    {
        this.stageNo1 = 1;
        this.stageNo2 = 3;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.vegetables;

        this.fieldDataYList = new List<FieldDataX>() {
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 11,  9, 11,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  9,  0,  9,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  4,  9,  3,  9,  2,  9,  1, 11,  9, 11,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  4,  0,  0,  4,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  0,  0,  9,  0,  0,  0,  0,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  3,  0,  0,  3,  0,  0,  0,  0,  2,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  1,  2, 80, -1,  3, 11,  4,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  2,  0,  0,  2,  0,  0,  0,  0,  3,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  0,  0,  9,  0,  0,  0,  0,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  1,  0,  0,  1,  0,  0,  0,  0,  4,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0, -2,  4,  9,  3,  9,  2,  9,  1,  9,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0})
        };

    }
}
