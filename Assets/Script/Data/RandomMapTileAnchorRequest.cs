using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapTileAnchorRequest 
{
    public TileDataIndex DataIndex { get; private set; }
    public Const.Direction Direction { get; private set; }

    public RandomMapTileAnchorRequest(TileDataIndex dataIndex, Const.Direction direction)
    {
        this.DataIndex = dataIndex;
        this.Direction = direction;
    }

}
