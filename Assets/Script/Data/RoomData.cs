using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomData
{

    public static int RoomID_None = -1;
    public const int RoomID_Entrance = 0;
    public const int RoomID_Living = 1;
    public const int RoomID_Bedroom = 2;

    public static int RoomID_Max = 3;

    private static readonly int[] cost =
    {
        0 ,
        3,
        7,
    };

    public int RoomID;
    public bool IsUnlocked;

    public int UseCount;

    public RoomData( int roomID )
    {
        this.RoomID = roomID;
        this.IsUnlocked = false;
        this.UseCount = 0;
    }

    public void Unlock()
    {
        this.IsUnlocked = true;
    }

    public void Use()
    {
        this.UseCount++;
    }

    public static bool GetCanUnlock(int roomID, int starTotalCount)
    {
        if (roomID >= RoomData.cost.Length)
            return false;

        if (RoomData.cost[roomID] <= starTotalCount)
            return true;

        return false;
    }
}
