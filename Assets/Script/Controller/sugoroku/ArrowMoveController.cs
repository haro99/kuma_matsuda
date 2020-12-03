using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoveController : MonoBehaviour {

    float mTime;

    [SerializeField]
    Const.Direction direction = Const.Direction.none;

	// Use this for initialization
	void Start () {
        mTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        mTime += Time.deltaTime;
        if (mTime > 0.5f)
        {
            Vector3 pos = this.gameObject.transform.position;
            Vector3[] path = new Vector3[3];

            switch (direction)
            {
                case Const.Direction.up:
                    path[0] = new Vector3(pos.x - 5.0f, pos.y + 5.0f, 0);
                    path[1] = new Vector3(pos.x + 3.0f, pos.y - 3.0f, 0);
                    path[2] = new Vector3(pos.x, pos.y, 0);
                    break;
                case Const.Direction.right:
                    path[0] = new Vector3(pos.x + 5.0f, pos.y + 5.0f, 0);
                    path[1] = new Vector3(pos.x - 3.0f, pos.y - 3.0f, 0);
                    path[2] = new Vector3(pos.x, pos.y, 0);
                    break;
                case Const.Direction.down:
                    path[0] = new Vector3(pos.x + 5.0f, pos.y - 5.0f, 0);
                    path[1] = new Vector3(pos.x - 3.0f, pos.y + 3.0f, 0);
                    path[2] = new Vector3(pos.x, pos.y, 0);
                    break;
                case Const.Direction.left:
                    path[0] = new Vector3(pos.x - 5.0f, pos.y - 5.0f, 0);
                    path[1] = new Vector3(pos.x + 3.0f, pos.y + 3.0f, 0);
                    path[2] = new Vector3(pos.x, pos.y, 0);
                    break;
                default:
                    break;
            }

            iTween.MoveTo(this.gameObject, iTween.Hash("path", path, "delay", 0, "time", 0.5f, "easeType", iTween.EaseType.linear));
            mTime = 0;
        }

    }
}
