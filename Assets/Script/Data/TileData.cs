using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public TileData( int x, int y , int data )
    {
        this.SetMapIndex(x, y , data );


    }

    public void SetMapIndex( int x, int y , int data  )
    {
        this.MapIndexX = x;
        this.MapIndexY = y;
        this.MapData = data;
    }

    public int MapIndexX;
    
    public int MapIndexY;

    public int MapData;


    public void SetGoalDirection( TileDataIndex index )
    {
        this.GoalDirectionX = index.GoalDirectionX;
        this.GoalDirectionY = index.GoalDirectionY;
        this.GoalDistance = index.GoalDistance;
    }

    public int GoalDistance;
    public int GoalDirectionX;
    public int GoalDirectionY;

}
