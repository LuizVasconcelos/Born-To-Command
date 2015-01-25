using UnityEngine;
using System.Collections;

public class ScriptSaveGame : MonoBehaviour {

	private static int gameSavedNumber;
	private GameController gc;
	private Player player;

	void Start(){
		Debug.Log("Starting");
		gc = new GameController();
		Debug.Log("Started");
	}

	void OnButtonClicked1(){
		Debug.Log("Game loaded " + "1");
		player = gc.LoadGame(1);
		Debug.Log ("Test 1 " + player.Health);
		Application.LoadLevel("mainScene");
	}

	void OnButtonClicked2(){
		Debug.Log("Game loaded " + "2");
		player = gc.LoadGame(2);
		Debug.Log ("Test 2 " + player.Health);
		Application.LoadLevel("mainScene");
	}

	void OnButtonClicked3(){
		Debug.Log("Game loaded " +  "3");
		player = gc.LoadGame(3);
		Debug.Log ("Test 3 " + player.Health);
		Application.LoadLevel("mainScene");
	}

	void OnButtonClicked4(){
		Debug.Log("Game loaded " +  "4");
		player = gc.LoadGame(4);
		Debug.Log ("Test 4 " + player.Health);
		Application.LoadLevel("mainScene");
	}
}

