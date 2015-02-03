using UnityEngine;
using System.Collections;

public class Mission1Script : MonoBehaviour {

	private int index;
	private bool spacePressed;

	// Use this for initialization
	void Start () {
		index = GameObject.FindGameObjectsWithTag("Route").Length-1;
		spacePressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown (KeyCode.Space) || spacePressed)&&index>=0) {
			spacePressed = true;
			show (index);
			index--;
		}
	}

	void show(int idx){
		GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route");

		Vector3 pos = labels[idx].transform.position;
		pos.z = 0.0f;
		labels[idx].transform.position = pos;
	}
}
