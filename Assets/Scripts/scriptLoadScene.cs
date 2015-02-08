using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class scriptLoadScene : MonoBehaviour {
	private int numberGame = 0;
	private Player player = new Player(10,10, new Troop(new List<Unit>()), 10, 10);
	
	void OnButtonClicked1(){
		numberGame = 1;
		Debug.Log ("Load Game 1");
		// TODO
	}
	
	void OnButtonClicked2(){
		numberGame = 2;
		Debug.Log ("Load Game 2");
		// TODO
	}
	
	void OnButtonClicked3(){
		numberGame = 3;
		Debug.Log ("Load Game 3");
		// TODO
	}
	
	void OnButtonClicked4(){
		numberGame = 4;
		Debug.Log ("Load Game 4");
		// TODO
	}
	
	void OnButtonStartClicked(){
		// TODO
		GameManager.currentNumberGame = numberGame;
		Debug.Log ("mainScene");
		Application.LoadLevel("mainScene");
	}
}
