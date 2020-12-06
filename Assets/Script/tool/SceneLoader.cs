using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader
{
    static Dictionary<string, object> sceneArgsDic = new Dictionary<string, object>();

    static public void LoadScene(string sceneName, LoadSceneMode mode, object args)
    {
        // 読み込むシーンと引数をキャッシュ
        sceneArgsDic.Add(sceneName, args);

        // シーン読み込み, イベント追加
        SceneManager.LoadScene(sceneName, mode);
        SceneManager.sceneLoaded += OnLoadedScene;
    }

    static void OnLoadedScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        //Debug.Log("scene:" + scene.name);
        if (!sceneArgsDic.ContainsKey(scene.name))
        {
            return;
        }
 
        // キャッシュから引数取り出し
        var args = sceneArgsDic[scene.name];
        sceneArgsDic.Remove(scene.name);

        // GameObject取得
        GameObject loadedSceneObj = null;
        foreach (var obj in scene.GetRootGameObjects())
        {
            //Debug.LogFormat("RootObject = {0}", obj.name);
            if (scene.name == obj.name) {
                loadedSceneObj = obj;
            }
        }

        if (loadedSceneObj == null)
        {
            return;
        }

        // 引数渡す、初期化
        //Debug.Log("loadedSceneObj:" + loadedSceneObj.name);
        SceneBase sceneObj = loadedSceneObj.GetComponent<SceneBase>();
        sceneObj.Args = args;
    }
}
