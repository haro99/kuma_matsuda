using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInAnimationEventer : MonoBehaviour
{

    private CutInController cutInController;

    void Start()
    {
        GameObject cutInControlObject = GameObject.Find("CanvasCutIn");
        this.cutInController = cutInControlObject.GetComponent<CutInController>();
    }

    public void SoundPlay( string requestName )
    {
        this.cutInController.SoundPlay(requestName);
    }


}
