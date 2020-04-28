using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyDataTable : ScriptableObject
{

    [SerializeField]
    int minDice = 0, maxDice = 0;

    public int MinDice
    {
        get
        {
            return minDice;
        }
    }

    public int MaxDice
    {
        get
        {
            return maxDice;
        }
    }

}
