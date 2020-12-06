using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageConfirmController : MonoBehaviour
{

    private StageDataTable stageData;
    private HomeDirector director;

    private int stageNo1, stageNo2;

    [SerializeField] Toggle m_Toggle;

    public void Initalize( int stageNo1 , int stageNo2 )
    {
        this.stageNo1 = stageNo1;
        this.stageNo2 = stageNo2;


        this.stageData = this.getStageDataTable(stageNo1, stageNo2);
        this.director = HomeDirector.GetInstance();


        for (int i = 1; i <= 3; i++)
        {
            GameObject clearNode = this.transform.Find("Node" + i).gameObject;

            Sprite spriteItemImage = Resources.Load("images/sugoroku/item/item_" + stageData.ExtraClearItem.ToString(), typeof(Sprite)) as Sprite;
            clearNode.transform.Find("item").gameObject.GetComponent<Image>().sprite = spriteItemImage;

            clearNode.transform.Find("amount").gameObject.GetComponent<Text>().text = (stageData.ClearItemCount[stageData.ExtraClearItemNo()] + i - 1).ToString();
        }

        for (int i = 1; i <= 4; i++)
        {
            // Const.Item
            // meat = 1,
            // egg = 2,
            // vegetables = 3,
            // fish = 4,
            // this.stageData.ClearItem[0];

            AmountPrinter amountPrinter = this.transform.Find("Item" + i).GetComponent<AmountPrinter>();

            amountPrinter.Value = this.stageData.ClearItemCount[i-1];
            Sprite spriteItemImage = Resources.Load("images/sugoroku/item/item_" + this.stageData.ClearItem[i - 1].ToString(), typeof(Sprite)) as Sprite;
            amountPrinter.Image = spriteItemImage;
        }

    }

    private StageDataTable getStageDataTable(int stageNo1, int stageNo2)
    {
        return Resources.Load("data/StageData/Stage" + stageNo1.ToString().PadLeft(2, '0') + stageNo2.ToString().PadLeft(2, '0') + "Data") as StageDataTable;
    }


    public void StageStart()
    {
        // メニューを非表示
        //this.SetActive(false);

        // 渡したい引数を定義
        SugorokuSceneArgs sugorokuSceneArgs = new SugorokuSceneArgs(this.stageNo1, this.stageNo2);
        // SceneLoaderクラス越しに引数を渡す
        SceneLoader.LoadScene("SugorokuScene", UnityEngine.SceneManagement.LoadSceneMode.Single, sugorokuSceneArgs);
        //SceneManager.LoadScene("SugorokuScene");    }
        // Use this for initialization

        this.director.SoundPlaySelect();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //==========================================================================
    // メニューを閉じる
    //==========================================================================
    public void CloseStageConfirm()
    {
        m_Toggle.isOn = false;
    }
}
