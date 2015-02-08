using UnityEngine;
using System.Collections;

public class scriptMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}

	void OnNewGameClicked(){
		Debug.Log("On New Game");
		Application.LoadLevel("prologueScene");
	}

	void OnLoadGameClicked(){
		Debug.Log("On Load Game");
		Application.LoadLevel ("LoadGameMenuScene");
	}

	void OnOptionsClicked(){
		Debug.Log("On Options");
	}

	void OnExitClicked(){
		Debug.Log("On Exit");
		Application.Quit();
	}
}
