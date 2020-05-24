using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteObject : MonoBehaviour
{

    protected SpriteRenderer render = null;

    public SpriteObject()
    {
    }

    public virtual void Start()
    {
        this.render = this.GetComponent<SpriteRenderer>();
       

    }


    public float X
    {
        get
        {
            return this.transform.position.x;
        }
    }


    public float Y
    {
        get
        {
            return this.transform.position.y;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return this.render.sprite;
        }
        set
        {
            if (this.render != null)
                this.render.sprite = value;
        }
    }

    public Color Color
    {
        get
        {
            return this.render.color;
        }
    }

    public bool Visible
    {
        get
        {
            return this.render.enabled;
        }

        set
        {
            if( this.render != null )
                this.render.enabled = value;
        }
    }

    public void SetColor( float r , float g , float b , float a )
    {
        //this.render = this.GetComponent<SpriteRenderer>();
        this.render.color = new Color(r, g, b, a);
    }


    public void SetPosition( float x, float y )
    {

        Vector3 temp = this.transform.position;
        temp.x = x;
        temp.y = y;
        temp.z = 0f;
        this.transform.position = temp;
    }
    public void SetPosition(Vector3 position )
    {
        this.SetPosition(position.x, position.y);
    }


    public void SetRotate( float z)
    {
        Vector3 worldAngle = transform.localEulerAngles;
        worldAngle.z = z * Mathf.Rad2Deg; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
        transform.localEulerAngles = worldAngle; // 回転角度を設定    
    }

    public void SetRotate( float x, float y ,  float z )
    {
        Vector3 worldAngle = transform.localEulerAngles;
        worldAngle.x = x*Mathf.Rad2Deg; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
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

    public void SetScale( float value )
    {
        this.SetScale(value, value);
    }

    public void SetScale( float x , float y )
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

    public int SortingOrder
    {
        get
        {
            return this.render.sortingOrder;
        }
        set
        {
            this.render.sortingOrder = value;
        }
    }


}
