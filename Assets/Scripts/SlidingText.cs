using UnityEngine;
using System.Collections;

public class SlidingText : MonoBehaviour {

	private GameObject param;

	// Use this for initialization
	void Start () {
		param = GameObject.Find ("textWall");
	}
	
	// Update is called once per frame
	void Update () {

		// current label y position
		float current = gameObject.transform.position.y;
		// hardcoded target depth
		float target = 380.0f;
		// 0.0 for default
		float currentVelocity = 0.0f;

		// fast mode vs. slow mode
		//float smoothTime = 8.0f;
		float smoothTime = 13.5f;

		// sliding phase phase
		Vector3 temp = gameObject.transform.position;
		temp.y = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);
		gameObject.transform.position = temp;

		// open phase

		//Debug.Log ("Label -> Y: " + gameObject.transform.position.y + "; Param -> Y: "+param.transform.position.y);
		if (gameObject.transform.position.y >= param.transform.position.y) 
		{
			Application.LoadLevel ("mainScene");
		}
	}
}
