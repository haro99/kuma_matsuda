using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingDiceController : MonoBehaviour
{
    int diceCount;

    public GameObject number;

    private GameObject number1;
    private GameObject number2;

    // Use this for initialization
    void Start()
    {
        number1 = number.transform.Find("Number1").gameObject;
        number2 = number.transform.Find("Number2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initDiceCount(int diceCount)
    {
        this.diceCount = diceCount;
        this.changeDisp();
    }

    public void appendDiceCount( int count )
    {
        this.diceCount += count;
        this.changeDisp();
    }

    public void decreaseDiceCount()
    {
        diceCount--;
        this.changeDisp();
    }

    void changeDisp()
    {
        // 動き
        Vector3 pos = number.transform.position;
        Vector3[] path = new Vector3[2];
        path[0] = new Vector3(pos.x, pos.y + 50.0f, 0);
        path[1] = new Vector3(pos.x, pos.y, 0);
        iTween.MoveTo(number, iTween.Hash("path", path, "delay", 0, "time", 0.5f));

        if (diceCount >= 10)
        {
            Sprite spriteCountImage1 = Resources.Load("images/text/number/white/" + (diceCount / 10), typeof(Sprite)) as Sprite;
            number1.GetComponent<Image>().sprite = spriteCountImage1;

            Sprite spriteCountImage2 = Resources.Load("images/text/number/white/" + (diceCount % 10), typeof(Sprite)) as Sprite;
            number2.GetComponent<Image>().sprite = spriteCountImage2;
        }
        else
        {
            if (diceCount < 4)
            {
                // 数字を更新
                Sprite spriteCountImage1 = Resources.Load("images/text/number/red/" + diceCount, typeof(Sprite)) as Sprite;
                number1.GetComponent<Image>().sprite = spriteCountImage1;
            }
            else
            {
                Sprite spriteCountImage1 = Resources.Load("images/text/number/white/" + diceCount, typeof(Sprite)) as Sprite;
                number1.GetComponent<Image>().sprite = spriteCountImage1;
            }

            Sprite spriteCountImage2 = Resources.Load("images/text/number/white/-1", typeof(Sprite)) as Sprite;
            number2.GetComponent<Image>().sprite = spriteCountImage2;
        }
    }

    public int getDiceCount()
    {
        return diceCount;
    }
}