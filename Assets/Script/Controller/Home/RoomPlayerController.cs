using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerController : MonoBehaviour
{
    static float min_x = -2.5f;
    static float max_x =    2f;
    static float min_y =   -1f;
    static float max_y =    1f;

    float mTime;

    Const.Animation animationNow;

    // Use this for initialization
    void Start()
    {
        mTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mTime += Time.deltaTime;

        if (mTime > 10)
        {
            float pos_x = Random.Range(min_x, max_x);
            float pos_y = Random.Range(min_y, max_y);

            Vector2 p1 = transform.position;
            Vector2 p2 = new Vector2(pos_x, pos_y);

            Vector2 dir = p1 - p2;
            float d = dir.magnitude;
            //Debug.Log("d:" + d);

            this.ChangeLeftAndRightDirection(p1.x, p2.x);
            this.ChangeAnimation(Const.Animation.WalkTrigger);

            iTween.MoveTo(gameObject, iTween.Hash("x", p2.x, "y", p2.y, "delay", 0, "time", d * 2,
                "easetype", iTween.EaseType.linear, "oncomplete", "PlayerMoved", "oncompletetarget", gameObject));
            //iTween.MoveTo(gameObject, iTween.Hash("x", p2.x, "y", p2.y, "delay", 0, "time", d,
            //    "easetype", iTween.EaseType.linear, "oncomplete", "PlayerMoved", "oncompletetarget", gameObject));

            mTime = 0;
        }
    }

    void PlayerMoved()
    {
        this.ChangeAnimation(Const.Animation.StandTrigger);
    }

    private void ChangeAnimation(Const.Animation animation)
    {
        // 同一アニメーションの設定はしない
        if (this.animationNow != animation)
        {
            Animator animator = this.GetComponent<Animator>();
            animator.SetTrigger(animation.ToString());
            this.animationNow = animation;
        }
    }

    private void ChangeLeftAndRightDirection(float start_x, float end_x)
    {
        if (start_x > end_x)
        {
            // 左向き
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

            GameObject balloon = transform.Find("balloon").gameObject;
            balloon.transform.localScale = new Vector3(Mathf.Abs(balloon.transform.localScale.x), balloon.transform.localScale.y, balloon.transform.localScale.z);
            balloon.transform.localPosition = new Vector3(Mathf.Abs(balloon.transform.localPosition.x), balloon.transform.localPosition.y, balloon.transform.localPosition.z);
        }
        else if (start_x < end_x)
        {
            // 右向き
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, this.transform.localScale.y, this.transform.localScale.z);

            GameObject balloon = transform.Find("balloon").gameObject;
            balloon.transform.localScale = new Vector3(Mathf.Abs(balloon.transform.localScale.x) * -1, balloon.transform.localScale.y, balloon.transform.localScale.z);
            balloon.transform.localPosition = new Vector3(Mathf.Abs(balloon.transform.localPosition.x) * -1, balloon.transform.localPosition.y, balloon.transform.localPosition.z);
        }
    }
}
