using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugorokuSceneArgs {

    public SugorokuSceneArgs(int stageNo1, int stageNo2)
    {
        this.StageNo1 = stageNo1;
        this.StageNo2 = stageNo2;
    }

    // SugorokuSceneロード時渡したいやつ
    public int StageNo1 { get; set; }
    public int StageNo2 { get; set; }
}
