using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Mission1Script : MonoBehaviour {

	// Players variables
	private Player localPLayer;
	private Troop villageTroops;
	private Troop castleTroops;

	// define routes
	private readonly int ROUTE_CAMP_TO_VILLAGE = 1;
	private readonly int ROUTE_VILLAGE_TO_CASTLE = 2;
	private readonly int ROUTE_CAMP_TO_CASTLE = 3;
	// define village choices
	private readonly int ATTACK_VILLAGE = 4;
	private readonly int PAY_VILLAGE = 5;
	// define castle choices
	private readonly int DIRECT_ATTACK_CASTLE = 6;
	private readonly int SURPRISE_ATTACK_CASTLE = 7;

	private int index;
	private bool village3Clicked;
	private bool castleClicked;
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
		village3Clicked = false;
		castleClicked = false;
		startTime = Time.time;

		isGoClicked = false;
		step2 = false;
		villageChoice = 0;
		castleChoice = 0;

		localPLayer = new Player (1570, 6, generateTroop(50), 0, 0);
		villageTroops = generateTroop (30);
		castleTroops = generateTroop (60);

		target = GameObject.Find ("Personagem").transform.position;
		halt = true;

		labelsCrossed = 0;
	}
	
	// Update is called once per frame
	void Update () {

		// Update log and status text
		updateStatus ();
		updateLog ();

		/**************************** PHASE 1 ********************************/

		// Lines exposition
		if (village3Clicked && index >= 0) {
			if(castleClicked){
				show (index,ROUTE_VILLAGE_TO_CASTLE);
				index--;
			}else{
				show (index,ROUTE_CAMP_TO_VILLAGE);
				index--;
			}
		}else if(castleClicked && index >= 0){
			show (index,ROUTE_CAMP_TO_CASTLE);
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
				if (hitInfo.transform.gameObject.name == "Village3-obj" && !castleClicked){
					village3Clicked = true;
					index = GameObject.FindGameObjectsWithTag("Route-1").Length-1;
				// Clicked the castle
				} else if(hitInfo.transform.gameObject.name == "Castle-obj"){
					updateStatus();
					castleClicked = true;

					// Indirect route
					if(village3Clicked){
						index = GameObject.FindGameObjectsWithTag("Route-2").Length-1;
					// Direct route
					}else{
						index = GameObject.FindGameObjectsWithTag("Route-3").Length-1;
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
			if(village3Clicked ){
				if(step2){
					bool stopped = moveTo (ROUTE_VILLAGE_TO_CASTLE);

					if(stopped){
						// Here starts the castle script per se
						castleEvent();
						
						// We halt the GO during the combat
						isGoClicked = false;
					}
				}else{
					bool stopped = moveTo (ROUTE_CAMP_TO_VILLAGE);
						
					// no more labels in this route
					if(stopped){
						step2 = true;

						// Here starts the village script per se
						villageEvent();

						// We halt the GO during the combat
						isGoClicked = false;
					}
				}
				// Direct route
			}else{
				bool stopped = moveTo (ROUTE_CAMP_TO_CASTLE);

				if(stopped){
					// Here starts the castle script per se
					castleEvent();

					// We halt the GO during the combat
					isGoClicked = false;
				}
			}
		}
		/*********************************************************************/

		/**************************** PHASE 3 ********************************/
		if (villageChoice > 0) {
			if(villageChoice == ATTACK_VILLAGE){
				int total = localPLayer.Units.Units.Count+villageTroops.Units.Count;
				float odds = (float)localPLayer.Units.Units.Count/(float)total;
				//Debug.Log("total::odds = "+total+"::"+odds);
				Tuple<Troop, Troop> result = combatTurn(localPLayer.Units,villageTroops,odds);
				localPLayer.Units = result.First;
				villageTroops = result.Second;

				Debug.Log("Allied ammount: "+localPLayer.Units.Units.Count);
				Debug.Log("Enemy ammount: "+villageTroops.Units.Count);

				// Check end of combat
				if(localPLayer.Units.Units.Count == 0){
					// Game over :(

				}else if(villageTroops.Units.Count == 0){
					// Win :)	

					isGoClicked = true;
					localPLayer.Food += 6;
					localPLayer.Gold += 500;
					villageChoice = 0;
				}
			}else if(villageChoice == PAY_VILLAGE){
				isGoClicked = true;
				localPLayer.Food += 4;
				localPLayer.Gold -= 1000;
				villageChoice = 0;
			}
		}
		if (castleChoice > 0) {
			if(castleChoice == DIRECT_ATTACK_CASTLE){

			}else if(castleChoice == SURPRISE_ATTACK_CASTLE){

			}
		}
		/*********************************************************************/
	}

	/*********************************************************************/
	/************************* AUXILIAR FUNCTIONS ************************/
	/*********************************************************************/

	void starving(){
		int score = localPLayer.Food - localPLayer.Travelling;
		if (score < 0) {
			score = Mathf.Abs(score);
			// Soldiers die due to starvation
			for(int i = 1; i< Mathf.Min(score*2,localPLayer.Units.Units.Count); i++){
				localPLayer.Units.Units.RemoveAt(i);
				localPLayer.Units.Deaths++;
			}
		}
	}

	void updateLog(){
		GameObject log = GameObject.Find ("Log");
		UILabel content = log.GetComponent<UILabel> ();

		int swordmans = 0, knights = 0, archers = 0, wounded = 0;
		int dead = localPLayer.Units.Deaths;

		for (int i = 0; i<localPLayer.Units.Units.Count; i++) {
			if(localPLayer.Units.Units[i].Type.Equals(Unit.SWORDMAN)){
				swordmans++;
			}else if(localPLayer.Units.Units[i].Type.Equals(Unit.KNIGHT)){
				knights++;
			}else if(localPLayer.Units.Units[i].Type.Equals(Unit.ARCHER)){
				archers++;
			}

			if(localPLayer.Units.Units[i].Health < 100){
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
		if (localPLayer.Moral == 0) {
			moral = "stable";
		}else if(localPLayer.Moral < 0 && localPLayer.Moral >= -50){
			moral = "low";
		}
		else if(localPLayer.Moral > 0 && localPLayer.Moral <= 50){
			moral = "graceful";
		}else if(localPLayer.Moral < -50){
			moral = "critical";
		}else if(localPLayer.Moral > 50){
			moral = "untouchable";
		}

		content.text = "Gold: "+localPLayer.Gold+"g\n"+
					   	"Food: "+Mathf.Max((localPLayer.Food-localPLayer.Travelling),0)+" day(s) worth\n"+
						"Troop's moral: "+moral+"\n"+
						"Days traveling: "+localPLayer.Travelling+" day";
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
					localPLayer.Travelling++;
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
		GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route-"+route);

		Vector3 pos = labels[idx].transform.position;
		pos.z = 0.0f;
		labels[idx].transform.position = pos;
	}

	void OnGoClicked(){
		if (!castleClicked) {
			if (EditorUtility.DisplayDialog ("No route selected!", //title
			                                 "You must select a route to siege the castle.\n" +
			                                 "You can either go through the forest village ou direct to the castle", // text
			                                 "OK")) { // yes, no
				//isGoClicked = true;
			}
		} else {
			string info = "";
			if(village3Clicked) info = "Hint: The forest folk might be dangerous.";
			else info = "Hint: Going directly might exhaust your resources on the journey.";
			if (EditorUtility.DisplayDialog ("March to Battle!", //title
			                                 "Are you sure you want to follow this route to battle?\n"+info, // text
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
		for (int i = 1; i<=3; i++) {
			GameObject[] labels =  GameObject.FindGameObjectsWithTag("Route-"+i);

			foreach (GameObject label in labels){
				Vector3 pos = label.transform.position;
				pos.z = 0.05f;
				label.transform.position = pos;
			}
		}

		// Clear variables
		village3Clicked = false;
		castleClicked = false;
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
	void villageEvent(){
		if (EditorUtility.DisplayDialog ("You arrived at the forest village", //title
		                                 "People from the village are not allied of your reign and seem a bit hostile.\n" +
		                                 "They don't have enough weaponry and a combat would favor your army.\n" +
		                                 "You can either plunder the village or offer them gold for your stay.", // text
		                                 "Attack", "Pay 1000g")) { // yes, no
			villageChoice = ATTACK_VILLAGE;
		}else{
			villageChoice = PAY_VILLAGE;
		}
	}
	/************************************************************************/

	/***************************** Castle Script ***************************/
	void castleEvent(){
		if (EditorUtility.DisplayDialog ("You found the castle!", //title
		                                 "They didn't see your troops yet.\n" +
		                                 "You can go for the battle directly or prepare your army to a surprise attack.\n" +
		                                 "Hint: For surprise attack, you need more supplies to wait for the best time to go for it.\n", // text
		                                 "Direct attack", "Surprise attack")) { // yes, no
			castleChoice = DIRECT_ATTACK_CASTLE;
		}else{
			castleChoice = SURPRISE_ATTACK_CASTLE;
		}
	}
	/************************************************************************/

	/*************************** Combat Script ******************************/
	Tuple<Troop, Troop> combatTurn(Troop alliedTroops, Troop enemyTroops, float odds){
		// Number of units in combat per turn
		int garther = 20;
		float alliedOdds = garther * odds;
		float enemyOdds = garther * (1-odds);
		int alliedPortion = Mathf.Min(Mathf.CeilToInt (alliedOdds),19);
		int enemyPortion = Mathf.Max(Mathf.FloorToInt (enemyOdds),1);

		// Allies damage enemies
		Debug.Log("PLAYER PHASE ("+alliedPortion+")");
		for (int i = 0; i<alliedPortion; i++) {
			enemyTroops = damage(enemyTroops, alliedTroops.Units[i], alliedPortion);
		}

		if (enemyTroops.Units.Count > 0) {
			Debug.Log("ENEMY PHASE ("+enemyPortion+")");
			/// Enemies damage allies
			for (int i = 0; i<enemyPortion; i++) {
				alliedTroops = damage(alliedTroops, enemyTroops.Units[i], enemyPortion);
			}
		}

		return new Tuple<Troop, Troop>(alliedTroops, enemyTroops);
	}

	Troop damage(Troop defenders, Unit attacker, int ammount){
		int damage = attacker.RollAttackDie ();

		for (int i = 0; i<Mathf.Min(ammount,defenders.Units.Count); i++) {
			Unit defender = defenders.Units[i];
			float rawDamage = damage*defender.Armor;

			// Rock-paper-scisors bonuses
			if(attacker.Type.Equals(Unit.KNIGHT) && defender.Type.Equals(Unit.SWORDMAN)){
				rawDamage *= 1.3f;
			}else if(attacker.Type.Equals(Unit.SWORDMAN) && defender.Type.Equals(Unit.ARCHER)){
				rawDamage *= 1.25f;
			}else if(attacker.Type.Equals(Unit.ARCHER) && defender.Type.Equals(Unit.KNIGHT)){
				rawDamage *= 1.75f;
			}

			int damageDealt = Mathf.CeilToInt(rawDamage);

			Debug.Log("A unit dealt "+damageDealt+"("+damage+"*"+defender.Armor+") damage!\n" +
			          "Health: "+defender.Health+"->"+(defender.Health - damageDealt));

			defender.Health -=  damageDealt;
			if(defender.Health <= 0){
				defenders.Units.RemoveAt(i);
				defenders.Deaths += 1;
			}
		}
		return defenders;
	}

	// Dummy function
	Troop generateTroop(int size){
		Troop units = new Troop (new List<Unit>(size));
		for (int i = 0; i<size; i++) {
			int type = Random.Range(1,4);
			Unit u = null;
			switch(type){
				case 1:
					u = new Unit(Unit.SWORDMAN,100,5,15,0.2f);
					break;
				case 2:
					u = new Unit(Unit.KNIGHT,100,2,35,0.35f);
					break;
				case 3:
					u = new Unit(Unit.ARCHER,100,100,1,0.05f);
					break;
				default:
					u = new Unit(Unit.SWORDMAN,100,5,15,0.2f);
					break;
			}
			/*Debug.Log("Unit:: \n" +
				"Health: "+u.Health+"\n" +
			    "Attack: "+u.Attack+"\n" +
			    "Armor: "+u.Armor);*/
			units.Units.Add(u);
		}

		return units;
	}
	/************************************************************************/
}
