using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CardData
{
    public static readonly int CardID_Ramen = 1; // ラーメン
    public static readonly int CardID_Omelette = 2; // オムライス
    public static readonly int CardID_Pizza = 3; // ピザ
    public static readonly int CardID_Steak = 4; // ステーキ
    public static readonly int CardID_Udon = 5; // うどん
    public static readonly int CardID_Spaghetti = 6; // スパゲティ
    public static readonly int CardID_Sashimi = 7; // さしみ
    public static readonly int CardID_Potatofries = 8; // ポテトフライ
    public static readonly int CardID_Hamburger = 9; // ハンバーグ
    public static readonly int CardID_Porkcutlet = 10; // とんかつ
    public static readonly int CardID_Friedchicken = 11; // からあげ
    public static readonly int CardID_Grilledmeat = 12; // 焼肉
    public static readonly int CardID_Curryrice = 13; // カレーライス
    public static readonly int CardID_Sushi = 14; // 寿司
    public static readonly int CardID_Friedegg = 15; // 卵焼き
    public static readonly int CardID_Sukiyaki = 16; // すき焼き
    public static readonly int CardID_Friedrice = 17; // チャーハン
    public static readonly int CardID_Soba = 18; // そば
    public static readonly int CardID_Grilledfish = 19; // 焼き魚
    public static readonly int CardID_Tempura = 20; // 天ぷら
    public static readonly int CardID_Stew = 21; // シチュー
    public static readonly int CardID_Sandwich = 22; // サンドイッチ
    public static readonly int CardID_Max = 23; 

    private int[] starCost;

    [SerializeField]
    public IntHashSet CardNoSet;

    public CardData ()
    {
        CardNoSet = new IntHashSet();

        this.starCost = new int[]
        {
            0  ,
            3  ,
            6  ,
            9  ,
            12 ,
            15 ,
            18 ,
            21 ,
            24 ,
            27 ,
            30 ,
            33 ,
            36 ,
            39 ,
            42 ,
            45 ,
            48 ,
            51 ,
            54 ,
            57 ,
            60 ,
            63 ,
            66 ,
        };
    }

    public int GetCardCost( int cardID )
    {
        return this.starCost[cardID];
    }

    public int GetNewestCardID( int totalStar )
    {
        int newestCardID = 0;

        for (int checkId = this.starCost.Length - 1; checkId >= 1; checkId--)
        {
            if (this.starCost[checkId] <= totalStar)
            { 
                newestCardID = checkId;
                break;
            }

        }

        return newestCardID;
    }
}
