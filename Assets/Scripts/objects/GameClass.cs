using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameClass
{
	// Add all the attributes to store a game configuration
	public List<Soldier> soldiers;
	public List<Level> levels;

	public GameClass(){
		soldiers = new List<Soldier>();
		levels = new List<Level>();
	}
}

