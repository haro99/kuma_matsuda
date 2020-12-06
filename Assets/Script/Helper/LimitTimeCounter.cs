using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitTimeCounter 
{
    public float activeTime { get; private set; }
    public float countTime { get; private set; }

    public LimitTimeCounter()
    {
        this.activeTime = 0f;
    }

    public void Start( float time )
    {
        this.countTime = time;
        this.activeTime = time;
    }

    public void Clear()
    {
        this.countTime = 0;
        this.activeTime = 0;
    }

    public float Ratio
    {
        get
        {
            return this.activeTime / this.countTime;
        }
    }

    public float RatioReverse
    {
        get
        {
            return 1f - this.activeTime / this.countTime;

        }
    }

    public bool IsFinished
    {
        get { return this.activeTime <= 0f; }
    }

    public void Update()
    {

        if (this.activeTime > 0f)
        {

            this.activeTime -= Time.deltaTime;

            if (this.activeTime <= 0f)
                this.activeTime = 0f;
        }
    }




}
