using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialDiceFreeTitleFlush : MonoBehaviour
{
    private LimitTimeCounter activeTime;

    private Image image;
    // Start is called before the first frame update

    void Awake()
    {
        this.activeTime = new LimitTimeCounter();
        this.image = this.GetComponent<Image>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.activeTime.IsFinished)
        {
            this.activeTime.Update();

            if (this.activeTime.IsFinished)
                this.image.color = new Color(1f, 1f, 1f, 0f);
            else
            {
                this.image.color = new Color(1f, 1f, 1f, this.activeTime.Ratio);
                Vector3 temp = this.image.transform.localScale;
                temp.x = this.activeTime.RatioReverse * 0.2f + 1f;
                temp.y = this.activeTime.RatioReverse *0.2f + 1f;
                this.image.transform.localScale = temp;

            }
        }

    }

    public void Flush()
    {
        this.activeTime.Start(0.3f);
    }
}
