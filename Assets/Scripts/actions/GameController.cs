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
			float rawDamage = defender.Defend (damage);
			
			// Rock-paper-scisors bonuses
			if(attacker.Type.Equals(Unit.KNIGHT) && defender.Type.Equals(Unit.SWORDMAN)){
				rawDamage *= 1.3f;
			}else if(attacker.Type.Equals(Unit.SWORDMAN) && defender.Type.Equals(Unit.ARCHER)){
				rawDamage *= 2.1f;
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

public class Item
{
	public Item(float Val){
		this.Val = Val;
	}
	
	public Item(){
		// Neutral value
		this.Val = 1.0f;
	}
	
	public float Val { get; set; }
}

public class Unit
{
	
	public Unit(string Type, int Health, Tuple<int, int> Attack, float Defense, Item Weapon, Item Armor, Item Shield){
		this.Type = Type;
		this.Health = Health;
		this.Attack = Attack;
		this.Defense = Defense;
		this.Weapon = Weapon;
		this.Armor = Armor;
		this.Shield = Shield;
	}
	
	public Unit(){}
	
	public int Damage(){
		return (int) (this.Weapon.Val * Random.Range (this.Attack.First, this.Attack.Second));
	}
	
	public float Defend(int damage){
		return (damage * (this.Defense * this.Armor.Val)) - this.Shield.Val;
	}
	
	public static Unit newSwordman(){
		return new Unit(Unit.SWORDMAN,100,new Tuple<int, int>(20,35),0.2f, new Item(), new Item(), new Item());
	}
	
	public static Unit newKnight(){
		return new Unit(Unit.KNIGHT,100,new Tuple<int, int>(25,30),0.35f, new Item(), new Item(), new Item());
	}
	
	public static Unit newArcher(){
		return new Unit(Unit.ARCHER,100,new Tuple<int, int>(10,100),0.05f, new Item(), new Item(), new Item());
	}
	
	public const string SWORDMAN = "Swordman";
	public const string KNIGHT = "Knight";
	public const string ARCHER = "Archer";
	
	public string Type { get; set; }
	public int Health { get; set; }
	public Tuple<int, int> Attack { get; set; }
	public float Defense { get; set; }
	
	// equipament
	public Item Weapon { get; set; }
	public Item Armor { get; set; }
	public Item Shield { get; set; }
}

public class Ship
{
	public Ship(int Capacity){
		this.Capacity = Capacity;
	}
	
	public Ship(){}
	
	public int Capacity { get; set; }
}

public class Troop
{
	public Troop (List<Unit> Units, List<Ship> Ships, int Deaths = 0){
		this.Units = Units;
		this.Ships = Ships;
		this.Deaths = Deaths;
	}
	
	public Troop(){}
	
	public List<Unit> Units { get; set; }
	public List<Ship> Ships { get; set; }
	public int Deaths { get; set; }
}

[XmlRoot]
public class Player
{
	public Player (int Gold, int Food, Troop Units, int Moral, int Travelling, int[] Game){
		this.Gold = Gold;
		this.Food = Food;
		this.Units = Units;
		this.Moral = Moral;
		this.Travelling = Travelling;
		this.Game = Game;
		this.Items = new Item[]{new Item(), new Item(), new Item()};
		
	}
	
	public Player(){}
	
	public const int DISABLED = 0;
	public const int ENABLED = 1;
	public const int WON = 2;
	public const int LOST = 3;
	
	public static Troop generateTroop(int size){
		Troop units = new Troop (new List<Unit>(size), new List<Ship>());
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
		Troop units = new Troop (new List<Unit> (swordmans + knights + archers), new List<Ship>());
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
	
	public void UpgradeWeapon(Item item){
		for (int i = 0; i<this.Units.Units.Count; i++) {
			this.Units.Units[i].Weapon = item;
		}
		
		this.Items [0] = item;
	}
	
	public void UpgradeArmor(Item item){
		for (int i = 0; i<this.Units.Units.Count; i++) {
			this.Units.Units[i].Armor = item;
		}
		
		this.Items [1] = item;
	}
	
	public void UpgradeShield(Item item){
		for (int i = 0; i<this.Units.Units.Count; i++) {
			this.Units.Units[i].Shield = item;
		}
		
		this.Items [2] = item;
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
	public int[] Game { get; set; }
	
	[XmlArray]
	public Item[] Items { get; set; }
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
	
	public Tuple(){}
	
	internal Tuple(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}





}