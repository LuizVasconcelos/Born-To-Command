using UnityEngine;
using System.Collections;

public class Soldier
{
	private string TypeSoldier;
	private int HealthPoints;
	private int ExperiencePoints;
	private Weapon WeaponSoldier;

	Soldier(string Type, Weapon WeaponSoldier){
		this.TypeSoldier = Type;
		this.HealthPoints = 0;
		this.ExperiencePoints = 0;
		this.WeaponSoldier = WeaponSoldier;
	}

	public string GetTypeSoldier(){
		return this.TypeSoldier;
	}

	public void decreaseHealth(int Value){
		if(this.HealthPoints > Value){
			this.HealthPoints = this.HealthPoints - Value;
		}else{
			this.HealthPoints = 0;
		}
	}

	public bool decreaseExperience(int Value){
		bool Died = false;
		if(this.ExperiencePoints > Value){
			this.ExperiencePoints = this.ExperiencePoints - Value;
		}else{
			this.ExperiencePoints = 0;
			Died = true;
		}
		return Died;
	}
	// Diminish the weapon power and returns the liveness of player
	public bool decreaseWeaponPower(int Value){
		return WeaponSoldier.makeDamage(Value);
	}

}

