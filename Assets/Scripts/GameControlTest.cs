using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*
Official link to the save/load data: 
http://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/persistence-data-saving-loading
Debugging project
http://unity3d.com/learn/tutorials/modules/beginner/scripting/monodevelops-debugger
 */

[Serializable]
public class GameControlTest : MonoBehaviour {

	public static GameControl control;

	public float health;
	public float experience;

	// This happen before Start method
	void Awake(){
		Debug.Log ("Awaking process");
		if (control == null) {
			Debug.Log("control is not null");
			DontDestroyOnLoad (gameObject);
			control = this; // I truly believe that means POG
		} else if(control != this){
			Debug.Log ("control is not null");
			Destroy(gameObject);
		}
	}

	// Save player's data during a battle, call this in a changing of Scene
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); 
		PlayerData data = new PlayerData();
		data.health = health;
		data.experience = experience;

		bf.Serialize (file, data);
		file.Close();
	}

	// Load the player's data in somewhere else
	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			Debug.Log("File path = " + Application.persistentDataPath.ToString());
			PlayerData data = (PlayerData) bf.Deserialize(file);
			file.Close();
			health = data.health;
			experience = data.experience;
		}
	}

	// Serializable class that represents the Player and all the features needed.
	[Serializable]
	class PlayerData{
		public float health;
		public float experience;
	}
}
