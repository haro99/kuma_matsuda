using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    public virtual object Args { get; set; }

    public static explicit operator SceneBase(GameObject v)
    {
        throw new NotImplementedException();
    }
}
