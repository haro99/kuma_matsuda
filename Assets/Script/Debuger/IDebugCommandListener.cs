using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebugCommandListener 
{
    void Error( string[] commandWords , string message );
    void Success(string[] commandWords, string message );
    void Success(string[] commandWords);
    void Message(string message);
}
