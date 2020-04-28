using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarTotalCountPrinter : NumberPrinter
{
    private HomeDirector director;

	// Use this for initialization
	void Start ()
    {
        this.director = HomeDirector.GetInstance();
        this.numberSprites = this.director.Resource.SpriteNumberParts;


        for( int id=1; id<5; id++ )
        {
            Image image = this.transform.Find("Image_Digit_" + id.ToString()).gameObject.GetComponent<Image>();
            this.ImageDigits.Add(image);
        }

        this.Refresh();
    }

    public void Refresh()
    {
        ClearStarData clearStar = SaveData.GetClass<ClearStarData>("clearStar", new ClearStarData());
        int totalStarCount = clearStar.GetTotalStarCount();
        this.SetNumber(totalStarCount);

    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
