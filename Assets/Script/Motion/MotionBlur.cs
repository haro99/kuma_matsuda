using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBlur : MonoBehaviour
{
    // Start is called before the first frame update

    private float angleBase;
    private float timeSeed;

    private float step;
    private LimitTimeCounter stepChangeTime;

    void Start()
    {
        this.angleBase = this.transform.rotation.eulerAngles.z;
        this.timeSeed = 0f;

        this.step = Random.Range(1f, 3f);
        this.stepChangeTime = new LimitTimeCounter();
        this.stepChangeTime.Start(Random.Range(1, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if( this.stepChangeTime.IsFinished )
            this.step = Random.Range(1f, 4f);

        this.timeSeed += this.step * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angleBase + Mathf.Cos(this.timeSeed) * 15f) ;
    }
}
