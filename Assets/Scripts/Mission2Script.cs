using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif 

#if UNITY_EDITOR
public class Mission2Script : MonoBehaviour {
	
	// Players variables
	private Player localPlayer;
	private int playerTroopsMax;
	private Troop villageTroops;
	private int villageTroopsMax;
	private Troop castleTroops;
	private int castleTroopsMax;
	
	// define routes
	private readonly int ROUTE_HOME_TO_ICEGARD = 1;
	private readonly int ROUTE_HOME_TO_OUTPOST = 2;
	private readonly int ROUTE_HOME_TO_MAHAJA = 3;
	private readonly int ROUTE_OUTPOST_TO_MAHAJA = 4;
	private readonly int ROUTE_OUTPOST_TO_ICEGARD = 5;
	// define village choices
	//private readonly int ATTACK_VILLAGE = 4;
	//private readonly int PAY_VILLAGE = 5;
	// define castle choices
	//private readonly int ATTACK_CASTLE = 6;
	//private readonly int DUEL_CASTLE = 7;
	
	private int index;
	
	// Clicking options
	private bool outpostClicked;
	private bool icegardClicked;
	private bool mahajaClicked;
	
	private float startTime;
	private bool step2;
	private bool justarrived;
	private Vector3 target;
	private bool halt;
	private int labelsCrossed;
	
	// Action buttons
	private bool isGoClicked;
	
	// Actions
	private int villageChoice;
	private int castleChoice;
	
	// Use this for initialization
	void Start () {
		outpostClicked = false;
		icegardClicked = false;
		mahajaClicked = false;	
		
		startTime = Time.time;
		
		isGoClicked = false;
		step2 = false;
		villageChoice = 0;
		castleChoice = 0;
		
		localPlayer = GameManager.player;
		playerTroopsMax = localPlayer.Units.Units.Count;
		villageTroopsMax = 40;
		castleTroopsMax = 70;
		villageTroops = Player.generateTroop (villageTroopsMax);
		castleTroops = Player.generateTroop (castleTroopsMax);
		
		target = GameObject.Find ("Personagem").transform.position;
		halt = true;
		
		labelsCrossed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Update all
		updateStatus ();
		updateLog ();
		updateBars ();
		
		/**************************** PHASE 1 ********************************/
		
		// Lines exposition
		if (outpostClicked && index >= 0) {
			if(icegardClicked){
				show (index,ROUTE_OUTPOST_TO_ICEGARD);
				index--;
			}else if(mahajaClicked){
				show (index,ROUTE_OUTPOST_TO_MAHAJA);
				index--;
			}else{
				show (index,ROUTE_HOME_TO_OUTPOST);
				index--;
			}
		}else if(icegardClicked && index >= 0){
			show (index,ROUTE_HOME_TO_ICEGARD);
			index--;
		}else if(mahajaClicked && index >= 0){
			show (index,ROUTE_HOME_TO_MAHAJA);
			index--;
		}
	
	// Click check
	if(Input.GetMouseButtonDown(0)){
		
		//Debug.Log("Mouse is down");
		
		RaycastHit hitInfo = new RaycastHit();
		bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
		if (hit){
			Debug.Log("Hit " + hitInfo.transform.gameObject.name);
			// Clicked nearest village
			if (hitInfo.transform.gameObject.name == "Outpost-obj" && !icegardClicked && !mahajaClicked){
				outpostClicked = true;
				index = GameObject.FindGameObjectsWithTag("Route-2").Length-1;
				// Clicked the castle
			} else if(hitInfo.transform.gameObject.name == "Mahaja-Castle-obj" && !icegardClicked){
				mahajaClicked = true;
				
				// Indirect route
				if(outpostClicked){
					index = GameObject.FindGameObjectsWithTag("Route-4").Length-1;
					// Direct route
				}else{
					index = GameObject.FindGameObjectsWithTag("Route-3").Length-1;
				}
			}else if(hitInfo.transform.gameObject.name == "Icegard-Castle-obj" && !mahajaClicked){
				icegardClicked = true;
				
				// Indirect route
				if(outpostClicked){
					index = GameObject.FindGameObjectsWithTag("Route-5").Length-1;
					// Direct route
				}else{
					index = GameObject.FindGameObjectsWithTag("Route-1").Length-1;
				}
			} else {
			Debug.Log ("TAG: " + hitInfo.transform.gameObject.tag);
			}
		} else {
			Debug.Log("No hit");
		}
	}
	/*********************************************************************/
	
	/**************************** PHASE 2 ********************************/
	if(isGoClicked){
		// Check desired route
		GameObject personagem = GameObject.Find ("Personagem");
		
		// Indirect route
		if(outpostClicked){
			if(step2 && icegardClicked){
				bool stopped = moveTo (ROUTE_OUTPOST_TO_ICEGARD);
				
				if(stopped){
					// Here starts the castle script per se
					//castleEvent();
					
					// We halt the GO during the combat
					//isGoClicked = false;
				}
			}else if(step2 && mahajaClicked){
				bool stopped = moveTo (ROUTE_OUTPOST_TO_MAHAJA);
				
				// no more labels in this route
				if(stopped){
					// Here starts the village script per se
					//villageEvent();
					
					// We halt the GO during the combat
					//isGoClicked = false;
				}
			}else{
				bool stopped = moveTo (ROUTE_HOME_TO_OUTPOST);
				
				// no more labels in this route
				if(stopped){
					step2 = true;
					
					// Here starts the village script per se
					//villageEvent();
					
					// We halt the GO during the combat
					//isGoClicked = false;
				}
			}
			// Direct routes
		}else if(mahajaClicked){
			bool stopped = moveTo (ROUTE_HOME_TO_MAHAJA);
			
			if(stopped){
				// Here starts the castle script per se
				//castleEvent();
				
				// We halt the GO during the combat
				//isGoClicked = false;
			}
		}else{
			bool stopped = moveTo (ROUTE_HOME_TO_ICEGARD);
			
			if(stopped){
				// Here starts the castle script per se
				//castleEvent();
				
				// We halt the GO during the combat
				//isGoClicked = false;
			}
		}
	}
	/*********************************************************************/
	
	/**************************** PHASE 3 ********************************/
	/*if (villageChoice > 0) {
		if(villageChoice == ATTACK_VILLAGE){
			int total = localPlayer.Units.Units.Count+villageTroops.Units.Count;
			float odds = (float)localPlayer.Units.Units.Count/(float)total;
			//Debug.Log("total::odds = "+total+"::"+odds);
			Tuple<Troop, Troop> result = GameController.combatTurn(localPlayer.Units,villageTroops,odds);
			localPlayer.Units = result.First;
			villageTroops = result.Second;
			
			Debug.Log("Allied ammount: "+localPlayer.Units.Units.Count);
			Debug.Log("Enemy ammount: "+villageTroops.Units.Count);
			
			// Check end of combat
			if(localPlayer.Units.Units.Count == 0){
				// Game over :(
				gameOver("Your forces have been defeated by the forest folk!", false);
				
			}else if(villageTroops.Units.Count == 0){
				// Win :)	
				
				isGoClicked = true;
				localPlayer.Food += 6;
				localPlayer.Gold += 500;
				villageChoice = 0;
			}
		}else if(villageChoice == PAY_VILLAGE){
			isGoClicked = true;
			localPlayer.Food += 4;
			localPlayer.Gold -= 1000;
			villageChoice = 0;
		}
	}
	if (castleChoice > 0) {
		if(castleChoice == ATTACK_CASTLE){
			int total = localPlayer.Units.Units.Count+castleTroops.Units.Count;
			float odds = (float)localPlayer.Units.Units.Count/(float)total;
			odds = Mathf.Max(odds, 0.35f);
			//Debug.Log("total::odds = "+total+"::"+odds);
			Tuple<Troop, Troop> result = GameController.combatTurn(localPlayer.Units,castleTroops,odds);
			localPlayer.Units = result.First;
			castleTroops = result.Second;
			
			Debug.Log("Allied ammount: "+localPlayer.Units.Units.Count);
			Debug.Log("Enemy ammount: "+castleTroops.Units.Count);
			
			// Check end of combat
			if(localPlayer.Units.Units.Count == 0){
				// Game over :(
				gameOver("Your forces have been defeated by the castellan's!", false);
				
			}else if(castleTroops.Units.Count == 0){
				// Win :)	
				gameOver("You defeated the castellan forces! The castle is yours.", true);
				
				isGoClicked = true;
				localPlayer.Food += 10;
				localPlayer.Gold += 1500;
				castleChoice = 0;
			}
		}else if(castleChoice == DUEL_CASTLE){
			// Roll a die
			int result = Random.Range(1,20);
			if(result>10){
				// Win :)
				gameOver("Your champion won! The castle is yours.", true);
			}else{
				// Game Over :(
				gameOver("Your champion lost!", false);
			}
		}
	}*/
	/*********************************************************************/
}

/*********************************************************************/
/************************* AUXILIAR FUNCTIONS ************************/
/*********************************************************************/

void gameOver(string msg, bool win){
	string title = "";
	string ok = "";
	
	if (win) {
		title = "You win!";
		ok = "Proceed";
		localPlayer.Game = new bool[]{true};
	} else {
		title = "You lose!";
		ok = "Try again";
	}
	if (EditorUtility.DisplayDialog (title, //title
	                                 msg, // text
	                                 ok)) { // yes, no
		Application.LoadLevel ("mainScene");
	}
}

void starving(){
	int score = localPlayer.Food - localPlayer.Travelling;
	if (score < 0) {
		score = Mathf.Abs(score);
		// Soldiers die due to starvation
		for(int i = 1; i< Mathf.Min(score*2,localPlayer.Units.Units.Count); i++){
			localPlayer.Units.Units.RemoveAt(i);
			localPlayer.Units.Deaths++;
		}
	}
}

void updateBars(){
	//updateBar ("Personagem", playerTroopsMax, localPlayer.Units.Units.Count);
	updateBar ("Mahaja-Outpost", castleTroopsMax, castleTroops.Units.Count);
	updateBar ("Mahaja-Castle", villageTroopsMax, villageTroops.Units.Count);				
	updateBar ("Icegard-Castle", villageTroopsMax, villageTroops.Units.Count);
	updateBar ("Gentlehood-Castle", villageTroopsMax, villageTroops.Units.Count);
}

void updateBar(string objName, int total, int current){
	GameObject bar = GameObject.Find (objName+"/Foreground-bar");
	Vector3 scale = bar.transform.localScale;
	
	// 200 is the total scale
	scale.x = 200 * ((float)current / (float)total);
	bar.transform.localScale = scale;
}

void updateLog(){
	GameObject log = GameObject.Find ("Log");
	UILabel content = log.GetComponent<UILabel> ();
	
	int swordmans = 0, knights = 0, archers = 0, wounded = 0;
	int dead = localPlayer.Units.Deaths;
	
	for (int i = 0; i<localPlayer.Units.Units.Count; i++) {
		if(localPlayer.Units.Units[i].Type.Equals(Unit.SWORDMAN)){
			swordmans++;
		}else if(localPlayer.Units.Units[i].Type.Equals(Unit.KNIGHT)){
			knights++;
		}else if(localPlayer.Units.Units[i].Type.Equals(Unit.ARCHER)){
			archers++;
		}
		
		if(localPlayer.Units.Units[i].Health < 100){
			wounded++;
		}
	}
	
	content.text = "Combat log\n\n" +
		"Swordmans: " + swordmans + "\n" +
			"Knights: " + knights + "\n" +
			"Archers: " + archers + "\n\n" +
			"Dead soldiers: " + dead + "\n" +
			"Wounded soldiers: " + wounded + "\n";
	
	// For debug only
	//"Forest folk: "+villageTroops.Units.Count+"\n"+
	//"Dead villagers: "+villageTroops.Deaths;
}

void updateStatus(){
	GameObject status = GameObject.Find ("Status");
	UILabel content = status.GetComponent<UILabel> ();
	
	string moral = "";
	if (localPlayer.Moral == 0) {
		moral = "stable";
	}else if(localPlayer.Moral < 0 && localPlayer.Moral >= -50){
		moral = "low";
	}
	else if(localPlayer.Moral > 0 && localPlayer.Moral <= 50){
		moral = "graceful";
	}else if(localPlayer.Moral < -50){
		moral = "critical";
	}else if(localPlayer.Moral > 50){
		moral = "untouchable";
	}
	
	content.text = "Gold: "+localPlayer.Gold+"g\n"+
		"Food: "+Mathf.Max((localPlayer.Food-localPlayer.Travelling),0)+" day(s) worth\n"+
			"Troop's moral: "+moral+"\n"+
			"Days traveling: "+localPlayer.Travelling+" day";
}

bool moveTo(int route){
	
	GameObject personagem = GameObject.Find ("Personagem");
	bool stopped = false;
	
	// Check if personagem reached next label
	if(Mathf.Abs(personagem.transform.position.x - target.x) >= 0.01f &&
	   Mathf.Abs(personagem.transform.position.y - target.y) >= 0.01f &&
	   !halt){
		// current position
		Vector3 current = personagem.transform.position;
		// 0.0 for default
		Vector3 currentVelocity = new Vector3(0.0f,0.0f,0.0f);
		
		float smoothTime = 0.2f;
		
		personagem.transform.position = Vector3.SmoothDamp (current, target, ref currentVelocity, smoothTime);
	}else{
		// Get another label
		GameObject nextLabel = FindClosestLabel(route);
		if(nextLabel != null){
			
			// Each 3 labels crossed = 1 day of journey
			labelsCrossed++;
			if(labelsCrossed == 3){
				labelsCrossed = 0;
				localPlayer.Travelling++;
				starving();
			}
			
			target = new Vector3(nextLabel.transform.position.x,
			                     nextLabel.transform.position.y,
			                     personagem.transform.position.z);
			halt = false;
		}else{
			stopped = true;
		}
	}
	
	return stopped;
}

void show(int idx, int route){
	Debug.Log ("Show");
	GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route-"+route);
	
	Vector3 pos = labels[idx].transform.position;
	pos.z = -0.05f;
	labels[idx].transform.position = pos;
}

void OnGoClicked(){
	if (!mahajaClicked && !icegardClicked) {
		if (EditorUtility.DisplayDialog ("No route selected!", //title
		                                 "You must select a route to one of the castles.\n" +
		                                 "You can either go through the outpost village ou direct to the desired castle", // text
		                                 "OK")) { // yes, no
			//isGoClicked = true;
		}
	} else {
		string info = "";

		if(mahajaClicked) info = "Hint: Attacking Mahaja will declare war against their kingdom.\n" +
									"They will become your enemies and many attempts of peace will be lost.";
		else info = "Hint: Attacking Icegard might be a dificult task since they are remarcable figthers," +
						"but will provide much information about their culture and strategy.";

		if(outpostClicked) info += "\n\nAlso: Attacking the outpost will take more time of travel and resources.";
		else info += "\n\nAlso: Going directly is a risky strategy since you might be attacked by two fronts.";

		if (EditorUtility.DisplayDialog ("March to Battle!", //title
		                                 "Are you sure you want to follow this route to battle?\n\n"+info, // text
		                                 "Yes", "No")) { // yes, no
			isGoClicked = true;
			GameObject btnGo = GameObject.Find ("btnGo");
			GameObject btnCancel = GameObject.Find ("btnCancel");
			
			btnGo.SetActive(false);
			btnCancel.SetActive(false);
		}
	}
}

void onCancelClicked(){
	// Clear lines
	for (int i = 1; i<=5; i++) {
		GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route-"+i);
		
		foreach (GameObject label in labels){
			Vector3 pos = label.transform.position;
			pos.z = 0.05f;
			label.transform.position = pos;
		}
	}
	
	// Clear variables
	outpostClicked = false;
	mahajaClicked = false;
	icegardClicked = false;
}

GameObject FindClosestLabel(int route) {
	GameObject personagem = GameObject.Find ("Personagem");
	
	GameObject[] gos;
	gos = GameObject.FindGameObjectsWithTag("Route-"+route);
	GameObject closest = null;
	float distance = Mathf.Infinity;
	Vector3 position = personagem.transform.position;
	foreach (GameObject go in gos) {
		Vector3 diff = go.transform.position - position;
		float curDistance = diff.sqrMagnitude;
		if (curDistance < distance && go.transform.localScale.x == 28f) {
			closest = go;
			distance = curDistance;
		}
	}
	
	// Marking as visited (BEWARE: NOT SAFE)
	if (closest != null) {
		Vector3 scale = closest.transform.localScale;
		scale.x = 27f;
		closest.transform.localScale = scale;
		//closest.renderer.material.color = Color.green;
	}
	
	return closest;
}

/***************************** Village Script ***************************/
/*void villageEvent(){
	if (EditorUtility.DisplayDialog ("You arrived at the forest village", //title
	                                 "People from the village are not allied of your reign and seem a bit hostile.\n" +
	                                 "They don't have enough weaponry and a combat would favor your army.\n" +
	                                 "You can either plunder the village or offer them gold for your stay.", // text
	                                 "Attack", "Pay 1000g")) { // yes, no
		villageChoice = ATTACK_VILLAGE;
	}else{
		villageChoice = PAY_VILLAGE;
	}
}*/
/************************************************************************/

/***************************** Castle Script ***************************/
/*void castleEvent(){
	if (EditorUtility.DisplayDialog ("You found the castle!", //title
	                                 "The castle is very fortified. Attacking directly might cost you many fighters.\n"+
	                                 "Another option is to challange the castellan, your best champion against his.",
	                                 "Direct attack", "Duel")) { // yes, no
		castleChoice = ATTACK_CASTLE;
	}else{
		castleChoice = DUEL_CASTLE;
	}
}*/
/************************************************************************/
}
#endif 