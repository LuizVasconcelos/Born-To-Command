using UnityEngine;
using System.Collections;
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

	private int index;
	private bool village3Clicked;
	private bool castleClicked;
	private float startTime;
	private bool step2;
	private bool justarrived;

	// Action buttons
	private bool isGoClicked;

	// Actions
	private int villageChoice;

	// Use this for initialization
	void Start () {
		village3Clicked = false;
		castleClicked = false;
		startTime = Time.time;

		isGoClicked = false;
		step2 = false;
		villageChoice = 0;

		localPLayer = new Player (1570, 12, generateTroop(112), 0, 1);
		villageTroops = generateTroop (45);
		castleTroops = generateTroop (60);
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
					GameObject nextLabel = FindClosestLabel(ROUTE_VILLAGE_TO_CASTLE);

					if(nextLabel != null){
						personagem.transform.position = nextLabel.transform.position;
					}
				}else{
					GameObject nextLabel = FindClosestLabel(ROUTE_CAMP_TO_VILLAGE);
						
					// no more labels in this route
					if(nextLabel == null){
						step2 = true;

						// Here starts the village script per se
						villageChoice();

						// We halt the GO during the combat
						isGoClicked = false;
					
					}else{
						personagem.transform.position = nextLabel.transform.position;
					}
				}
				// Direct route
			}else{
				GameObject nextLabel = FindClosestLabel(ROUTE_CAMP_TO_CASTLE);

				if(nextLabel != null){
					personagem.transform.position = nextLabel.transform.position;
				}
			}
		}
		/*********************************************************************/

		/**************************** PHASE 3 ********************************/
		if (villageChoice > 0) {
			if(villageChoice == ATTACK_VILLAGE){

			}else if(villageChoice == PAY_VILLAGE){

			}
		}
		/*********************************************************************/
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
		}
		
		return closest;
	}

	/***************************** Village Script ***************************/
	void villageChoice(){
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

	/*************************** Combat Script ******************************/
	Tuple<Troop, Troop> combatTurn(Troop alliedTroops, Troop enemyTroops, float odds){
		// Number of units in combat per turn
		int garther = 6;
		int alliedPortion = Mathf.Ceil (garther * odds);
		int enemyPortion = Mathf.Floor (garther * (1-odds));

		// Allies damage enemies
		for (int i = 0; i<alliedPortion; i++) {
			enemyTroops = damage(enemyTroops, alliedTroops.Units[i].Attack, enemyPortion);
		}

		/// Enemies damage allies
		for (int i = 0; i<enemyPortion; i++) {
			alliedTroops = damage(alliedTroops, enemyTroops.Units[i].Attack, alliedPortion);
		}

		return new Tuple<alliedTroops, enemyTroops>();
	}

	Troop damage(Troop defender, int damage, int ammount){
		for (int i = 0; i<Mathf.Min(ammount,defender.Units.Capacity); i++) {
			defender.Units[i].Heath -=  damage*defender.Units[i].Armor;
			if(defender.Units[i].Heath < 0){
				defender.Units.RemoveAt(i);
			}
		}
		return defender;
	}

	// Dummy function
	Troop generateTroop(int size){
		Troop units = new Troop (new Unit[size]);
		for (int i = 0; i<size; i++) {
			units.Units[i] = new Unit("",Random.Range(90, 100),Random.Range(1, 30),Random.Range(0.0f, 4.0f));
		}

		return units;
	}
	/************************************************************************/
}
