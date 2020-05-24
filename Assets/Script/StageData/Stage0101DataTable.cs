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

        this.clearItemCount = new int[4] { 9, 12, 3, 15 };
        this.extraClearItem = Const.Item.fish;

        //this.BackgroundPrefab = (GameObject)Resources.Load("prefabs/Map/MapStage0101");

        // ランダムマップ用
        //this.RandomMapClearRouteDistanceMax = 35;
        //this.RandomMapClearRouteTileCountMin = 10;

        /*
        this.fieldDataYList = new List<FieldDataX>()
        {
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  1,  1,  1,  8,  8,  8,  8,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  2,  2,  2,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0, -1,100,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  1,  1,  1,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
        };
        */


        
        this.fieldDataYList = new List<FieldDataX>()
        {
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,  0,110,110,110,110,110,110,110,110,110,110,110,  0,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,  0,  0,110,110,110,110,  0,110,110,110,110,  0,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,  0,110,  0,110,  0,110,110,110,110,  0,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,  0,110,110,  0,  0,  0,  0,  0,  0,  0,110,  0,  0,  0,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,111,  0,100,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,  0,110,  0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,111,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,  0,  0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,111,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,  0,110,  0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,  0,  0,  8,  8,  8,  8,  8,  8,  8,  8,  1,  1,  1,  1,  1,  1,  1, 41,  1, 45,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,110,  1,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,  8,  8,  8,  8,  8,  8,  8,  8,  1,  1,  1,  1,  1,  1,  1, 41,  1, 45,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,  0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,  0,  0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,  0,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,110,  0,  0,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,  0,110,  0,110,  0,110,  0,110,110,  0,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,  0,110,  0,110,110,110,110,  0,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
new FieldDataX(new List<int>(){ 110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,110,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),






        };
        


        /*
        this.fieldDataYList = new List<FieldDataX>()
        {
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
            new FieldDataX(new List<int>(){ 8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8}),
        };
        */
        /*
                this.fieldDataYList = new List<FieldDataX>() 
                {
                    new FieldDataX(new List<int>(){ 0,  0,  0, 30, 30, 30, 30, 30, 30,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  0,  0, 30, 30, 30, 30, 30,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0, 31,  1,  2,  2,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0, 31,  3, 32,  2,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  3,  3,  9,  1,  1,  1,  9,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  9,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  9,  4,  4,  4,  9,  9,  9,  9,  4,  4, 80,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  9,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  9,  0,  0,  0,  0,  0,  9,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  9,  0,  0,  0,  0,  0,  1, 33, 10,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  9,  0,  0,  0,  0, 33,  9,  0,  0,  0,  0,  2,  2,  2,  0,  0}),
                    new FieldDataX(new List<int>(){ 0, 12,  9,  4,  4,  9,  1,  9, -1,  9,  9,  9,  9,  1, 32,  1,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  9,  0,  0,  0,  0, 33,  9, 33,  0,  0,  0,  3,  3,  3,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0,  9, 30, 30,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0,  0, 80,  9,  9,  9,  2,  2,  9,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){30, 30, 30, 30, 30, 31,  2, 32,  3,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){30, 30, 30, 30,  0, 31,  1,  3,  3,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                    new FieldDataX(new List<int>(){ 0, 30, 30,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
                };
        */
        /* オリジナル
        this.fieldDataYList = new List<FieldDataX>() {
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  9,  3,  4, 11,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  2,  0,  0,  4,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  3,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  9,  3,  2,  1, -1,  1,  2,  9,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  4,  0,  0,  0,  4,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  1,  0, 10,  0,  3,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  2,  0,  0,  0,  2,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0, 11,  3,  4,  1,  9,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}),
            new FieldDataX(new List<int>(){ 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0})
        };
         */
    }
}
