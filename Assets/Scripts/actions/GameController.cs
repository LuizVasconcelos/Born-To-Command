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

	/*************************** Combat Script ******************************/
	public static Tuple<Troop, Troop> combatTurn(Troop alliedTroops, Troop enemyTroops, Tuple<int,int> odds){
		int alliedIDX = 0;
		int enemyIDX = 0;
		bool alliedTurn = true;

		for (int i = 0; i<(alliedTroops.Units.Count+enemyTroops.Units.Count); ) {
			// Switch between turns
			if(alliedTurn){
				alliedTurn = false;

				if(alliedIDX<alliedTroops.Units.Count){
					Unit u = alliedTroops.Units[alliedIDX];
					enemyTroops = damage(enemyTroops, u, odds.First);
					alliedIDX++;

					i++;
				}
			}else{
				alliedTurn = true;

				if(enemyIDX<enemyTroops.Units.Count){
					Unit u = enemyTroops.Units[enemyIDX];
					alliedTroops = damage(alliedTroops, u, odds.Second);
					enemyIDX++;

					i++;
				}
			}
		}
		
		return new Tuple<Troop, Troop>(alliedTroops, enemyTroops);
	}
	
	public static Troop damage(Troop defenders, Unit attacker, int ammount){
		int damage = attacker.Damage ();
		
		for (int i = 0; i<ammount && defenders.Units.Count > 0; i++) {
			
			int idx = Random.Range(0,(defenders.Units.Count-1));
			
			Unit defender = defenders.Units[idx];
			float rawDamage = damage*defender.Armor;
			
			// Rock-paper-scisors bonuses
			if(attacker.Type.Equals(Unit.KNIGHT) && defender.Type.Equals(Unit.SWORDMAN)){
				rawDamage *= 1.3f;
			}else if(attacker.Type.Equals(Unit.SWORDMAN) && defender.Type.Equals(Unit.ARCHER)){
				rawDamage *= 1.25f;
			}else if(attacker.Type.Equals(Unit.ARCHER) && defender.Type.Equals(Unit.KNIGHT)){
				rawDamage *= 1.75f;
			}
			
			int damageDealt = Mathf.CeilToInt(rawDamage);
			
			Debug.Log("A unit dealt "+damageDealt+"("+damage+"*"+defender.Armor+") damage!\n" +
			          "Health: "+defender.Health+"->"+(defender.Health - damageDealt));
			
			defender.Health -=  damageDealt;
			if(defender.Health <= 0){
				defenders.Units.RemoveAt(idx);
				defenders.Deaths += 1;
			}
		}
		return defenders;
	}
	/************************************************************************/

}

public class Unit
{

	public Unit(string Type, int Health, Tuple<int, int> Attack, float Armor){
		this.Type = Type;
		this.Health = Health;
		this.Attack = Attack;
		this.Armor = Armor;
	}

	public Unit(){}

	public int Damage(){
		return Random.Range (this.Attack.First, this.Attack.Second);
	}

	public static Unit newSwordman(){
		return new Unit(Unit.SWORDMAN,100,new Tuple<int, int>(20,35),0.2f);
	}

	public static Unit newKnight(){
		return new Unit(Unit.KNIGHT,100,new Tuple<int, int>(25,30),0.35f);
	}

	public static Unit newArcher(){
		return new Unit(Unit.ARCHER,100,new Tuple<int, int>(10,100),0.05f);
	}

	public const string SWORDMAN = "Swordman";
	public const string KNIGHT = "Knight";
	public const string ARCHER = "Archer";

	public string Type { get; set; }
	public int Health { get; set; }
	public Tuple<int, int> Attack { get; set; }
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
			int type = Random.Range(1,3);
			Unit u = null;
			switch(type){
			case 1:
				u = Unit.newSwordman();
				break;
			case 2:
				u = Unit.newKnight();
				break;
			case 3:
				u = Unit.newArcher();
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

	public static Troop generateTroop(int swordmans, int knights, int archers){
		Troop units = new Troop (new List<Unit> (swordmans + knights + archers));
		for (int i = 0; i<swordmans; i++) {
				Unit u = Unit.newSwordman ();
				/*Debug.Log("Unit:: \n" +
		"Health: "+u.Health+"\n" +
	    "Attack: "+u.Attack+"\n" +
	    "Armor: "+u.Armor);*/
				units.Units.Add (u);
		}

		for (int i = 0; i<knights; i++) {
				Unit u = Unit.newKnight ();
				/*Debug.Log("Unit:: \n" +
		"Health: "+u.Health+"\n" +
	    "Attack: "+u.Attack+"\n" +
	    "Armor: "+u.Armor);*/
				units.Units.Add (u);
		}

		for (int i = 0; i<archers; i++) {
				Unit u = Unit.newArcher ();
				/*Debug.Log("Unit:: \n" +
		"Health: "+u.Health+"\n" +
	    "Attack: "+u.Attack+"\n" +
	    "Armor: "+u.Armor);*/
				units.Units.Add (u);
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