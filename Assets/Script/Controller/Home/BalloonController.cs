using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonController : MonoBehaviour
{
    [SerializeField]
    float mTime;

    // Use this for initialization
    void Start()
    {
        mTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mTime += Time.deltaTime;

        if (mTime > 10)
        {
            int value = Random.Range(1, 5);

            switch (value)
            {
                case 1:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("images/room/balloon/light_blue", typeof(Sprite)) as Sprite;
                    break;
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("images/room/balloon/orange", typeof(Sprite)) as Sprite;
                    break;
                case 3:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("images/room/balloon/pink", typeof(Sprite)) as Sprite;
                    break;
                default:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("images/room/balloon/clear", typeof(Sprite)) as Sprite;
                    break;
            }

            mTime = 0;
        }
    }
}
