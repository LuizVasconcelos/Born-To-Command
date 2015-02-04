using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class Mission1Script : MonoBehaviour {

	private int index;
	private bool village3Clicked;
	private bool castleClicked;
	private float startTime;

	// Action buttons
	private bool isGoClicked;

	// Use this for initialization
	void Start () {
		village3Clicked = false;
		castleClicked = false;
		startTime = Time.time;

		isGoClicked = false;
	}
	
	// Update is called once per frame
	void Update () {

		/**************************** PHASE 1 ********************************/

		// Lines exposition
		if (village3Clicked && index >= 0) {
			if(castleClicked){
				show (index,2);
				index--;
			}else{
				show (index,1);
				index--;
			}
		}else if(castleClicked && index >= 0){
			show (index,3);
			index--;
		}

		// Click check
		if(Input.GetMouseButtonDown(0)){
			
			//Debug.Log("Mouse is down");
			
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit){
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				// Clicked nearest village
				if (hitInfo.transform.gameObject.name == "Village3-obj" && !castleClicked){
					village3Clicked = true;
					index = GameObject.FindGameObjectsWithTag("Route-1").Length-1;
				// Clicked the castle
				} else if(hitInfo.transform.gameObject.name == "Castle-obj"){
					castleClicked = true;

					// Indirect route
					if(village3Clicked){
						index = GameObject.FindGameObjectsWithTag("Route-2").Length-1;
					// Direct route
					}else{
						index = GameObject.FindGameObjectsWithTag("Route-3").Length-1;
					}
				} else {
					Debug.Log ("TAG: " + hitInfo.transform.gameObject.tag);
				}
			} else {
				Debug.Log("No hit");
			}
		}
		/*********************************************************************/

		/**************************** PHASE 2 ********************************/
		if(isGoClicked){
			
			RaycastHit hitInfo = new RaycastHit();
			GameObject personagem = GameObject.Find ("Personagem");
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit){
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.name == "Castle-obj" || 
				    hitInfo.transform.gameObject.name == "Village1-obj" || 
				    hitInfo.transform.gameObject.name == "Village2-obj" ||
				    hitInfo.transform.gameObject.name == "Village3-obj" ||
				    hitInfo.transform.gameObject.name == "Campfire-obj"){
					Debug.Log ("It's working!");
					Vector3 clickedPosition = hitInfo.point;
					personagem.transform.position = Vector3.Lerp(personagem.transform.position, clickedPosition, (Time.time - startTime) / 1.0f);
					
				} else {
					Debug.Log ("nopz");
					Debug.Log ("TAG: " + hitInfo.transform.gameObject.tag);
				}
			} else {
				Debug.Log("No hit");
			}
		}
		/*********************************************************************/
	}

	void show(int idx, int route){
		GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route-"+route);

		Vector3 pos = labels[idx].transform.position;
		pos.z = 0.0f;
		labels[idx].transform.position = pos;
	}

	void OnGoClicked(){
		if (EditorUtility.DisplayDialog ("March to Battle!", //title
		                                 "Are you sure you want to follow this route to battle?", // text
		                                 "Yes", "No")) { // yes, no
			isGoClicked = true;
		}
	}

	void onCancelClicked(){
		// Clear lines
		for (int i = 1; i<=3; i++) {
			GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route-"+i);

			foreach (GameObject label in labels){
				Vector3 pos = label.transform.position;
				pos.z = 0.05f;
				label.transform.position = pos;
			}
		}

		// Clear variables
		village3Clicked = false;
		castleClicked = false;
	}

	GameObject FindClosestLabel(int route) {
		GameObject personagem = GameObject.Find ("Personagem");

		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Route-"+route);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = personagem.transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
