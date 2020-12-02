using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{
    protected Image image = null;

    public ImageObject()
    {
    }

    protected void InitalizeSprite()
    {
        this.image = this.GetComponent<Image>();
    }

    public virtual void Start()
    {
        this.InitalizeSprite();
    }


    public float X
    {
        get
        {
            return this.transform.localPosition.x;
        }
    }


    public float Y
    {
        get
        {
            return this.transform.localPosition.y;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return this.image.sprite;
        }
        set
        {
            if (this.image != null)
                this.image.sprite = value;
        }
    }



    public Color Color
    {
        get
        {
            return this.image.color;
        }
    }

    public bool Visible
    {
        get
        {
            return this.image.enabled;
        }

        set
        {
            if (this.image != null)
                this.image.enabled = value;
        }
    }

    public void SetColor(float r, float g, float b, float a)
    {
        //this.render = this.GetComponent<SpriteRenderer>();
        this.image.color = new Color(r, g, b, a);
    }


    public void SetPosition(float x, float y)
    {

        Vector3 temp = this.transform.localPosition;
        temp.x = x;
        temp.y = y;
        temp.z = 0f;
        this.transform.localPosition = temp;
    }

    public void SetPosition(float x, float y , float z)
    {

        Vector3 temp = this.transform.localPosition;
        temp.x = x;
        temp.y = y;
        temp.z = z;
        this.transform.localPosition = temp;
    }

    public void SetPosition(Vector3 position)
    {
        this.SetPosition(position.x, position.y);
    }


    public void SetRotate(float z)
    {
        Vector3 worldAngle = transform.localEulerAngles;
        worldAngle.z = z * Mathf.Rad2Deg; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
        transform.localEulerAngles = worldAngle; // 回転角度を設定    
    }

    public void SetRotate(float x, float y, float z)
    {
        Vector3 worldAngle = transform.localEulerAngles;
        worldAngle.x = x * Mathf.Rad2Deg; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
        worldAngle.y = y * Mathf.Rad2Deg; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
        worldAngle.z = z * Mathf.Rad2Deg; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
        transform.localEulerAngles = worldAngle; // 回転角度を設定    
    }

    public float Rotate
    {
        get
        {
            return this.transform.eulerAngles.z * Mathf.Deg2Rad;
        }

        set
        {
            Vector3 worldAngle = this.transform.eulerAngles;
            worldAngle.z = value * Mathf.Rad2Deg;
            this.transform.eulerAngles = worldAngle;
        }
    }

    public Vector3 Scale
    {
        get
        {
            return this.transform.localScale;
        }

        set
        {
            this.transform.localScale = value;
        }
    }


    public void SetScale(float value)
    {
        this.SetScale(value, value);
    }

    public void SetScale(float x, float y)
    {
        Vector3 temp = this.transform.localScale;
        temp.x = x;
        temp.y = y;
        this.transform.localScale = temp;
    }

    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }



}
