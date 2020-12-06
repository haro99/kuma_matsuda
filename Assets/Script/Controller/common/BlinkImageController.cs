using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImageController : MonoBehaviour
{

    Image text;
    [SerializeField]
    float m_AngularFrequency = 3.0f;
    [SerializeField]
    float m_DeltaTime = 0.0333f;

    float m_Time;
    float total;

    // Use this for initialization
    void Start()
    {
        text = this.GetComponent<Image>();
        m_Time = 0.0f;
        total = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;

        if (m_Time > m_DeltaTime)
        {
            var color = text.color;
            color.a = Mathf.Abs(Mathf.Sin(total));
            //Debug.Log("a : " + color.a);
            text.color = color;

            total += m_DeltaTime * m_AngularFrequency;
            m_Time = 0;
        }

        /*
        // 左クリックしたら、効果音を鳴らす
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();  // 効果音を鳴らす
        }
        */
    }
}
