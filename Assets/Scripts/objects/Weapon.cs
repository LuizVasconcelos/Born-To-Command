using UnityEngine;
using System.Collections;

public class Weapon
{
	private string Type; // Ex.: Sword, Bow...
	private int Payload; // Represents the value of the weapon
	// private float resellingPrice; FIXME Check this feature out

	Weapon(string Type, int Payload){
		this.Type = Type;
		this.Payload = Payload;
	}

	public bool makeDamage(int Value){
		if(this.Payload > Value){
			this.Payload = this.Payload - Value;
			return false;
		}else{
			this.Payload = 0;
			return true;
		}
	}

}

