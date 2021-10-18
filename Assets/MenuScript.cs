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
        SceneManager.LoadScene("SugorokuScene");
    }
    public void BackHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }
}
