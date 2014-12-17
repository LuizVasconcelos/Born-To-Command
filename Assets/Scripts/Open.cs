using UnityEngine;
using System.Collections;

public class Open : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			// Open
			gameObject.transform.Translate(new Vector3(25,25,0));
		}
	}
}
