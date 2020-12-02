using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonForHome : CloseButton
{
    public GameObject CardMenu;
    public override void PointDown()
    {
        base.PointDown();
        //HomeDirector.GetInstance().SoundPlaySelect();
        CardMenu.SetActive(true);
    }

}
