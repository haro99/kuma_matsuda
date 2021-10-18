using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablObject : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetPosition( float x , float y )
    {
        Vector3 temp = this.transform.position;

        temp.x = x;
        temp.y = y;

        this.transform.position = temp;
    }

    public void SetScale(float x, float y )
    {
        Vector3 temp = this.transform.localScale;

        temp.x = x;
        temp.y = y;

        this.transform.localScale = temp;
    }

    public void SetRotate(float x, float y,float z)
    {
        this.transform.rotation = Quaternion.Euler(x,y,z);
    }

    public void Move( float targetX , float targetY , float moveDistance )
    {
        float w = this.X - targetX;
        float h = this.Y - targetY;
        float a = Mathf.Atan2(h, w) + Mathf.PI;

        //Debug.Log("動きました");
        this.SetPosition(this.X + Mathf.Cos(a) * moveDistance * Time.deltaTime, this.Y + Mathf.Sin(a) * moveDistance * Time.deltaTime);
    }

    public float X
    {
        get
        {
            return this.transform.position.x;
        }
    }

    public float Y
    {
        get
        {
            return this.transform.position.y;
        }
    }



}
