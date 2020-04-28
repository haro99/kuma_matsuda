using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCostPrinter : NumberPrinter
{
    private HomeDirector director;

	// Use this for initialization
	void Awake ()
    {
        this.director = HomeDirector.GetInstance();
        this.numberSprites = this.director.Resource.SpriteNumberParts;


        for( int id=1; id<=2; id++ )
        {
            Image image = this.transform.Find("Image_Digit_" + id.ToString()).gameObject.GetComponent<Image>();
            this.ImageDigits.Add(image);
        }

        this.director = HomeDirector.GetInstance();
        this.numberSprites = this.director.Resource.SpriteNumberParts;

    }

    void Start()
    {
    }
    
    // Update is called once per frame

    void Update ()
    {
		
	}
}
