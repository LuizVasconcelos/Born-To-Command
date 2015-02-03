using UnityEngine;
using System.Collections;

public class scriptSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P) && gameObject.audio.isPlaying) {
			gameObject.audio.Pause ();
		} else if(Input.GetKeyDown (KeyCode.P) && !gameObject.audio.isPlaying){
			gameObject.audio.Play ();
		}
	}
}
