using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechData 
{
    public static readonly int SpeechID_None = 0;
    public static readonly int SpeechID_Hello = 1; // こんにちは
    public static readonly int SpeechID_Bored = 2; // ひまだなぁ
    public static readonly int SpeechID_Spleepy = 3; // 眠い
    public static readonly int SpeechID_Zzz = 4; // zzz...
    public static readonly int SpeechID_Hungry = 5; // お腹減った

    private static readonly int[] cost =
    {
        0 ,
        1,//ID_Hello = 1; // こんにちは
        2,//ID_Bored = 2; // ひまだなぁ
        5,//ID_Spleepy = 3; // 眠い
        7,//ID_Zzz = 4; // zzz...
        9,//ID_Hungry= 5; // お腹減った
    };
    
    public static bool GetCanSpeech(int speechID , int starTotalCount )
    {
        if (speechID >= SpeechData.cost.Length)
            return false;

        if (SpeechData.cost[speechID] <= starTotalCount)
            return true;

        return false;
        //return true;
    }

    /*
    public static Sprite GetSprite( int speechID )
    {
        return Resources.Load("images/room/balloon/" + speechID.ToString() , typeof(Sprite)) as Sprite;
    }
    */

}
