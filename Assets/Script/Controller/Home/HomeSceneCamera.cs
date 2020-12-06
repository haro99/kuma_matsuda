using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneCamera : MonoBehaviour
{
    public Camera camera;
    public float baseWidth = 300.0f;
    public float baseHeight = 600.0f;

    void Awake()
    {

        camera = GetComponent<Camera>();

        var scaleWidth = (Screen.height / this.baseHeight) * (this.baseWidth / Screen.width);
        this.camera.fieldOfView = Mathf.Atan(Mathf.Tan(this.camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * scaleWidth) * 2.0f * Mathf.Rad2Deg;
    }


}
