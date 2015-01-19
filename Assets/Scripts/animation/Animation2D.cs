using UnityEngine;
using System.Collections;

/**
Detecting collision in 2D environment
 **/

public class Animation2D : MonoBehaviour {

	public Vector2 newPosition;

	void Awake(){
		newPosition = transform.position;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		PositionChanging();
	}

	void PositionChanging(){
		Vector2 positionA = new Vector2(0,0);
		
		if(Input.GetKey(KeyCode.Q)){
			newPosition = positionA;
		}
		//transform.position = newPosition;
		// Lerp = Interpolaçao linear
		transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime);
		Debug.Log ("Position changed");
	}

	// Primeiro contato na colisao
	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("Collision: " + col.gameObject.name);
	}

	void OnCollisionStay2D(Collision2D col){
		Debug.Log ("Collision stay: " + col.gameObject.name);
	}

}
