using UnityEngine;
using System.Collections;

/**
Links:
http://www.raywenderlich.com/66345/unity-2d-tutorial-animations
http://www.raywenderlich.com/61532/unity-2d-tutorial-getting-started
http://www.raywenderlich.com/70344/unity-2d-tutorial-physics-and-screen-sizes

Colliders:
https://unity3d.com/pt/learn/tutorials/modules/beginner/physics/colliders
http://msdn.microsoft.com/en-us/magazine/dn802605.aspx
http://unity3d.com/learn/tutorials/modules/beginner/scripting/instantiate

Scripting:
http://unity3d.com/learn/tutorials/modules/beginner/scripting

Sistema de Particulas
http://pixelnest.io/tutorials/2d-game-unity/particles/
ttp://unity3d.com/learn/tutorials/modules/beginner/scripting 
ttp://unity3d.com/learn/tutorials/modules/beginner/scripting 
 */

public class Animation2D : MonoBehaviour {

	public Vector2 position;

	void Awake(){
		position = transform.position;
	}

	void Start(){

	}

	void Update(){
		PositionChanging();
	}

	void PositionChanging(){
		//transform.Translate (Vector2.up * Time.deltaTime);
		//transform.position = new Vector2(0f,0f);
		Vector2 newPosition = new Vector2(0f,0f);
		transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime);
	}

	void OnCollisionStart2D(Collision2D col){
		// TODO Animacao 2D -> collision part
		Debug.Log ("Collision started " + col.gameObject.name);
	}

	void OnCollisionStay2D(Collision2D col){
		// TODO Animacao 2D -> collision part
	}
}

