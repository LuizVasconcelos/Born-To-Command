using UnityEngine;
using System.Collections;

/**
 * 
 * Esse exemplo foi feito para 3D environment
 * 
Based on this tutorial: http://unity3d.com/learn/tutorials/modules/beginner/scripting/translate-and-rotate

Another links:
http://www.raywenderlich.com/61532/unity-2d-tutorial-getting-started
https://unity3d.com/pt/learn/tutorials/modules/beginner/physics/colliders
http://msdn.microsoft.com/en-us/magazine/dn802605.aspx
http://www.raywenderlich.com/70344/unity-2d-tutorial-physics-and-screen-sizes

Scripting:
http://unity3d.com/learn/tutorials/modules/beginner/scripting

Particle system:
https://unity3d.com/pt/learn/tutorials/modules/beginner/live-training-archive/particle-systems
http://pixelnest.io/tutorials/2d-game-unity/particles/
*/

public class Animation3D : MonoBehaviour {

	public float moveSpeed = 1f;
	public Transform target;

	public GameObject cube;
	public GameObject sphere;
	public GameObject cylinder;

	public Vector2 newPosition;

	// Use this for initialization
	void Start () {}

	void Awake(){
		newPosition = transform.position;
		cube = GameObject.Find("Cube");
		sphere = GameObject.Find("Sphere");
		cylinder = GameObject.Find ("Cylinder");
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

		/*if(Input.GetKey(KeyCode.UpArrow)){
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
		*/
		// Look at another objects
		//transform.LookAt(target);


		PositionChanging();
		ColorChanging(cube, Color.green);
		ColorChanging (sphere, Color.blue);
		ColorChanging (cylinder, Color.red);
	}

	void ColorChanging(GameObject gameObject, Color color){
		gameObject.renderer.material.color = color;
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
	
	void OnCollisionEnter(Collision collision){
		/*if(collision.gameObject.name == "Cylinder"){
			Destroy(collision.gameObject);
		}*/
		Debug.Log ("Collision: ");
	}

	void OnCollisionStay(Collision collision){
		Debug.Log ("Collision stay: ");
	}

	void OnTriggerEnter(Collider col){
		Debug.Log ("Trigger enter");
	}

}
