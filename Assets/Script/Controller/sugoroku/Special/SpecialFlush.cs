using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialFlush : MonoBehaviour
{
    private Image image;
    private LimitTimeCounter flushTime;

    private int count;

    private void Awake()
    {
        this.image = this.GetComponent<Image>();
        this.flushTime = new LimitTimeCounter();

    }
    // Start is called before the first frame update



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if( !this.flushTime.IsFinished )
        {
            this.flushTime.Update();
            this.image.color = new Color(1f, 1f, 1f, this.flushTime.Ratio);


            if (this.flushTime.IsFinished)
            {
                this.count--;

                if (this.count > 0)
                {
                    this.flushTime.Start(0.3f);
                }
            }
        }
    }


    public void Flush()
    {
        this.Flush(1);
    }

    public void Flush( int count)
    {
        this.count = count;
        this.flushTime.Start(0.3f);
    }

}
