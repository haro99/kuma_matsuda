using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapTileAnchorData
{
    public bool IsPassableTop { get; private set; }

    public bool IsPassableBottom { get; private set; }

    public bool IsPassableLeft { get; private set; }

    public bool IsPassableRight { get; private set; }

    private MapController map;

    public int RouteCount;

    public int IndexX { get; private set; }
    public int IndexY { get; private set; }

    public RandomMapTileAnchorData( int x , int y , MapController map )
    {
        this.map = map;
        this.IndexX = x;
        this.IndexY = y;
        this.RouteCount = 0;
    }

    public void PassableRefresh()
    {
        this.RouteCount = 0;

        if (this.IndexX > 0  )
        {
            if( MapController.GetIsRouteMasu(this.map.getMapData(this.IndexX - 1, this.IndexY) ) )
            {
                this.IsPassableLeft = false;
                this.RouteCount++;
            }
            else if(this.map.getMapData(this.IndexX - 1, this.IndexY)==MapController.TileID_RandomTile )
                this.IsPassableLeft = true;
        }
        else
            this.IsPassableLeft = false;

        if (this.IndexX < this.map.TileXMax - 1 )
        {
            if( MapController.GetIsRouteMasu(this.map.getMapData(this.IndexX + 1, this.IndexY)) )
            {
                this.IsPassableRight = false;
                this.RouteCount++;
            }
            else if(this.map.getMapData(this.IndexX + 1, this.IndexY) == MapController.TileID_RandomTile)
                this.IsPassableRight = true;
        }
        else
            this.IsPassableRight = false;

        if (this.IndexY > 0 )
        {
            if( MapController.GetIsRouteMasu(this.map.getMapData(this.IndexX, this.IndexY - 1)) )
            {
                this.IsPassableTop = false;
                this.RouteCount++;
            }
            else if(this.map.getMapData(this.IndexX, this.IndexY - 1) == MapController.TileID_RandomTile )
                this.IsPassableTop = true;
        }
        else
            this.IsPassableTop = false;

        if (this.IndexY < this.map.TileYMax-1)
        {
            if (MapController.GetIsRouteMasu(this.map.getMapData(this.IndexX, this.IndexY + 1)))
            {
                this.IsPassableBottom = false;
                this.RouteCount++;
            }
            else if( this.map.getMapData(this.IndexX, this.IndexY + 1) == MapController.TileID_RandomTile )
                this.IsPassableBottom = true;
        }
        else
            this.IsPassableBottom = false;
    }


}
