using UnityEngine;
using System.Collections;

public class Weapon
{
	// Ex.: Sword, Bow...
	private string TypeWeapon; 
	// atributo: disponivel para compra
	private bool AvailableToPurchase;
	// atributo: valor da compra
	private float PriceOfPurchase; 
	// atributo: valor de revenda
	private float PriceOfSelling; 
	// Representa a carga da arma
	private int Payload; 

	// For initiate the weapons in the inventary	
	public Weapon(string TypeWeapon, int Payload, float PriceOfPurchase, float PriceOfSelling){
		this.TypeWeapon = TypeWeapon;
		this.Payload = Payload;
		this.PriceOfPurchase = PriceOfPurchase;
		this.PriceOfSelling = PriceOfSelling;
		this.AvailableToPurchase = true;
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


	public string GetTypeWeapon(){
		return this.TypeWeapon;
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

}

