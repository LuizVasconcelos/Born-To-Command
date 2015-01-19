using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MainGameController.instance.AdjustScore(2300);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
