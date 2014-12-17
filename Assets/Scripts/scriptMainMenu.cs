using UnityEngine;
using System.Collections;

public class scriptMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	void OnNewGameClicked(){
		Application.LoadLevel("mainScene");
	}

	void OnLoadGameClicked(){

	}

	void OnOptionsClicked(){

	}

	void OnExitClicked(){
		Application.Quit();
	}
}
