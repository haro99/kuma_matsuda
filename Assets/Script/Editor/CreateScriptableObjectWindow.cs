using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateScriptableObjectWindow : EditorWindow
{
    [MenuItem("Window/CreateScriptableObject")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CreateScriptableObjectWindow));
    }

    public static void Create(string typeName, string path)
    {
        ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance(typeName), path);
    }

    public Object CreateObject;

    void OnGUI()
    {
        CreateObject = (Object)EditorGUILayout.ObjectField(CreateObject, typeof(Object), false);
        if (GUILayout.Button("Create!"))
        {
            string path = AssetDatabase.GetAssetPath(CreateObject.GetInstanceID());
            string typeName = CreateObject.name;
            if (path.EndsWith(".cs"))
            {
                CreateScriptableObjectWindow.Create(typeName, path.Substring(0, path.Length - 3) + ".asset");
            }
        }
    }
}
