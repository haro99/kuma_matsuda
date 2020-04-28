using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : IDebugCommandRecver
{
    /*
    public enum RoomType
    {
        None ,

        Entrance,
        Living,
        Bedroom
    }
    */



    public static float LineY_Kuma_ActiveLine = -0.5f;
    public static float LineX_Kuma_OutLine_Left = -6f;
    public static float LineX_Kuma_OutLine_Right = 6f;

    protected SpriteRenderer backgroundRenderer;

    protected HomeDirector director;

    protected KumaForRoom kuma;

    protected List<GameObject> roomObject;

    public Room()
    {

        this.roomObject = new List<GameObject>();
        this.director = HomeDirector.GetInstance();
        this.kuma = KumaForRoom.GetInstance();

        GameObject roomBackgroundObject = GameObject.Find("RoomBackground");
        this.backgroundRenderer = roomBackgroundObject.GetComponent<SpriteRenderer>();


        //this.RoomCreated();
    }

    public abstract void RoomCreated();
    public abstract void RoomUpdate();
    public abstract void RoomUpdateFinished();
    public abstract void RoomDestroyed();

    public abstract bool RunDebugCommand(string[] commandWords , IDebugCommandListener commandListener);
    public abstract void RunDebugStatus( IDebugCommandListener commandListener);


    protected void AddRoomObject( GameObject obj )
    {
        this.roomObject.Add( obj );
    }

    protected void DeleteRoomObjectAll()
    {
        foreach (GameObject obj in this.roomObject)
            GameObject.Destroy(obj);
    }
}
