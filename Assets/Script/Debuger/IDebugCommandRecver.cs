using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebugCommandRecver 
{
    bool RunDebugCommand( string[] commandWords , IDebugCommandListener commandListener );
}
