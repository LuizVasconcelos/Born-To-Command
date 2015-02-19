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
	private Troop icegardTroops;
	private int icegardTroopsMax;
	private Troop mahajaTroops;
	private int mahajaTroopsMax;
	private Troop outpostTroops;
	private int outpostTroopsMax;
	
	// define routes
	private readonly int ROUTE_HOME_TO_ICEGARD = 1;
	private readonly int ROUTE_HOME_TO_OUTPOST = 2;
	private readonly int ROUTE_HOME_TO_MAHAJA = 3;
	private readonly int ROUTE_OUTPOST_TO_MAHAJA = 4;
	private readonly int ROUTE_OUTPOST_TO_ICEGARD = 5;
	// define enemy routes
	private readonly int ROUTE_ICEGARD_TO_OUTPOST = 6;
	private readonly int ROUTE_MAHAJA_TO_OUTPOST = 7;
	// define castle choices
	private readonly int CONCEDE = 10;
	private readonly int ATTACK = 11;
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
	private bool enemyArrive;
	private bool justarrived;
	private bool halt;
	private int labelsCrossed;

	// Targets
	private Vector3 target;
	private Vector3 targetIcegard;
	private Vector3 targetMahaja;
	
	// Action buttons
	private bool isGoClicked;
	
	// Actions
	private bool attackIcegard;
	private bool attackMahaja;
	private bool attackOutpost;
	private int icegardChoice;
	private int mahajaChoice;
	
	// Use this for initialization
	void Start () {
		outpostClicked = false;
		icegardClicked = false;
		mahajaClicked = false;	
		
		startTime = Time.time;
		
		isGoClicked = false;
		step2 = false;
		enemyArrive = false;

		attackIcegard = false;
		attackMahaja = false;
		attackOutpost = false;
		icegardChoice = 0;
		mahajaChoice = 0;
		
		//localPlayer = GameManager.player;
		bool[] game = new bool[]{false};
		localPlayer = new Player (1570, 6, Player.generateTroop(50,50,50), 0, 0,game);
		playerTroopsMax = localPlayer.Units.Units.Count;
		icegardTroopsMax = 50;
		mahajaTroopsMax = 50;
		outpostTroopsMax = 80;
		icegardTroops = Player.generateTroop (10,0,40);
		mahajaTroops = Player.generateTroop (0,10,40);
		outpostTroops = Player.generateTroop (60,10,10);
		
		target = GameObject.Find ("Personagem").transform.position;
		targetIcegard = GameObject.Find ("Personagem-Icegard").transform.position;
		targetMahaja = GameObject.Find ("Personagem-Mahaja").transform.position;
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
			// Click is not allowed after enemy movement
			if (hit && enemyArrive){
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

		// Enemy movement (dosen't require any preset)
		if (!enemyArrive) {

			GameObject personagemIcegard = GameObject.Find ("Personagem-Icegard");
			Tuple<bool,Vector3> resultIcegard = moveTo (personagemIcegard, ROUTE_ICEGARD_TO_OUTPOST, targetIcegard);
			bool stoppedIcegard = resultIcegard.First;
			targetIcegard = resultIcegard.Second;

			GameObject personagemMahaja = GameObject.Find ("Personagem-Mahaja");
			Tuple<bool,Vector3> resultMahaja = moveTo (personagemMahaja, ROUTE_MAHAJA_TO_OUTPOST, targetMahaja);
			bool stoppedMahaja = resultMahaja.First;
			targetMahaja = resultMahaja.Second;

			if(stoppedMahaja && stoppedIcegard){
				enemyArrive = true;
				reset (ROUTE_ICEGARD_TO_OUTPOST);
				reset (ROUTE_MAHAJA_TO_OUTPOST);
			}
		}

		// Allied movement
		if(isGoClicked){
			// Check desired route
			GameObject personagem = GameObject.Find ("Personagem");
			
			// Indirect route
			if(outpostClicked){
				if(step2 && icegardClicked){
					Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_OUTPOST_TO_ICEGARD, target);
					bool stopped = result.First;
					target = result.Second;
					
					if(stopped){
						// Here starts the icegard battle
						attackIcegard = true;
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}else if(step2 && mahajaClicked){
					Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_OUTPOST_TO_MAHAJA, target);
					bool stopped = result.First;
					target = result.Second;
					
					// no more labels in this route
					if(stopped){
						// Here starts the mahaja battle
						attackMahaja = true;
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}else{
					Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_HOME_TO_OUTPOST, target);
					bool stopped = result.First;
					target = result.Second;
					
					// no more labels in this route
					if(stopped){
						step2 = true;
						
						// Here starts the outpost battle
						attackOutpost = true;
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}
				// Direct routes
			}else if(mahajaClicked){
				Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_HOME_TO_MAHAJA, target);
				bool stopped = result.First;
				target = result.Second;
				
				if(stopped){
					// Here starts the mahaja battle
					attackMahaja = true;
					
					// We halt the GO during the combat
					isGoClicked = false;
				}
			}else if(icegardClicked){
				Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_HOME_TO_ICEGARD, target);
				bool stopped = result.First;
				target = result.Second;
				
				if(stopped){
					// Here starts the icegard battle
					attackIcegard = true;
					
					// We halt the GO during the combat
					isGoClicked = false;
				}
			}else if(icegardChoice == ATTACK){
				if(!step2){
					Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_ICEGARD_TO_OUTPOST, target);
					bool stopped = result.First;
					target = result.Second;
					
					if(stopped){
						step2 = true;

						// Here starts the outpost battle
						attackOutpost = true;
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}else{
					Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_MAHAJA_TO_OUTPOST, target);
					bool stopped = result.First;
					target = result.Second;
					
					if(stopped){
						// Here starts the mahaja battle
						attackMahaja = true;
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}
			}else if(mahajaChoice == ATTACK){
				if(!step2){
					Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_MAHAJA_TO_OUTPOST, target);
					bool stopped = result.First;
					target = result.Second;
					
					if(stopped){
						step2 = true;
						
						// Here starts the outpost battle
						attackOutpost = true;
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}else{
					Tuple<bool,Vector3> result = moveTo (personagem,ROUTE_ICEGARD_TO_OUTPOST, target);
					bool stopped = result.First;
					target = result.Second;
					
					if(stopped){
						// Here starts the mahaja battle
						attackIcegard = true;
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}
			}
		}
		/*********************************************************************/
		
		/**************************** PHASE 3 ********************************/
		if (attackOutpost) {
			Tuple<int, int> odds = new Tuple<int,int>(1,2);

			Tuple<Troop, Troop> result = GameController.combatTurn(localPlayer.Units,outpostTroops,odds);
			localPlayer.Units = result.First;
			outpostTroops = result.Second;
			
			Debug.Log("Allied ammount: "+localPlayer.Units.Units.Count);
			Debug.Log("Enemy ammount: "+outpostTroops.Units.Count);
			
			// Check end of combat
			if(localPlayer.Units.Units.Count == 0){
				// Game over :(
				gameOver("Your forces have been defeated during the battle for the outpost!", false);
				
			}else if(outpostTroops.Units.Count == 0){
				// Win :)	
				
				isGoClicked = true;
				localPlayer.Food += 6;
				localPlayer.Gold += 1570;
				attackOutpost = false;
			}
		}
		if (attackIcegard) {
			Tuple<int, int> odds = new Tuple<int,int>(2,3);

			Tuple<Troop, Troop> result = GameController.combatTurn(localPlayer.Units,icegardTroops,odds);
			localPlayer.Units = result.First;
			icegardTroops = result.Second;
			
			Debug.Log("Allied ammount: "+localPlayer.Units.Units.Count);
			Debug.Log("Enemy ammount: "+icegardTroops.Units.Count);
			
			// Check end of combat
			if(localPlayer.Units.Units.Count == 0){
				// Game over :(
				gameOver("Your forces have been defeated by the castellan's!", false);
				
			}else if(icegardTroops.Units.Count == 0){
				if(mahajaChoice == 0){
					// Clear routes and targets, then generate new enemy at outpost
					
					isGoClicked = true;
					localPlayer.Food += 9;
					localPlayer.Gold += 1220;
					
					attackIcegard = false;
					attackOutpost = false;
					attackMahaja = false;
					step2 = false;
					
					outpostClicked = false;
					mahajaClicked = false;
					icegardClicked = false;
					
					outpostTroopsMax = 20;
					outpostTroops = Player.generateTroop (0,15,5);
					
					// Call aftermatch event
					IcegardEvent();
				}else{
					// Win :)	
					gameOver("You defeated the castellan forces! The castle is yours.", true);
				}
			}
		}
		if (attackMahaja) {
			Tuple<int, int> odds = new Tuple<int,int>(3,4);

			Tuple<Troop, Troop> result = GameController.combatTurn(localPlayer.Units,mahajaTroops,odds);
			localPlayer.Units = result.First;
			mahajaTroops = result.Second;
			
			Debug.Log("Allied ammount: "+localPlayer.Units.Units.Count);
			Debug.Log("Enemy ammount: "+mahajaTroops.Units.Count);
			
			// Check end of combat
			if(localPlayer.Units.Units.Count == 0){
				// Game over :(
				gameOver("Your forces have been defeated by the castellan's!", false);
				
			}else if(mahajaTroops.Units.Count == 0){
				if(icegardChoice == 0){
					// Clear routes and targets, then generate new enemy at outpost
					
					isGoClicked = true;
					localPlayer.Food += 9;
					localPlayer.Gold += 1220;
					
					attackIcegard = false;
					attackOutpost = false;
					attackMahaja = false;
					step2 = false;
					
					outpostClicked = false;
					mahajaClicked = false;
					icegardClicked = false;
					
					outpostTroopsMax = 30;
					outpostTroops = Player.generateTroop (20,10,0);
					
					// Call aftermatch event
					MahajaEvent();
				}else{
					// Win :)	
					gameOver("You defeated the castellan forces! The castle is yours.", true);
				}
			}
		}
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
		updateBar ("Gentlehood-Outpost", outpostTroopsMax, outpostTroops.Units.Count);
		updateBar ("Mahaja-Castle", mahajaTroopsMax, mahajaTroops.Units.Count);				
		updateBar ("Icegard-Castle", icegardTroopsMax, icegardTroops.Units.Count);
		// TODO: stablish values
		updateBar ("Gentlehood-Castle", 1, 1);
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

	Tuple<bool,Vector3> moveTo(GameObject personagem, int route, Vector3 localtarget){
		
		//GameObject personagem = GameObject.Find ("Personagem");
		bool stopped = false;
		
		// Check if personagem reached next label
		if(Mathf.Abs(personagem.transform.position.x - localtarget.x) >= 0.01f &&
		   Mathf.Abs(personagem.transform.position.y - localtarget.y) >= 0.01f &&
		   !halt){
			// current position
			Vector3 current = personagem.transform.position;
			// 0.0 for default
			Vector3 currentVelocity = new Vector3(0.0f,0.0f,0.0f);
			
			float smoothTime = 0.2f;
			
			personagem.transform.position = Vector3.SmoothDamp (current, localtarget, ref currentVelocity, smoothTime);
		}else{
			// Get another label
			GameObject nextLabel = FindClosestLabel(personagem,route);
			if(nextLabel != null){
				
				// Each 3 labels crossed = 1 day of journey
				if(personagem.name.Equals("Personagem")){
					labelsCrossed++;
					if(labelsCrossed == 3){
						labelsCrossed = 0;
						localPlayer.Travelling++;
						starving();
					}
				}
		
				localtarget = new Vector3(nextLabel.transform.position.x,
				                     nextLabel.transform.position.y,
				                     nextLabel.transform.position.y-1f);
				halt = false;
			}else{
				stopped = true;
			}
		}
		
		return new Tuple<bool,Vector3>(stopped,localtarget);
	}

	void reset(int route){
		Debug.Log ("Reset");
		GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route-"+route);

		for (int i = 0; i<labels.Length; i++) {
			Vector3 scale = labels[i].transform.localScale;
			scale.x = 28f;
			labels[i].transform.localScale = scale;
		}
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
			                                 "You can either go through the outpost village or direct to the desired castle.", // text
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

	GameObject FindClosestLabel(GameObject personagem,int route) {
		//GameObject personagem = GameObject.Find ("Personagem");
		
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

	/***************************** Mahaja Script ***************************/
	void MahajaEvent(){
		if (EditorUtility.DisplayDialog ("Aftermatch!", //title
		                                 "The castle is now yours. But the outpost has been taken by the forces of icegard.\n"+
		                                 "You can concede the outpost to them and avoid more battles, or attack them as well.",
		                                 "Concede", "Attack")) { // yes, no
			mahajaChoice = CONCEDE;
			gameOver("You defeated the castellan forces! The castle is yours.", true);
		}else{
			mahajaChoice = ATTACK;
		}
	}
	/************************************************************************/

	/***************************** Icegard Script ***************************/
	void IcegardEvent(){
		if (EditorUtility.DisplayDialog ("Aftermatch!", //title
		                                 "The castle is now yours. But the outpost has been taken by the forces of mahaja.\n"+
		                                 "You can concede the outpost to them for 1000g and avoid more battles, or attack them as well.",
		                                 "Concede, gain 1000g", "Attack")) { // yes, no
			localPlayer.Gold += 1000;
			icegardChoice = CONCEDE;
			gameOver("You defeated the castellan forces! The castle is yours.", true);
		}else{
			icegardChoice = ATTACK;
		}
	}
	/************************************************************************/
}
#endif 