using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class scriptSaveScene : MonoBehaviour {

	// Game id default
	private int numberGame = 1; 

	void OnButtonClicked1(){
		numberGame = 1;
		Debug.Log ("Button 1 clicked.");
	}

	void OnButtonClicked2(){
		numberGame = 2;
		Debug.Log ("Button 2 clicked.");
	}

	void OnButtonClicked3(){
		numberGame = 3;
		Debug.Log ("Button 3 clicked.");
	}

	void OnButtonClicked4(){
		numberGame = 4;
		Debug.Log ("Button 4 clicked.");
	}

	void OnButtonSaveClicked(){
		GameManager.currentNumberGame = numberGame;
		Debug.Log ("Saving game number " + numberGame);
		Debug.Log ("Gold = " + GameManager.player.Gold);
		(new GameController()).SaveGame(numberGame, GameManager.player);
		Debug.Log ("Game " + numberGame + " saved.");
		Application.LoadLevel("mainScene");
	}
}
