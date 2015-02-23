using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class scriptSaveScene : MonoBehaviour {

	// Game id default
	private int numberGame = 1; 

	void Start(){
		for (int i = 1; i < 5; i++) {
			try{
				Player p = (new GameController ()).LoadGame (i);
				GameObject log = GameObject.Find ("Label"+i);
				UILabel content = log.GetComponent<UILabel> ();
				content.text = "Mission 1; Gold: "+p.Gold+"; Units: "+p.Units.Units.Count+";";
			}catch(FileNotFoundException e){
				GameObject log = GameObject.Find ("Label"+i);
				UILabel content = log.GetComponent<UILabel> ();
				content.text = "Empty";
			}
		}
	}

	void Onckb1Activate(){
		numberGame = 1;
	}
	
	void Onckb2Activate(){
		numberGame = 2;
	}
	
	void Onckb3Activate(){
		numberGame = 3;
	}
	
	void Onckb4Activate(){
		numberGame = 4;
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
