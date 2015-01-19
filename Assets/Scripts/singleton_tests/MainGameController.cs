using UnityEngine;
using System.Collections;

/**
	Only maintain just ONE instance of this object
 */

public class MainGameController : MonoBehaviour {

	private int _currentScore;
	public static MainGameController instance;

	void Awake(){
		instance = this;
	}

	public void AdjustScore(int num){
		_currentScore += num;
	}

	void OnGUI(){
		GUI.Label(new Rect(10,10,100,100), "Score: " + _currentScore);

	}
}
