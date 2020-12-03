using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectPrefab 
{
    public int ObjectID { get; private set; }
    public GameObject Prefab { get; private set; }

    public MapObjectPrefab( int id  , GameObject prefab )
    {
        this.ObjectID = id;
        this.Prefab = prefab;
    }
}
