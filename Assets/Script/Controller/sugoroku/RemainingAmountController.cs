using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingAmountController : MonoBehaviour {

    int diceCount;

    // 1桁の場合
    public GameObject number0;

    // 2桁の場合
    public GameObject number1;
    public GameObject number2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initDiceCount(int diceCount)
    {
        this.diceCount = diceCount;
        this.changeDisp();
    }

    public void appendDiceCount(int count)
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
        this.clearDisp();

        if (diceCount >= 10)
        {
            // 数字を更新
            Sprite spriteCountImage1 = Resources.Load("images/text/number/blue/" + (diceCount / 10), typeof(Sprite)) as Sprite;
            number1.GetComponent<Image>().sprite = spriteCountImage1;

            Sprite spriteCountImage2 = Resources.Load("images/text/number/blue/" + (diceCount % 10), typeof(Sprite)) as Sprite;
            number2.GetComponent<Image>().sprite = spriteCountImage2;
        }
        else
        {
            // 数字を更新
            Sprite spriteCountImage0 = Resources.Load("images/text/number/blue/" + (diceCount % 10), typeof(Sprite)) as Sprite;
            number0.GetComponent<Image>().sprite = spriteCountImage0;
        }
    }

    public void clearDisp()
    {
        // 透明画像をセット
        Sprite spriteCountImage0 = Resources.Load("images/text/number/blue/-1", typeof(Sprite)) as Sprite;
        number0.GetComponent<Image>().sprite = spriteCountImage0;

        Sprite spriteCountImage1 = Resources.Load("images/text/number/blue/-1", typeof(Sprite)) as Sprite;
        number1.GetComponent<Image>().sprite = spriteCountImage1;

        Sprite spriteCountImage2 = Resources.Load("images/text/number/blue/-1", typeof(Sprite)) as Sprite;
        number2.GetComponent<Image>().sprite = spriteCountImage2;

    }
}
