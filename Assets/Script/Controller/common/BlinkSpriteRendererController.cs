using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkSpriteRendererController : MonoBehaviour {

	SpriteRenderer text;
    [SerializeField]
    float m_AngularFrequency = 3.0f;
    [SerializeField]
	float m_DeltaTime = 0.0333f;
	Coroutine m_Coroutine;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<SpriteRenderer>();

		StartFlash();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Flash() {
		float m_Time = 0.0f;

		while (true)
		{
			m_Time += m_AngularFrequency * m_DeltaTime;
			var color = text.color;
			color.a = Mathf.Abs(Mathf.Sin (m_Time));
            //Debug.Log("a : " + color.a);
            text.color = color;
			yield return new WaitForSeconds(m_DeltaTime);
		}
	}

	public void StartFlash()
	{
		m_Coroutine = StartCoroutine(Flash());
	}

	public void StopFlash()
	{
		StopCoroutine(m_Coroutine);
	}
}
