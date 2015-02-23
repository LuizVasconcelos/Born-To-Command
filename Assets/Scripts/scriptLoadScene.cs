using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class scriptLoadScene : MonoBehaviour {
	private int numberGame = 0;
	private Player player = new Player(10,10, new Troop(new List<Unit>(), new List<Ship>()), 10, 10, new int[]{Player.ENABLED, Player.DISABLED, Player.DISABLED, Player.DISABLED});

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
		Debug.Log ("Load Game 1");
		// TODO
	}
	
	void Onckb2Activate(){
		numberGame = 2;
		Debug.Log ("Load Game 2");
		// TODO
	}
	
	void Onckb3Activate(){
		numberGame = 3;
		Debug.Log ("Load Game 3");
		// TODO
	}
	
	void Onckb4Activate(){
		numberGame = 4;
		Debug.Log ("Load Game 4");
		// TODO
	}
	
	void OnButtonStartClicked(){
		// TODO
		GameManager.currentNumberGame = numberGame;
		Player p = (new GameController ()).LoadGame (numberGame);
		GameManager.player = p;


		Debug.Log ("mainScene");
		Application.LoadLevel("mainScene");
	}
}
