using UnityEngine;
using System.Collections;

public class Soldier
{
	private string Type;
	private int HealthPoints;
	private int ExperiencePoints;
	private Weapon WeaponSoldier;

	Soldier(string Type, Weapon WeaponSoldier){
		this.Type = Type;
		this.HealthPoints = 0;
		this.ExperiencePoints = 0;
		this.WeaponSoldier = WeaponSoldier;
	}

	public string GetType(){
		return this.Type;
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

	public void decreaseWeaponPower(int Value){
		bool Died = false;
		if(this.WeaponSoldier > Value){
			this.WeaponSoldier = this.WeaponSoldier - Value;
		}else{
			this.WeaponSoldier = 0;
			Died = true;
		}
		return Died;
	}

}

