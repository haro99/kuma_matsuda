using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomDataList
{

    public RoomData[] Rooms;

    public RoomDataList()
    {
        this.Rooms = new RoomData[RoomData.RoomID_Max];

        for( int id=0; id< RoomData.RoomID_Max; id++ )
        {
            this.Rooms[id] = new RoomData( id );

            if (id == RoomData.RoomID_Entrance)
                this.Rooms[id].Unlock();
        }
    }
}
