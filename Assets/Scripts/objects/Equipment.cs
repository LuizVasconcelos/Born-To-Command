using UnityEngine;
using System.Collections;

public class Equipment
{
	// Ex.: Sword, Bow, Armor...
	private string TypeEquipment;
	// atributo: valor da compra
	private int PriceOfPurchase;
	private float Attack;
	private float Defense;

	// For initiate the weapons in the inventary	
	public Equipment(string TypeEquipment, float Attack, float Defense, int PriceOfPurchase){
		this.TypeEquipment = TypeEquipment;
		this.Attack = Attack;
		this.Defense = Defense;
		this.PriceOfPurchase = PriceOfPurchase;
	}

	public string GetTypeEquipment(){
		return this.TypeEquipment;
	}

	public float GetAttack(){
		return this.Attack;
	}

	public float GetDefense(){
		return this.Defense;
	}

	public int GetPriceOfPurchase(){
		return this.PriceOfPurchase;
	}

}

