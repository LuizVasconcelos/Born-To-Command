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

	public Unit(string Type, int Health, int AttackDie, int NumDie, float Armor){
		this.Type = Type;
		this.Health = Health;
		this.AttackDie = AttackDie;
		this.NumDie = NumDie;
		this.Armor = Armor;
	}

	public Unit(){}

	public int RollAttackDie(){
		int result = 1;
		for (int i = 1; i<this.NumDie; i++) {
			result += Random.Range(1, this.AttackDie);
		}
		return result;
	}

	public const string SWORDMAN = "Swordman";
	public const string KNIGHT = "Knight";
	public const string ARCHER = "Archer";

	public string Type { get; set; }
	public int Health { get; set; }
	public int AttackDie { get; set; }
	public int NumDie { get; set; }
	public float Armor { get; set; }
}

public class Troop
{
	public Troop (List<Unit> Units, int Deaths = 0){
		this.Units = Units;
		this.Deaths = Deaths;
	}

	public Troop(){}

	public List<Unit> Units { get; set; }
	public int Deaths { get; set; }
}

[XmlRoot]
public class Player
{
	public Player (int Gold, int Food, Troop Units, int Moral, int Travelling, bool[] Game){
		this.Gold = Gold;
		this.Food = Food;
		this.Units = Units;
		this.Moral = Moral;
		this.Travelling = Travelling;
		this.Game = Game;
	}

	public Player(){}

	public static Troop generateTroop(int size){
		Troop units = new Troop (new List<Unit>(size));
		for (int i = 0; i<size; i++) {
			int type = Random.Range(1,4);
			Unit u = null;
			switch(type){
			case 1:
				u = new Unit(Unit.SWORDMAN,100,5,15,0.2f);
				break;
			case 2:
				u = new Unit(Unit.KNIGHT,100,2,35,0.35f);
				break;
			case 3:
				u = new Unit(Unit.ARCHER,100,100,1,0.05f);
				break;
			default:
				u = new Unit(Unit.SWORDMAN,100,5,15,0.2f);
				break;
			}
			/*Debug.Log("Unit:: \n" +
				"Health: "+u.Health+"\n" +
			    "Attack: "+u.Attack+"\n" +
			    "Armor: "+u.Armor);*/
			units.Units.Add(u);
		}
		
		return units;
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

	[XmlArray]
	public bool[] Game { get; set; }
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

