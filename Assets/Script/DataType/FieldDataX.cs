using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldDataX {

    [SerializeField]
    public List<int> fieldDataXList;

    public FieldDataX(List<int> fieldDataXList)
    {
        this.fieldDataXList = fieldDataXList;
    }
}
