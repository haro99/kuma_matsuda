using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkController : MonoBehaviour
{
    public GameObject UIParts;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        gameObject.transform.position = player.transform.position;

        this.Dark(UIParts);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Dark(GameObject parts)
    {
        foreach (Transform child in parts.transform)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                //Debug.Log("r:" + image.color.r);
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a * 0.5f);
            }

            Dark(child.gameObject);
        }
    }
}
