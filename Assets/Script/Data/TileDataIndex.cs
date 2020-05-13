using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDataIndex 
{
    public TileDataIndex(int x, int y) : this( x , y, 0 )
    {
        //this.SetMapIndex(x, y);
        //this.SetGoalDirection(0, 0, 999);
    }

    public TileDataIndex(int x, int y , int data )
    {
        this.SetMapIndex(x, y);
        this.MapData = data;
        this.SetGoalDirection(0, 0, 999);
    }

    public TileDataIndex(TileDataIndex index)
    {
        this.SetMapIndex(index);

    }

    public void SetMapIndex(int x, int y)
    {
        this.MapIndexX = x;
        this.MapIndexY = y;
        this.ClearRouteIndex = -1;
    }

    public void SetMapIndex( TileDataIndex index )
    {
        this.MapIndexX = index.MapIndexX;
        this.MapIndexY = index.MapIndexY;
    }

    [SerializeField]
    public int MapIndexX
    {
        get; set;
    }

    [SerializeField]
    public int MapIndexY
    {
        get; set;
    }

    public bool GetIsSameTile(TileDataIndex check)
    {
        
        if ( check == null)
            return false;

        if (this.MapIndexX == check.MapIndexX && this.MapIndexY == check.MapIndexY)
            return true;

        return false;        
    }

    public int ClearRouteIndex
    {
        get; set;
    }


    public void SetGoalDirection(int x, int y, int distacne)
    {
        this.GoalDirectionX = x;
        this.GoalDirectionY = y;
        this.GoalDistance = distacne;
    }


    public int MapData;

    public int GoalDistance;

    public int GoalDirectionX;
    public int GoalDirectionY;
}
