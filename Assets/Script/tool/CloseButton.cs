using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : ButtonObject
{
    private ICloseButtonListener closeListener;

    public void SetCloseListener(ICloseButtonListener listener)
    {
        this.closeListener = listener;
    }

    public override void PointDown()
    {
        this.closeListener.Close();
    }

}
