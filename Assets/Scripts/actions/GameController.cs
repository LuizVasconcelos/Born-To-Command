using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

public class GameController {
	
	/**
	 * Links:
	 *  http://stackoverflow.com/questions/17286308/how-to-write-an-xml-file-in-c-sharp-in-unity
	 */

	string path = Application.persistentDataPath + "/PlayerData.xml";
	string filename = Application.persistentDataPath + "/PlayerData";

	void Awake(){
		SaveGame(0);
		Player player = LoadGame(0);

		// Testing reading file
		Debug.Log ("Health: " + player.Health + " Level: " + player.Level);
		string aux = "";
		foreach (Test t in player.Lista){
			aux += t.AtributoTeste + " ";
		}
		Debug.Log ("Lista: " + aux);
	}

	// Writing data to file PlayerData.xml
	public void SaveGame(int gameNumber){
		XmlSerializer xmls = new XmlSerializer(typeof(Player));
		using (var stream = File.OpenWrite(filename + gameNumber + ".xml")){
			List<Test> list = new List<Test>();
			list.Add(new Test(2));
			list.Add (new Test(3));
			list.Add (new Test(4));
			list.Add (new Test(5));
			list.Add (new Test(6));
			xmls.Serialize(stream, new Player { Level = 5, Health = 500, Lista = list });
		}
		Debug.Log (path);
	}

	// Reading data from Player
	public Player LoadGame(int gameNumber){
		XmlSerializer xmls = new XmlSerializer(typeof(Player));
		Player player = null;
		using (var stream = File.OpenRead(filename + gameNumber + ".xml")){
			player = xmls.Deserialize(stream) as Player;
		}
		Debug.Log ("Loaded Game");
		return player;
	}

}

[XmlRoot]
public class Player
{
	[XmlElement]
	public int Level { get; set; }
	
	[XmlElement]
	public int Health { get; set; }
	
	[XmlArray("Lista")]
	[XmlArrayItem("test")]
	public List<Test> Lista;
}


public class Test{

	public int AtributoTeste { get; set; }	

	public Test(int Value){
		this.AtributoTeste = Value;
	}
	// This empty construtor is mandatory
	public Test(){}
}

