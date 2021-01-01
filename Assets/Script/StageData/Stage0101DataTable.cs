using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage0101Data", menuName = "Stage0101Data")]
public class Stage0101DataTable : StageDataTable

{
    public Stage0101DataTable()
    {
        this.DiceCount = 20;

        this.stageNo1 = 1;
        this.stageNo2 = 1;

        this.clearItem = new Const.Item[4] { Const.Item.meat, Const.Item.egg, Const.Item.vegetables, Const.Item.fish };

        this.clearItemCount = new int[4] { 4, 4, 4, 4};
        this.extraClearItem = Const.Item.meat;

        this.fieldDataYList = new List<FieldDataX>() 
        {
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,113,113,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,113,113,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  0,113,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  8,113,113,113,  8,  1,  0,113,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  8,113,100,113,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  8,113,113,113,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  1,  1,  1,  1,  1, -1,  1,  1,  1,  1,  1,  1,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,113,113,113,  8,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,113,113,113,  8,  1,  0,113,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,113,113,113,  8,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  8,  8,  8,  8,  8,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,113,113,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,113,113,  0,  0,  0,  0,  0,  0}),
        };        
    }
}