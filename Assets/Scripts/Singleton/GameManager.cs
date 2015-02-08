using UnityEngine;
using System.Collections;

/**
Links:
http://wiki.unity3d.com/index.php/Singleton
http://rusticode.com/2014/01/21/creating-game-manager-using-singleton-pattern-and-monobehaviour-in-unity3d/
http://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/persistence-data-saving-loading
http://blog.christianhenschel.com/2013/05/16/how-to-pass-data-between-scenes-static-variables/
 **/

public class GameManager : MonoBehaviour {
	static public int currentNumberGame = 1;
	static public Player player = new Player();
}

