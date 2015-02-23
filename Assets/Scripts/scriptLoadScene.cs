using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class scriptLoadScene : MonoBehaviour {
	private int numberGame = 0;
	private Player player = new Player(10,10, new Troop(new List<Unit>(), new List<Ship>()), 10, 10, new int[]{Player.ENABLED, Player.DISABLED, Player.DISABLED, Player.DISABLED});
	
	private GameObject dialogPanel;
	private Vector3 dialogPanelPosition;
	private GameObject dialogMessage;
	private Vector3 dialogMessagePosition;
	private GameObject dialogButton;
	private Vector3 dialogButtonPosition;
	private Vector3 invisible;
	
	
	void Start(){
		
		dialogPanel = GameObject.Find ("DialogPanel");
		dialogMessage = GameObject.Find ("DialogMessage");
		dialogButton = GameObject.Find ("DialogButton");
		
		dialogPanelPosition = dialogPanel.transform.position;
		dialogMessagePosition = dialogMessage.transform.position;
		dialogButtonPosition = dialogButton.transform.position;
		invisible = new Vector3(300f, 300f, 300f);
		
		hideDialog();
		
		for (int i = 1; i < 5; i++) {
			try{
				Player p = (new GameController ()).LoadGame (i);
				GameObject log = GameObject.Find ("Label"+i);
				UILabel content = log.GetComponent<UILabel> ();
				content.text = "Mission 1; Gold: " + p.Gold + "; Units: " + p.Units.Units.Count+";";
				Debug.Log ("Test " + i);
			}catch(FileNotFoundException e){
				GameObject log = GameObject.Find ("Label"+i);
				UILabel content = log.GetComponent<UILabel> ();
				content.text = "Empty";
				Debug.Log ("Test " + i + " empty.");
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
	
	void OnButtonStartClicked(){
		if(GameObject.Find ("Label"+numberGame).GetComponent<UILabel> ().text.Equals("Empty")){
			Debug.Log ("Test");
			showDialog();
		}else{
			Debug.Log (numberGame.ToString());
			GameManager.currentNumberGame = numberGame;
			Player p = (new GameController ()).LoadGame (numberGame);
			GameManager.player = p;
			
			Debug.Log ("mainScene");
			Application.LoadLevel("mainScene");
		}
	}
	
	void OnDialogButtonClicked(){
		hideDialog();
	}
	
	void showDialog(){
		dialogPanel.transform.position = dialogPanelPosition;
		dialogMessage.transform.position = dialogMessagePosition;
		dialogButton.transform.position = dialogButtonPosition;
		dialogButton.SetActive(true);
	}
	
	void hideDialog(){
		dialogPanel.transform.position = invisible;
		dialogMessage.transform.position = invisible;
		dialogButton.transform.position = invisible;
		dialogButton.SetActive(false);
	}
}
