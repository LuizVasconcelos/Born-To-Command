using UnityEngine;
using System.Collections;

/**
Based on this tutorial: http://unity3d.com/learn/tutorials/modules/beginner/scripting/translate-and-rotate
 */

public class animation1 : MonoBehaviour {

	public float moveSpeed = 10f;
	public Transform target;

	public Vector2 newPosition;

	// Use this for initialization
	void Start () {}

	void Awake(){
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// Examples of Translations 
		// Command 1
		//transform.Translate(new Vector2(0.4f,2.2f));
		// Command 2
		//transform.Translate(new Vector2(0.4f,2.2f) * Time.deltaTime);
		// Command 3
		// P.S.: Esse comando usando Vector3 esta indo pro eixo Y e fica um efeito diferente
		//transform.Translate(Vector3.forward * Time.deltaTime); 
		//Command 4
		//transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); 

		// Examples based on Key press
		// right eh o eixo X
		// up eh o eixo Y

		if(Input.GetKey(KeyCode.UpArrow)){
			transform.Translate (Vector2.up * moveSpeed * Time.deltaTime);
			Debug.Log ("Obj to up");
		}

		if(Input.GetKey(KeyCode.DownArrow)){
			transform.Translate (-Vector2.up * moveSpeed * Time.deltaTime);
			Debug.Log ("Obj to down");
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
			Debug.Log ("Obj to right");
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate (-Vector2.right * moveSpeed * Time.deltaTime);
			Debug.Log ("Obj to left");
		}

		// Look at another objects
		//transform.LookAt(target);


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

}
