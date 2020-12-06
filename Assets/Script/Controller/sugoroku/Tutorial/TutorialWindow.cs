using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindow : MonoBehaviour , ICloseButtonListener
{
    // Start is called before the first frame update

    private SugorokuDirector director;


    void Start()
    {
        this.director = SugorokuDirector.GetInstance();

        CloseButton closeButton = this.transform.Find("CloseButton").GetComponent<CloseButton>();
        closeButton.SetCloseListener(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
        this.director.ClosedTutorial();
    }
}
