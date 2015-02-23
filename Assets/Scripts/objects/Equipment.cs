using UnityEngine;
using System.Collections;

public class Equipment
{
	// Ex.: Sword, Bow, Armor...
	private string TypeEquipment;
	//Ex.: Excalibur...
	private string NameEquipment;
	// atributo: disponivel para compra
	private bool AvailableToPurchase;
	// atributo: valor da compra
	private float PriceOfPurchase; 
	// atributo: valor de revenda
	private float PriceOfSelling;
	private float Attack;
	private float Defense;
	// Representa a carga da arma
	private int Payload; 
	//breve descriçao do equipamento0 em questao
	private string DescriptionEquipment;

	// For initiate the weapons in the inventary	
	public Equipment(string TypeEquipment, string NameEquipment, float Attack, float Defense, int Payload, float PriceOfPurchase, float PriceOfSelling, string DescriptionEquipment){
		this.TypeEquipment = TypeEquipment;
		this.NameEquipment = NameEquipment;
		this.Attack = Attack;
		this.Defense = Defense;
		this.Payload = Payload;
		this.PriceOfPurchase = PriceOfPurchase;
		this.PriceOfSelling = PriceOfSelling;
		this.AvailableToPurchase = true;
		this.DescriptionEquipment = DescriptionEquipment;
	}

	// Diminuir carga da Arma
	public bool MakeDamage(int Value){
		if(this.Payload > Value){
			this.Payload = this.Payload - Value;
			return false;
		}else{
			this.Payload = 0;
			return true;
		}
	}
	
	// Gets and Sets
	public void SetAvailableToPurchase(bool value){
		this.AvailableToPurchase = value;
	}


	public string GetTypeEquipment(){
		return this.TypeEquipment;
	}

	public string GetNameEquipment(){
		return this.NameEquipment;
	}

	public float GetAttack(){
		return this.Attack;
	}

	public float GetDefense(){
		return this.Defense;
	}

	public int GetPayload(){
		return this.Payload;
	}

	public bool GetAvailableToPurchase(){
		return this.AvailableToPurchase;
	}

	public float GetPriceOfPurchase(){
		return this.PriceOfPurchase;
	}

	public float GetPriceOfSelling(){
		return this.PriceOfSelling;
	}

	public string GetDescriptionEquipment(){
		return this.DescriptionEquipment;
	}

}

