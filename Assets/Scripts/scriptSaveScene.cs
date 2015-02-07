using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class scriptSaveScene : MonoBehaviour {

	private int numberGame = 0;
	private Player player = new Player(10,10, new Troop(new List<Unit>()), 10, 10);

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
		Debug.Log ("Saving game number " + numberGame);
		(new GameController()).SaveGame(numberGame, player);
		Debug.Log ("Game " + numberGame + " saved.");
		Application.LoadLevel("mainMenu");
	}
}
