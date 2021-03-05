using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0103Data", menuName = "Stage0103Data")]
public class Stage0103DataTable : StageDataTable
{
    public Stage0103DataTable()
    {
        this.DiceCount = 20;

        this.stageNo1 = 1;
        this.stageNo2 = 3;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4 };

        this.extraClearItem = Const.Item.vegetables;

        this.fieldDataYList = new List<FieldDataX>() {

new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,100,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0, -1,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  1,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0}),
new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0}),
new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0}),
new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0}),
new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0}),
new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  7,  5,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0}),


        };

    }
}
