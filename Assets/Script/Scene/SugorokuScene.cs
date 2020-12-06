using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugorokuScene : SceneBase
{

    [SerializeField]
    private StageDataTable stageData;

    // Use this for initialization
    void Start () {
        Init();
	}

    private void Init()
    {
        // Scene引数の取得
        SugorokuSceneArgs args = base.Args as SugorokuSceneArgs;
        if (args == null)
        {
            args = new SugorokuSceneArgs(1, 1);
        }
        //Debug.Log(string.Format("stage : {0}-{1}", args.StageNo1, args.StageNo2));

        //StageDataの取得
        stageData = Resources.Load("data/StageData/Stage" + args.StageNo1.ToString().PadLeft(2, '0') + args.StageNo2.ToString().PadLeft(2, '0') + "Data") as StageDataTable;
    }

    public StageDataTable GetStageData()
    {
        return stageData;
    }
}
