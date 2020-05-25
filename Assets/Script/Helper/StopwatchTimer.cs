using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopwatchTimer 
{

    public float Time 
    {   
        get
        {
            return (float)this.stopWatch.Elapsed.TotalMilliseconds;
        }

        
    }

    private enum StatusType
    {
        Stop ,
        Active ,
    }

    private StatusType status;

    private System.Diagnostics.Stopwatch stopWatch;

    private void StatusChange( StatusType status )
    {
        this.status = status;

        switch( status )
        {
            case StatusType.Active:
                this.stopWatch.Start();
                break;
            case StatusType.Stop:
                this.stopWatch.Stop();
                break;
        }
    }

    public StopwatchTimer()
    {
        this.stopWatch = new System.Diagnostics.Stopwatch();
    }


    public void Start()
    {
        this.StatusChange(StatusType.Active);
    }

    public void Stop()
    {
        this.StatusChange(StatusType.Stop);
    }



}
