using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionRotate : MonoBehaviour
{

    private float angleBase;
    private float timeSeed;


    // Start is called before the first frame update
    void Start()
    {
        this.angleBase = this.transform.rotation.eulerAngles.z;

    }

    // Update is called once per frame
    void Update()
    {
        this.timeSeed += 0.3f * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angleBase + Mathf.Cos(this.timeSeed) * 360f);

    }
}
