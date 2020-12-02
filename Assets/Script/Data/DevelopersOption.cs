using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DevelopersOption 
{

    public float DiceStopWait; // サイコロが止まった後の待ち時間
    public float PlayerMoveStepWait;　// プレイヤの一歩進む度に止める待ち時間(秒)
    public float EnemyMoveStartWait; // 敵が移動する前の待ち時間(秒) = カメラ移動待ち
    public float EnemyMoveStepWait;　// 敵の一歩進む度に止める待ち時間(秒)
    public float EnemyMoveFinishWait;　// 敵の移動終了後の待ち時間(秒)
    public float SpecialSelectFinishWait; // スペシャルマス選択後の待ち時間（秒）

    public float SpecialRollMaxSpeed; // スペシャルスロットのロール最大速度

}
