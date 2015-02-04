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
		if(Input.GetMouseButtonDown(0)){

			//Debug.Log("Mouse is down");
			
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit){
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.name == "Castle-obj" || 
				    hitInfo.transform.gameObject.name == "Village1-obj" || 
				    hitInfo.transform.gameObject.name == "Village2-obj" ||
				    hitInfo.transform.gameObject.name == "Village3-obj" ||
				    hitInfo.transform.gameObject.name == "Campfire-obj"){
					Debug.Log ("It's working!");
				} else {
					Debug.Log ("nopz");
					Debug.Log ("TAG: " + hitInfo.transform.gameObject.tag);
				}
			} else {
				Debug.Log("No hit");
			}
		}
	}

	void show(int idx){
		GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route");

		Vector3 pos = labels[idx].transform.position;
		pos.z = 0.0f;
		labels[idx].transform.position = pos;
	}
}
