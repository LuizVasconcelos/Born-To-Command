using UnityEngine;
using System.Collections;

public class Level
{
	// Identificacao do Level 
	private int Id;
	// Definir status do Level: (Nao jogado, concluido, nao concluido)
	private int Status; 

	public Level(int Id, int Status){
		this.Id = Id;
		this.Status = Status;
	}

	public int GetId(){
		return this.Id;
	}

	public int GetStatus(){
		return this.Status;
	}

	public void SetStatus(int NewStatus){
		this.Status = NewStatus;
	}
}


