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

		localPLayer = new Player (1570, 12, generateTroop(3), 0, 1);
		villageTroops = generateTroop (2);
		castleTroops = generateTroop (60);

		target = GameObject.Find ("Personagem").transform.position;
		halt = true;

	}
	
	// Update is called once per frame
	void Update () {

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
				Tuple<Troop, Troop> result = combatTurn(localPLayer.Units,villageTroops,0.5f);
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
					villageChoice = 0;
				}
			}else if(villageChoice == PAY_VILLAGE){
				isGoClicked = true;
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
		                                 "Attack", "Pay")) { // yes, no
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
		int garther = 2;
		int alliedPortion = Mathf.CeilToInt (garther * odds);
		int enemyPortion = Mathf.FloorToInt (garther * (1-odds));

		// Allies damage enemies
		Debug.Log("PLAYER PHASE");
		for (int i = 0; i<alliedPortion; i++) {
			enemyTroops = damage(enemyTroops, alliedTroops.Units[i].Attack, enemyPortion);
		}

		if (enemyTroops.Units.Count > 0) {
			Debug.Log("ENEMY PHASE");
			/// Enemies damage allies
			for (int i = 0; i<enemyPortion; i++) {
				alliedTroops = damage(alliedTroops, enemyTroops.Units[i].Attack, alliedPortion);
			}
		}

		return new Tuple<Troop, Troop>(alliedTroops, enemyTroops);
	}

	Troop damage(Troop defender, int damage, int ammount){
		for (int i = 0; i<Mathf.Min(ammount,defender.Units.Count); i++) {
			int damageDealt = (int) (damage*defender.Units[i].Armor);

			Debug.Log("A unit dealt "+damageDealt+" damage!\n" +
			          "Health: "+defender.Units[i].Health+"->"+(defender.Units[i].Health - damageDealt));

			defender.Units[i].Health -=  damageDealt;
			if(defender.Units[i].Health <= 0){
				defender.Units.RemoveAt(i);
			}
		}
		return defender;
	}

	// Dummy function
	Troop generateTroop(int size){
		Troop units = new Troop (new List<Unit>(size));
		for (int i = 0; i<size; i++) {
			Unit u = new Unit("",Random.Range(90, 100),Random.Range(10, 30),Random.Range(0.0f, 0.4f));
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
