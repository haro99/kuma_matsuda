using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetFoodData 
{
    //public int TileIndexX { get; private set; }
    //public int TileIndexY { get; private set; }
    public Const.Item Item { get; private set; }

    public GameObject tileObject { get; private set; }

    public EnemyGetFoodData( GameObject tileObject , Const.Item item )
    {
        //this.TileIndexX = tileIndexX;
        //this.TileIndexY = TileIndexY;
        this.Item = item;

        this.tileObject = tileObject;
    }

}
