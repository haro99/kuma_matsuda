using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
	public GameObject leftUpArrow;
	public GameObject rightUpArrow;
	public GameObject rightDownArrow;
	public GameObject leftDownArrow;

	public SugorokuDirector manager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDir(List<Const.Direction> dirList){
		leftUpArrow.SetActive(false);
		rightUpArrow.SetActive(false);
		rightDownArrow.SetActive(false);
		leftDownArrow.SetActive(false);

		for(int i = 0; i < dirList.Count; i++){
			var str = dirList[i];
			if(str == Const.Direction.up){
				leftUpArrow.SetActive(true);
			}else if(str == Const.Direction.right){
				rightUpArrow.SetActive(true);
			}else if(str == Const.Direction.down){
				rightDownArrow.SetActive(true);
			}else if(str == Const.Direction.left){
				leftDownArrow.SetActive (true);
			}
		}
	}

	public void LeftUpClick(){
		manager.UserSelectArrow(Const.Direction.up);
	}

	public void RightUpClick(){
		manager.UserSelectArrow(Const.Direction.right);
	}

	public void RightDownClick(){
		manager.UserSelectArrow(Const.Direction.down);
	}

	public void LeftDownClick(){
		manager.UserSelectArrow(Const.Direction.left);
	}

}
