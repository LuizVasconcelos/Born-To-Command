using UnityEngine;
using System.Collections;

public class Soldier
{
	private string TypeSoldier; // Tipo de soldado
	private int HealthPoints; // Pontos de Saude
	private int ExperiencePoints; // Pontos de experiencia (Usado para moral do General)
	private Weapon WeaponSoldier; // Arma usada pelo soldado

	// Construtor para Soldados que nao possuem armas
	Soldier(string TypeSoldier){
		this.TypeSoldier = TypeSoldier;
		this.HealthPoints = 0;
		this.ExperiencePoints = 0;
		this.WeaponSoldier = null;
	}
		
	// Construtor para Soldados armados
	Soldier(string Type, Weapon WeaponSoldier){
		this.TypeSoldier = Type;
		this.HealthPoints = 0;
		this.ExperiencePoints = 0;
		this.WeaponSoldier = WeaponSoldier;
	}

	public string GetTypeSoldier(){
		return this.TypeSoldier;
	}

	public void IncreaseHealth(int Value){
		this.HealthPoints = this.HealthPoints + Value;
	}

	public void DecreaseHealth(int Value){
		if(this.HealthPoints > Value){
			this.HealthPoints = this.HealthPoints - Value;
		}else{
			this.HealthPoints = 0;
		}
	}

	public bool DecreaseExperience(int Value){
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
	public bool DecreaseWeaponPower(int Value){
		return WeaponSoldier.MakeDamage(Value);
	}

}

