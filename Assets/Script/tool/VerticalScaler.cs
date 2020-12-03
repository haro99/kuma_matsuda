using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScaler : MonoBehaviour
{
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        this.rectTransform = this.GetComponent<RectTransform>();

        this.Refresh();
    }


    private void Refresh()
    {
        float scale = (float)Screen.width / rectTransform.rect.width;

        if( this.transform.localScale.x != scale )
            this.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private void LateUpdate()
    {
        this.Refresh();
    }

}
