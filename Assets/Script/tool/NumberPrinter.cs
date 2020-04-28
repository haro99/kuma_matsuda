using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPrinter : MonoBehaviour
{

    protected List<Image> ImageDigits;


    protected Sprite[] numberSprites;


    public NumberPrinter()
    {
        this.ImageDigits = new List<Image>();
    }


    public void SetNumber( int value )
    {
        string valueString = value.ToString();

        //int digitsID = this.ImageDigits.Count - 1;
        int digitsID = 0;

        for ( int strID=valueString.Length-1; strID>=0; strID--)
        {

            int digitValue = int.Parse(valueString.Substring(strID,1));
            this.ImageDigits[digitsID].sprite = numberSprites[digitValue];
            digitsID++;
        }

        for( int clearZeroID=digitsID; clearZeroID<this.ImageDigits.Count; clearZeroID++)
        {
            this.ImageDigits[clearZeroID].sprite = numberSprites[10];
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
