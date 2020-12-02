using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusControlListener 
{
    void StatusChanged(int nextStatusID , int lastStatusID , StatusController status);
    void StatusFinished( int statusID , StatusController status);
    void StatusUpdate(int statusID , StatusController status);
}
