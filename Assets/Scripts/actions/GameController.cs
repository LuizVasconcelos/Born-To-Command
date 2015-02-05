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

	// Writing data to file PlayerData.xml
	public void SaveGame(int gameNumber, Player player){
		XmlSerializer xmls = new XmlSerializer(typeof(Player));
		using (var stream = File.OpenWrite(filename + gameNumber + ".xml")){
			xmls.Serialize(stream, player);
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

public class Unit
{
	public Unit(string Type, int Health, int Attack, float Armor){
		this.Type = Type;
		this.Health = Health;
		this.Attack = Attack;
		this.Armor = Armor;
	}

	public string Type { get; set; }
	public int Health { get; set; }
	public int Attack { get; set; }
	public float Armor { get; set; }
}

public class Troop
{
	public Troop (List<Unit> Units, int Deaths = 0){
		this.Units = Units;
		this.Deaths = Deaths;
	}

	public List<Unit> Units { get; set; }
	public int Deaths { get; set; }
}

[XmlRoot]
public class Player
{
	public Player (int Gold, int Food, Troop Units, int Moral, int Travelling){
		this.Gold = Gold;
		this.Food = Food;
		this.Units = Units;
		this.Moral = Moral;
		this.Travelling = Travelling;
	}

	[XmlElement]
	public int Gold { get; set; }
	
	[XmlElement]
	public int Food { get; set; }
	
	[XmlElement]
	public Troop Units { get; set; }
	
	[XmlElement]
	public int Moral { get; set; }
	
	[XmlElement]
	public int Travelling { get; set; }
}


public class Test{

	public int AtributoTeste { get; set; }	

	public Test(int Value){
		this.AtributoTeste = Value;
	}
	// This empty construtor is mandatory
	public Test(){}
}

public class Tuple<T1, T2>
{
	public T1 First { get; private set; }
	public T2 Second { get; private set; }
	internal Tuple(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}
}

