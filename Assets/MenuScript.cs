using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// すごろくステージでのメニュー
/// </summary>
public class MenuScript : MonoBehaviour
{

    public void TimeChange(float number)
    {
        Time.timeScale = number;
    }

    public void Rstart()
    {
        Time.timeScale = 1f;
        
        // 渡したい引数を定義
        SugorokuSceneArgs sugorokuSceneArgs = new SugorokuSceneArgs(SceneLoader.stageNo1, SceneLoader.stageNo2);
        // SceneLoaderクラス越しに引数を渡す
        SceneLoader.LoadScene("SugorokuScene", UnityEngine.SceneManagement.LoadSceneMode.Single, sugorokuSceneArgs);

    }
    public void BackHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }
}
