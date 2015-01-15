using UnityEngine;
using System.Collections;

public class Level
{
	private int Id;
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


