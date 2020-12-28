using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			// 左クリックしたら、効果音を鳴らす
			if (Input.GetMouseButtonDown(0))
			{
				GetComponent<AudioSource>().Play();
				// 効果音を鳴らす
			}

			SceneManager.LoadScene ("HomeScene");
		}
	}
}
