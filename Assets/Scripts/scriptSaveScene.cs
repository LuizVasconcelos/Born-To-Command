using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class scriptSaveScene : MonoBehaviour {

	// Game id default
	private int numberGame = 1; 
	// Current Data player
	private Player player = new Player();

	void OnButtonClicked1(){
		numberGame = 1;
		// TODO Update Player data information for this slot
		Debug.Log ("Button 1 clicked.");
	}

	void OnButtonClicked2(){
		numberGame = 2;
		// TODO Update Player data information for this slot
		Debug.Log ("Button 2 clicked.");
	}

	void OnButtonClicked3(){
		numberGame = 3;
		// TODO Update Player data information for this slot
		Debug.Log ("Button 3 clicked.");
	}

	void OnButtonClicked4(){
		numberGame = 4;
		// TODO Update Player data information for this slot
		Debug.Log ("Button 4 clicked.");
	}

	void OnButtonSaveClicked(){
		GameManager.currentNumberGame = numberGame;
		player = GameManager.player;
		Debug.Log ("Saving game number " + numberGame);
		(new GameController()).SaveGame(numberGame, player);
		Debug.Log ("Game " + numberGame + " saved.");
		Application.LoadLevel("mainScene");
	}
}
