using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerObject {
	// Lista de reinos aliados
	private List<string> AlliedKingdoms;
	// Lista de armas do jogador
	private List<Weapon> PlayerWeaponList;
	// Lista de soldados do jogador
	private List<Soldier> PlayerSoldierList;
	// Quantidade de ouro
	private float Gold;
	// Cabeças de gado do jogador
	private int Herd;

	public PlayerObject(float Gold){
		this.AlliedKingdoms = new List<string>();
		this.PlayerWeaponList = new List<Weapon> ();
		this.PlayerSoldierList = new List<Soldier>();
		this.Gold = Gold;
		this.Herd = 0;
	}

	// Adiciona reino aliado
	public void AddAlliedKingdom(string Kingdom){
		AlliedKingdoms.Add(Kingdom);
	}

	// Adiciona a lista de armas quando uma nova arma e comprada
	public void AddWeapon(Weapon NewWeapon){
		this.PlayerWeaponList.Add(NewWeapon);
	}

	// Adiciona a lista de soldados quando um novo soldado eh adquirido
	public void AddSoldier(Soldier NewSoldier){
		this.PlayerSoldierList.Add(NewSoldier);
	}

	// Ganho de ouro
	public void AddAmountOfGold(float Gold){
		this.Gold += Gold;
	}

	// Perda de ouro ou compra de item
	public void RemoveAmountOfGold(float Gold){
		this.Gold -= Gold;
	}

	// Ganho de cabecas de gado -> Pergunta ao Victor Pimenta
	public void AddHerd(int Herd){
		this.Herd += Herd;
	}

	// Perda de cabecas de gado -> Pergunta ao Victor Pimenta
	public void RemoveHerd(int Herd){
		this.Herd -= Herd;
	}

}

