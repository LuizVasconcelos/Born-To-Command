using UnityEngine;
using System.Collections;

public class AdjustScript : MonoBehaviour
{
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(50,160,100,90), "Loader Menu");

		GUI.Label(new Rect(10,10,100,30), "Health: " + GameControlTest.control.health);
		GUI.Label(new Rect(10,30,100,30), "Experience: " + GameControlTest.control.experience);
		GUI.Label (new Rect (10,50,100,30), "Weapon: " + "text");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(60,185,80,20), "Level 1")){
			int c = Application.loadedLevel;
			Debug.Log("Variable in the scene is " + c);
			Application.LoadLevel("Scene1");
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(60,210,80,20), "Level 2")) {
			int c = Application.loadedLevel;
			PlayerPrefs.SetString("weapon", "arrow");
			Debug.Log("Variable in the scene is " + c);
			Application.LoadLevel("Scene2");
			//player.GetSoldiers
		}

		GUI.Box(new Rect(50,290,130,170), "Changing values:");
		
		if(GUI.Button (new Rect(60,320,100,20), "Health up")){
			GameControlTest.control.health += 10;
		}
		if(GUI.Button (new Rect(60,345,100,20), "Health down")){
			GameControlTest.control.health -= 10;
		}
		if(GUI.Button (new Rect(60,370,100,20), "Experience up")){
			GameControlTest.control.experience += 10;
		}
		if(GUI.Button (new Rect(60,395,100,20), "Experience up")){
			GameControlTest.control.experience -= 10;
		}
		if(GUI.Button (new Rect(60,420,100,20), "Save")){
			Debug.Log ("Saving file");
			GameControlTest.control.Save();
			Debug.Log ("Saved");
		}
		if(GUI.Button (new Rect(60,445,100,20), "Load")){
			Debug.Log ("Loading from file");
			GameControlTest.control.Load ();

			Debug.Log ("Loaded");
		}

		if (Application.loadedLevel == 1) {
			Debug.Log ("Experience " + GameControlTest.control.experience.ToString());
		}
		/*if (GUI.Button (new Rect (60, 425, 100, 20), "Add soldier")) {
			string nome = GUI.TextArea(new Rect (60, 395, 100, 20), "");
			//PlayerFeatures.AddSoldier(nome, (int) GameControl.control.experience);		
		}*/
	}
}

