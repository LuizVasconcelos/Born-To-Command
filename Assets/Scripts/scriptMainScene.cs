using UnityEngine;
using System.Collections;

public class scriptMainScene : MonoBehaviour {

	private bool isBlacksmithClicked;

	// Use this for initialization
	void Start () {
		isBlacksmithClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isBlacksmithClicked){
			// current camera depth
			float current = Camera.main.camera.orthographicSize;
			// hardcoded target depth
			float target = 0.05f;
			// 0.0 for default
			float currentVelocity = 0.0f;
			// 0.15 for default
			float smoothTime = 0.15f;
			
			// zoom phase
			Camera.main.camera.orthographicSize = Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);

			// open phase
			if(Camera.main.camera.orthographicSize <= 0.06f){
				Application.LoadLevel("blacksmithScene");
			}
		}
	}

	void onBlacksmithClicked(){
		isBlacksmithClicked = true;
	}
}
