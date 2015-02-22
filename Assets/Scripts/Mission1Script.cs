using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif 

#if UNITY_EDITOR
public class Mission1Script : MonoBehaviour {

	// Dialogs variables
	private GameObject mainChar;
	private GameObject tutorialScroll;
	private bool tutorialOn;
	private bool charOnPos;
	private bool scrollOnPos;
	private bool charOnPos2;
	private bool scrollOnPos2;
	private int tutorialPhase;
	private GameObject tutorialText;
	private Vector3 originalCharPosition1;
	private Vector3 originalCharPosition2;
	private Vector3 originalScrollPosition1;
	private Vector3 originalScrollPosition2;

	// aux boolean
	private bool canContinue;
	private bool weWin;

	// Players variables
	private Player localPlayer;
	private int playerTroopsMax;
	private Troop villageTroops;
	private int villageTroopsMax;
	private Troop castleTroops;
	private int castleTroopsMax;

	// define routes
	private readonly int ROUTE_CAMP_TO_VILLAGE = 1;
	private readonly int ROUTE_VILLAGE_TO_CASTLE = 2;
	private readonly int ROUTE_CAMP_TO_CASTLE = 3;
	// define village choices
	private readonly int ATTACK_VILLAGE = 4;
	private readonly int PAY_VILLAGE = 5;
	// define castle choices
	private readonly int ATTACK_CASTLE = 6;
	private readonly int DUEL_CASTLE = 7;

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

	// Dialog
	private DialogUtils dialog;
	private bool singleButton;
	private string dialogPos;

	// Use this for initialization
	void Start () {
		dialog = new DialogUtils();
		dialog.hideDialog();

		GameObject.Find("ParticleSystem").particleSystem.Stop();
		village3Clicked = false;
		castleClicked = false;
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

		// Dialogs

		this.mainChar = GameObject.Find ("main2");
		this.tutorialScroll = GameObject.Find ("scrollTutorial2");
		this.originalCharPosition2 = mainChar.transform.position;
		this.originalScrollPosition2 = tutorialScroll.transform.position;

		this.mainChar = GameObject.Find ("main1");
		this.tutorialScroll = GameObject.Find ("scrollTutorial");
		this.tutorialOn = true;
		tutorialPhase = 0;
		this.charOnPos = false;
		this.scrollOnPos = false;
		this.charOnPos2 = false;
		this.scrollOnPos2 = false;

		//NAO MEXER NISSO DAQUI
		this.originalCharPosition1 = mainChar.transform.position;
		//originalCharPosition2 = GameObject.Find("main2").transform.position;
		this.originalScrollPosition1 = tutorialScroll.transform.position;
		//originalScrollPosition2 = GameObject.Find("scrollTutorial2").transform.position;
		///////////////////////

		// aux boolean
		canContinue = false;
		weWin = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Update all
		updateStatus ();
		updateLog ();
		updateBars ();

		// dialogs starts here
		if (tutorialOn) {
			switch (tutorialPhase) {
			case 0:
				if(charOnPos && scrollOnPos) {
					tutorialText = GameObject.Find ("tutorialText1");
					Vector3 labelPosition = tutorialText.transform.position;
					Vector3 newLabelPosition = new Vector3(labelPosition.x, labelPosition.y, -1);
					
					tutorialText.transform.position = newLabelPosition;

					if(Input.GetKeyDown(KeyCode.Return)) tutorialPhase++;
				} else {
					Vector3 charPos = mainChar.transform.position;
					Vector3 scrollPos = tutorialScroll.transform.position;

					Vector3 newCharPos = new Vector3(1.2f, charPos.y, charPos.z);
					Vector3 newScrollPos = new Vector3(-0.2f, scrollPos.y, scrollPos.z);

					Vector3 velocity = new Vector3 (0.0f, 0.0f, 0.0f);
					float smoothTime = 0.15f;

					if(mainChar.transform.position.x >= (1.2f) && !charOnPos && !scrollOnPos) {
						mainChar.transform.position = Vector3.Lerp(charPos, newCharPos, Time.deltaTime * 2.0f);
					} else {
						charOnPos = true;
					}

					if(tutorialScroll.transform.position.x >= (-0.1f) && !scrollOnPos) {
						tutorialScroll.transform.position = Vector3.SmoothDamp(scrollPos, newScrollPos, ref velocity, smoothTime);
					} else {
						scrollOnPos = true;
					}
				}
				break;

			case 1:
				if(charOnPos && scrollOnPos) {
					Vector3 auxPos  = new Vector3(tutorialText.transform.position.x, tutorialText.transform.position.y, 1.0f);
					tutorialText.transform.position = auxPos;
					tutorialText = GameObject.Find ("tutorialText2");
					Vector3 labelPosition = tutorialText.transform.position;
					Vector3 newLabelPosition = new Vector3(labelPosition.x, labelPosition.y, -1);
					
					tutorialText.transform.position = newLabelPosition;

					if(Input.GetKeyDown(KeyCode.Return)) tutorialPhase++;
				}
				break;

			case 2:
				if(charOnPos && scrollOnPos) {
					Vector3 auxPos  = new Vector3(tutorialText.transform.position.x, tutorialText.transform.position.y, 1.0f);
					tutorialText.transform.position = auxPos;
					tutorialText = GameObject.Find ("tutorialText3");
					Vector3 labelPosition = tutorialText.transform.position;
					Vector3 newLabelPosition = new Vector3(labelPosition.x, labelPosition.y, -1);
					
					tutorialText.transform.position = newLabelPosition;
					
					if(Input.GetKeyDown(KeyCode.Return)) tutorialPhase++;
				}
				break;
			
			case 3:
				if(charOnPos && scrollOnPos) {
					Vector3 auxPos = new Vector3(tutorialText.transform.position.x, tutorialText.transform.position.y, 1.0f);
					tutorialText.transform.position = auxPos;
					
					mainChar.transform.position = originalCharPosition1;
					tutorialScroll.transform.position = originalScrollPosition1;

					charOnPos = false;
					scrollOnPos = false;
					
					mainChar = GameObject.Find("main2");
					tutorialScroll = GameObject.Find("scrollTutorial2");
				}

				if(charOnPos2 && scrollOnPos2) {
					tutorialText = GameObject.Find ("tutorialText4");
					Vector3 labelPosition = tutorialText.transform.position;
					Vector3 newLabelPosition = new Vector3(labelPosition.x, labelPosition.y, -1);
					
					tutorialText.transform.position = newLabelPosition;

					if(Input.GetKeyDown(KeyCode.Return)) tutorialPhase++;
				} else {
					Vector3 charPos = mainChar.transform.position;
					Vector3 scrollPos = tutorialScroll.transform.position;
					
					Vector3 newCharPos = new Vector3(-0.9f, charPos.y, charPos.z);
					Vector3 newScrollPos = new Vector3(0.5f, scrollPos.y, scrollPos.z);
					
					Vector3 velocity = new Vector3 (0.0f, 0.0f, 0.0f);
					float smoothTime = 0.15f;
					
					if(mainChar.transform.position.x <= (-0.95f) && !charOnPos && !scrollOnPos) {
						mainChar.transform.position = Vector3.Lerp(charPos, newCharPos, Time.deltaTime * 2.0f);
					} else {
						charOnPos2 = true;
					}
					
					if(tutorialScroll.transform.position.x <= (0.3f) && !scrollOnPos) {
						tutorialScroll.transform.position = Vector3.SmoothDamp(scrollPos, newScrollPos, ref velocity, smoothTime);
					} else {
						scrollOnPos2 = true;
					}
				}
				break;

			case 4:
				if(charOnPos2 && scrollOnPos2) {
					Vector3 auxPos = new Vector3(tutorialText.transform.position.x, tutorialText.transform.position.y, 1.0f);
					tutorialText.transform.position = auxPos;
					
					mainChar.transform.position = originalCharPosition2;
					tutorialScroll.transform.position = originalScrollPosition2;
					
					charOnPos2 = false;
					scrollOnPos2 = false;
					
					mainChar = GameObject.Find("main1");
					tutorialScroll = GameObject.Find("scrollTutorial");
				}

				if(charOnPos && scrollOnPos) {
					tutorialText = GameObject.Find ("tutorialText5");
					Vector3 labelPosition = tutorialText.transform.position;
					Vector3 newLabelPosition = new Vector3(labelPosition.x, labelPosition.y, -1);
					
					tutorialText.transform.position = newLabelPosition;
					
					if(Input.GetKeyDown(KeyCode.Return)) tutorialPhase++;
				} else {
					Vector3 charPos = mainChar.transform.position;
					Vector3 scrollPos = tutorialScroll.transform.position;
					
					Vector3 newCharPos = new Vector3(1.2f, charPos.y, charPos.z);
					Vector3 newScrollPos = new Vector3(-0.2f, scrollPos.y, scrollPos.z);
					
					Vector3 velocity = new Vector3 (0.0f, 0.0f, 0.0f);
					float smoothTime = 0.15f;
					
					if(mainChar.transform.position.x >= (1.2f) && !charOnPos && !scrollOnPos) {
						mainChar.transform.position = Vector3.Lerp(charPos, newCharPos, Time.deltaTime * 2.0f);
					} else {
						charOnPos = true;
					}
					
					if(tutorialScroll.transform.position.x >= (-0.1f) && !scrollOnPos) {
						tutorialScroll.transform.position = Vector3.SmoothDamp(scrollPos, newScrollPos, ref velocity, smoothTime);
					} else {
						scrollOnPos = true;
					}
				}
				break;

			case 5:
				if(tutorialOn) {
					Vector3 auxPos = new Vector3(tutorialText.transform.position.x, tutorialText.transform.position.y, 1.0f);
					tutorialText.transform.position = auxPos;
					
					mainChar.transform.position = originalCharPosition1;
					tutorialScroll.transform.position = originalScrollPosition1;
					
					charOnPos = false;
					scrollOnPos = false;

					tutorialOn = false;
					canContinue = true;
				}
				break;

			case 6:
				if(!weWin) {
					tutorialPhase++;
					return;
				}
				if(charOnPos && scrollOnPos) {
					tutorialText = GameObject.Find ("tutorialText6");
					Vector3 labelPosition = tutorialText.transform.position;
					Vector3 newLabelPosition = new Vector3(labelPosition.x, labelPosition.y, -1);
					
					tutorialText.transform.position = newLabelPosition;
					
					if(Input.GetKeyDown(KeyCode.Return)) tutorialPhase++;
				} else {
					Vector3 charPos = mainChar.transform.position;
					Vector3 scrollPos = tutorialScroll.transform.position;
					
					Vector3 newCharPos = new Vector3(1.2f, charPos.y, charPos.z);
					Vector3 newScrollPos = new Vector3(-0.2f, scrollPos.y, scrollPos.z);
					
					Vector3 velocity = new Vector3 (0.0f, 0.0f, 0.0f);
					float smoothTime = 0.15f;
					
					if(mainChar.transform.position.x >= (1.2f) && !charOnPos && !scrollOnPos) {
						mainChar.transform.position = Vector3.Lerp(charPos, newCharPos, Time.deltaTime * 2.0f);
					} else {
						charOnPos = true;
					}
					
					if(tutorialScroll.transform.position.x >= (-0.1f) && !scrollOnPos) {
						tutorialScroll.transform.position = Vector3.SmoothDamp(scrollPos, newScrollPos, ref velocity, smoothTime);
					} else {
						scrollOnPos = true;
					}
				}
				break;

			/*case 7:
				Application.LoadLevel("mainScene");
				break;*/
			}
		}

		/********************************************************************/
		if(!canContinue) return;

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
				GameObject.Find("ParticleSystem").particleSystem.Play();

				Tuple<int, int> odds = new Tuple<int,int>(1,1);

				Tuple<Troop, Troop> result = GameController.combatTurn(localPlayer.Units,villageTroops,odds);
				localPlayer.Units = result.First;
				villageTroops = result.Second;

				Debug.Log("Allied ammount: "+localPlayer.Units.Units.Count);
				Debug.Log("Enemy ammount: "+villageTroops.Units.Count);

				// Check end of combat
				if(localPlayer.Units.Units.Count == 0){
					// Game over :(
					GameObject.Find("ParticleSystem").particleSystem.Stop();
					gameOver("Your forces have been defeated by the forest folk!", false);

				}else if(villageTroops.Units.Count == 0){
					// Win :)	

					isGoClicked = true;
					localPlayer.Food += 6;
					localPlayer.Gold += 500;
					villageChoice = 0;
					GameObject.Find("ParticleSystem").particleSystem.Stop();
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

				Tuple<int, int> odds = new Tuple<int,int>(1,3);

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
		}
		/*********************************************************************/
	}

	/*********************************************************************/
	/************************* AUXILIAR FUNCTIONS ************************/
	/*********************************************************************/

	void gameOver(string msg, bool win){
		string title = "";
		string ok = "";
		tutorialOn = true;
		canContinue = false;
		weWin = win;
		tutorialPhase++;

		if (win) {
			title = "You win!";
			ok = "Proceed";
			localPlayer.Game = new bool[]{true};
		} else {
			title = "You lose!";
			ok = "Try again";
		
			singleButton = true;
			dialogPos = "finalMessage";
			dialog.showSingleDialogMessage(title, msg, ok);
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
		updateBar ("Castle", castleTroopsMax, castleTroops.Units.Count);
		updateBar ("Village3", villageTroopsMax, villageTroops.Units.Count);				
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
		if (!castleClicked) {
			singleButton = true;
			dialogPos = "notRouteMessage";
			dialog.showSingleDialogMessage("No route selected!",
			                               "You must select a route to siege the castle.\n" +
			                               "You can either go through the forest village ou direct to the castle",
			                               "OK");
			singleButton = true;
			/*if (EditorUtility.DisplayDialog ("No route selected!", //title
			                                 "You must select a route to siege the castle.\n" +
			                                 "You can either go through the forest village ou direct to the castle", // text
			                                 "OK")) { // yes, no
				//isGoClicked = true;
			}*/
		} else {
			string info = "";
			if(village3Clicked) info = "Hint: The forest folk might be dangerous.";
			else info = "Hint: Going directly might exhaust your resources on the journey.";
			singleButton = false;
			dialogPos = "startMessage";
			dialog.showDialogMessage("March to Battle!",
			                         "Are you sure you want to follow this route to battle?\n"+info,
			                         "Yes", "No");
			/*if (EditorUtility.DisplayDialog ("March to Battle!", //title
		                                 "Are you sure you want to follow this route to battle?\n"+info, // text
		                                 "Yes", "No")) { // yes, no*/

			//}
		}
	}

	public void onClickButton1(){
		if(singleButton){
			Debug.Log ("Dialog button 1");
			if(dialogPos == "notRouteMessage"){
				dialog.hideDialog ();
			}else if(dialogPos == "finalMessage"){
				Application.LoadLevel("mainScene");
				//dialog.hideDialog ();
			}
		}else{
			if(dialogPos == "startMessage"){

				isGoClicked = true;
				GameObject btnGo = GameObject.Find ("btnGo");
				GameObject btnCancel = GameObject.Find ("btnCancel");
				
				btnGo.SetActive(false);
				btnCancel.SetActive(false);

			}else if(dialogPos == "villageMessage"){
				villageChoice = ATTACK_VILLAGE;
				dialogPos = "";
			}else if(dialogPos == "castleMessage"){
				castleChoice = ATTACK_CASTLE;
				dialogPos = "";
			}
			dialog.hideDialog ();
		}
	}
	
	public void onClickButton2(){
		Debug.Log ("Dialog button 2");
		if(dialogPos == "startMessage"){
			// Do nothing
		}else if(dialogPos == "villageMessage"){
			villageChoice = PAY_VILLAGE;
			dialogPos = "";
		}else if(dialogPos == "castleMessage"){
			castleChoice = DUEL_CASTLE;
			dialogPos = "";
		}
		dialog.hideDialog();
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
		dialog.showDialogMessage("You arrived at the forest village", //title
		                         "People from the village are not allied of your reign and seem a bit hostile.\n" +
		                         "They don't have enough weaponry and a combat would favor your army.\n" +
		                         "You can either plunder the village or offer them gold for your stay.", // text
		                         "Attack", "Pay 1000g");
		dialogPos = "villageMessage";
		/*if (EditorUtility.DisplayDialog ("You arrived at the forest village", //title
		                                 "People from the village are not allied of your reign and seem a bit hostile.\n" +
		                                 "They don't have enough weaponry and a combat would favor your army.\n" +
		                                 "You can either plunder the village or offer them gold for your stay.", // text
		                                 "Attack", "Pay 1000g")) { // yes, no
			villageChoice = ATTACK_VILLAGE;
		}else{
			villageChoice = PAY_VILLAGE;
		}*/
	}
	/************************************************************************/

	/***************************** Castle Script ***************************/
	void castleEvent(){
		singleButton = false;
		dialog.showDialogMessage("You found the castle!", //title
		                             "The castle is very fortified. Attacking directly might cost you many fighters.\n"+
		                             "Another option is to challange the castellan, your best champion against his.",
		                         "Direct attack", "Duel");
		dialogPos = "castleMessage";
		/*if (EditorUtility.DisplayDialog ("You found the castle!", //title
		                                 "The castle is very fortified. Attacking directly might cost you many fighters.\n"+
		                                 "Another option is to challange the castellan, your best champion against his.",
		                                 "Direct attack", "Duel")) { // yes, no
			castleChoice = ATTACK_CASTLE;
		}else{
			castleChoice = DUEL_CASTLE;
		}*/
	}
	/************************************************************************/

	void OnMouseOver() {
		Debug.Log ("mouse over");		
	}
	void OnMouseExit() {
		Debug.Log ("mose exit");
	}
}
#endif 