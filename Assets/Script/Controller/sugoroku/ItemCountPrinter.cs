using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCountPrinter : MonoBehaviour
{
    // Start is called before the first frame update    

    private int count;
    private Sprite[] numberSprites;

    private GameObject countObject;
    private SpriteRenderer countRender;

    private GameObject plusObject;
    private SpriteRenderer plusRender;

    public void Initalize( int count)
    {
        this.count = count;
        this.numberSprites = SugorokuDirector.GetInstance().Resource.SpritesNumberParts;

        this.countObject = this.transform.Find("count").gameObject;
        this.countRender = this.countObject.GetComponent<SpriteRenderer>();

        this.plusObject = this.transform.Find("plus").gameObject;
        this.plusRender = this.plusObject.GetComponent<SpriteRenderer>();

        if( count <= 1 )
        {
            this.countRender.enabled = false;
            this.plusRender.enabled = false;
        }

    }

    public void SetSortingOrder( int order )
    {
        this.plusRender.sortingOrder = order;
        this.countRender.sortingOrder = order;
    }

    void Start()
    {
        int count = this.count;

        this.countRender.sprite = this.numberSprites[count];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
