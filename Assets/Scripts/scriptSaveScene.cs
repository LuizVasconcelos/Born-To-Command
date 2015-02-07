using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class scriptSaveScene : MonoBehaviour {

	private int numberGame = 0;
	private Player player = new Player(10,10, new Troop(new List<Unit>()), 10, 10);

	void OnButtonClicked1(){
		(new GameController()).SaveGame(1, player);
		Debug.Log ("Button 1 clicked.");
	}

	void OnButtonClicked2(){
		(new GameController()).SaveGame(2, player);
		Debug.Log ("Button 2 clicked.");
	}

	void OnButtonClicked3(){
		(new GameController()).SaveGame(3, player);
		Debug.Log ("Button 3 clicked.");
	}

	void OnButtonClicked4(){
		(new GameController()).SaveGame(4, player);
		Debug.Log ("Button 4 clicked.");
	}

	void OnButtonSaveClicked(){
		Application.LoadLevel("mainMenu");
	}

}
