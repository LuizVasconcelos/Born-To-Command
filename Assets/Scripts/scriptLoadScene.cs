using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class scriptLoadScene : MonoBehaviour {
	private int numberGame = 0;
	private Player player = new Player(10,10, new Troop(new List<Unit>()), 10, 10);
	
	void OnButtonClicked1(){
		Debug.Log ("Load Game 1");
		// TODO
	}
	
	void OnButtonClicked2(){
		Debug.Log ("Load Game 1");
		// TODO
	}
	
	void OnButtonClicked3(){
		Debug.Log ("Load Game 1");
		// TODO
	}
	
	void OnButtonClicked4(){
		Debug.Log ("Load Game 1");
		// TODO
	}
	
	void OnButtonStartClicked(){
		// TODO
		Debug.Log ("mainScene");
		Application.LoadLevel("mainScene");
	}
}
