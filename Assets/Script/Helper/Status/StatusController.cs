using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController 
{
    private IStatusControlListener listener;

    public float ActiveTime { get; private set; }

    public LimitTimeCounter Timer
    {
        get; private set;
    }

    public StatusController(IStatusControlListener listener )
    {
        this.listener = listener;
        this.Timer = new LimitTimeCounter();
        this.ActiveTime = 0f;
    }

    public int StatusID { get; private set; }

    public void Change(int id)
    {
        this.Change(id, 0f);
    }

    public void Change( int id , float activeTime )
    {
        int lastID = this.StatusID;
        this.StatusID = id;

        if( activeTime > 0f )
            this.Timer.Start(activeTime);
        this.listener.StatusChanged(this.StatusID,lastID, this);

        this.ActiveTime = 0f;

    }

    public void Update()
    {
        this.ActiveTime += Time.fixedUnscaledDeltaTime;

        if( !this.Timer.IsFinished )
        {
            this.Timer.Update();

            this.listener.StatusUpdate(this.StatusID, this);

            if ( this.Timer.IsFinished )
                this.listener.StatusFinished(this.StatusID, this);
        }
        else
            this.listener.StatusUpdate(this.StatusID,this);
    }



}
