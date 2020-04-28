using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{

    GameObject charaObject;

    private LimitTimeCounter firstTimeCounter;

    // Use this for initialization
    void Start()
    {
        charaObject = GameObject.Find("Player");
        this.firstTimeCounter = new LimitTimeCounter();
        this.firstTimeCounter.Start(5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (charaObject != null)
        {
            Vector3 movePos = new Vector3(charaObject.transform.position.x, charaObject.transform.position.y, transform.position.z);

            if( !this.firstTimeCounter.IsFinished )
            {
                this.firstTimeCounter.Update();
                transform.position = movePos;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * 5);
            }
        }
        else
        {
            charaObject = GameObject.Find("Player");
        }
    }

    public void setCharaObject(GameObject charaObject)
    {
        this.charaObject = charaObject;
    }
}
