using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if UNITY_EDITOR
public class scriptMainScene : MonoBehaviour {

	private bool isBlacksmithClicked;
	private bool isMedicalCenterClicked;
	private bool isTrainingCenterClicked;
	private bool isCarpenterClicked;
	private bool isMissionsClicked;
	private bool isSaveGameClicked;

	private AudioSource auSong;

	// mission 1 objs
	private GameObject btnTarget1;
	private GameObject btnTarget2;
	private GameObject btnTarget3_1;
	private GameObject btnTarget3_2;
	private GameObject panel;
	private GameObject music;

	private GameObject btnMissions;
	private Vector3 currentRotation;
	private GameObject scroll;
	private float currentMiddleScroll;
	private bool openScroll;

	private GameObject map;

	private bool showMessage;

	private Player localPlayer;

	// Use this for initialization
	void Start () {
		isBlacksmithClicked = false;
		isMedicalCenterClicked = false;
		isTrainingCenterClicked = false;
		isCarpenterClicked = false;
		isMissionsClicked = false;
		isSaveGameClicked = false;

		showMessage = false;

		btnMissions = GameObject.Find ("btnMissions");
		currentRotation = new Vector3(btnMissions.transform.rotation.x,btnMissions.transform.rotation.y,btnMissions.transform.rotation.z);

		map = GameObject.Find ("mapZone");

		scroll = GameObject.Find("scroll");
		if (scroll != null) {
			currentMiddleScroll = scroll.transform.localScale.y;
		}
		openScroll = false;

		btnTarget1 = GameObject.Find ("btnTarget1");
		btnTarget2 = GameObject.Find ("btnTarget2");
		btnTarget3_1 = GameObject.Find ("btnTarget3-1");
		btnTarget3_2 = GameObject.Find ("btnTarget3-2");

		panel = GameObject.Find ("Panel");
		music = GameObject.Find ("Camera");
		auSong = (AudioSource)music.AddComponent ("AudioSource");
		AudioClip mainSong;
		mainSong = (AudioClip)Resources.Load ("Main Song");
		auSong.clip = mainSong;
		auSong.loop = true;

		auSong.Play ();

		if (scroll != null) {
			scroll.SetActive (false);
		}
		
		if (GameManager.player == null) {
			int[] game = new int[]{Player.ENABLED, Player.DISABLED,Player.DISABLED,Player.DISABLED};
			localPlayer = new Player (1570, 6, Player.generateTroop (50, 50, 50), 0, 0, game);
			GameManager.player = localPlayer;
		} else {
			localPlayer = GameManager.player;
		}

		for (int i = 0; i<4; i++) {
			GameObject go = null;
			switch(i){
				case 0:
					go = btnTarget1;
					break;
				case 1:
					go = btnTarget2;
					break;
				case 2:
					go = btnTarget3_1;
					break;
				case 3:
					go = btnTarget3_2;
					break;
			}

			try{
				if (localPlayer.Game [i] == Player.DISABLED) {
					go.SetActive(false);
					//go.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
				}else if(localPlayer.Game [i] == Player.WON){
					go.transform.FindChild("Label").GetComponent<UILabel>().color = Color.green;
					go.transform.FindChild("Label").GetComponent<UILabel>().text = "O";
				}else if(localPlayer.Game [i] == Player.LOST){
					go.transform.FindChild("Label").GetComponent<UILabel>().color = Color.black;
				}
			}catch(System.NullReferenceException e){}
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateLog ();
		updateStatus ();

		if (isBlacksmithClicked) {
						// current camera depth
						float current = Camera.main.camera.orthographicSize;
						// hardcoded target depth
						float target = 0.05f;
						// 0.0 for default
						float currentVelocity = 0.0f;
						// 0.15 for default
						float smoothTime = 0.15f;
			
						// zoom phase
						Camera.main.camera.orthographicSize = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);

						// open phase
						if (Camera.main.camera.orthographicSize <= (target + 0.01f)) {
								Application.LoadLevel ("blacksmithScene");
						}
				} else if (isMedicalCenterClicked) {
						// current camera depth
						float current = Camera.main.camera.orthographicSize;
						// hardcoded target depth
						float target = 0.05f;
						// 0.0 for default
						float currentVelocity = 0.0f;
						// 0.15 for default
						float smoothTime = 0.15f;
			
						// zoom phase
						Camera.main.camera.orthographicSize = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);
			
						// open phase
						if (Camera.main.camera.orthographicSize <= (target + 0.01f)) {
								Application.LoadLevel ("medicalCenterScene");
						}
				} else if (isTrainingCenterClicked) {
						// current camera depth
						float current = Camera.main.camera.orthographicSize;
						// hardcoded target depth
						float target = 0.05f;
						// 0.0 for default
						float currentVelocity = 0.0f;
						// 0.15 for default
						float smoothTime = 0.15f;
			
						// zoom phase
						Camera.main.camera.orthographicSize = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);
			
						// open phase
						if (Camera.main.camera.orthographicSize <= (target + 0.01f)) {
								Application.LoadLevel ("trainingCenterScene");
						}
				} else if (isCarpenterClicked) {
						// current camera depth
						float current = Camera.main.camera.orthographicSize;
						// hardcoded target depth
						float target = 0.05f;
						// 0.0 for default
						float currentVelocity = 0.0f;
						// 0.15 for default
						float smoothTime = 0.15f;
			
						// zoom phase
						Camera.main.camera.orthographicSize = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);
			
						// open phase
						if (Camera.main.camera.orthographicSize <= (target + 0.01f)) {
								Application.LoadLevel ("carpenterScene");
						}
				} else if (isMissionsClicked) {

						GameObject background = btnMissions.transform.FindChild("Background").gameObject;
						if(background.activeSelf) background.SetActive(false);

						if (btnMissions.transform.rotation.eulerAngles.z <= 89) {
								btnMissions.transform.Rotate (new Vector3 (currentRotation.x, currentRotation.y, 90) * Time.deltaTime);
						} else {
							if(!openScroll) {
								Vector3 currentPosition = btnMissions.transform.position;
								Vector3 currentScale = btnMissions.transform.localScale;
								Vector3 target = new Vector3 (map.transform.position.x, map.transform.position.y, currentPosition.z);
								Vector3 velocity = new Vector3 (0.0f, 0.0f, 0.0f);
								float currentVelocity = 0.0f;
								float smoothTime = 0.15f;


								btnMissions.transform.position = Vector3.SmoothDamp (currentPosition, target, ref velocity, smoothTime);

								//Debug.Log ("x: " + btnMissions.transform.position.x);
								//Debug.Log ("target x: " + map.transform.position.x);

								//Debug.Log ("y: " + btnMissions.transform.position.y);
								//Debug.Log ("target y: " + map.transform.position.y);

								if ((btnMissions.transform.position.x <= (target.x + 0.05f)) && 
										(btnMissions.transform.position.y >= (target.y - 0.05f))) {

										target = new Vector3 (4.75f, 7.5f, currentScale.z);

										//Debug.Log("target scale x: " + target.x);
										//Debug.Log("scaled x: " + btnMissions.transform.localScale.x);




										btnMissions.transform.localScale = Vector3.SmoothDamp (currentScale, target, ref velocity, smoothTime);

										if ((btnMissions.transform.localScale.x >= (target.x - 0.05f)) &&
						    			  (btnMissions.transform.localScale.y >= (target.x - 0.05f))) {
											btnMissions.SetActive(false);
											scroll.SetActive(true);
											openScroll = true;
										}
								}
							}else {
								GameObject leftSide = scroll.transform.FindChild("leftSide").gameObject;
								GameObject middle = scroll.transform.FindChild("middle").gameObject;
								GameObject rightSide = scroll.transform.FindChild("rightSide").gameObject;

								Vector3 currentPosition2 = leftSide.transform.position;
								Vector3 target2 = new Vector3 ((-1.4f), leftSide.transform.position.y, leftSide.transform.position.z);
								Vector3 velocity2 = new Vector3 (0.0f, 0.0f, 0.0f);
								float smoothTime2 = 0.15f;

								Vector3 currentPosition3 = rightSide.transform.position;
								Vector3 target3 = new Vector3 (1.825f, rightSide.transform.position.y, rightSide.transform.position.z);
								Vector3 velocity3 = new Vector3 (0.0f, 0.0f, 0.0f);
								float smoothTime3 = 0.15f;

								//Vector3 newScrollContentPosition = new Vector3((currentPosition2.x + rightSide.transform.position.x)/2, middle.transform.position.y, middle.transform.position.z);
								Vector3 newScrollContentScale = new Vector3(middle.transform.localScale.x, 2.0f, middle.transform.localScale.z);
								Vector3 currentScrollContentScale = new Vector3(middle.transform.localScale.x,middle.transform.localScale.y,middle.transform.localScale.z);
								Vector3 velocity4 = new Vector3 (0.0f, 0.0f, 0.0f);
								float smoothTime4 = 0.15f;

								if((leftSide.transform.position.x >= (-1.39f))
					   				&& (rightSide.transform.position.x <= 1.815f)) {
									leftSide.transform.position = Vector3.SmoothDamp(currentPosition2, target2, ref velocity2, smoothTime2);
									rightSide.transform.position = Vector3.SmoothDamp(currentPosition3, target3, ref velocity3, smoothTime3);

									middle.transform.localScale = Vector3.SmoothDamp(currentScrollContentScale, newScrollContentScale, ref velocity4, smoothTime4);
						
								}else {
									isMissionsClicked = false;								
								}
							}
						}
				} else if (isSaveGameClicked) {
						isSaveGameClicked = false;
						Debug.Log ("Save Game");
						Application.LoadLevel("SaveGameMenuScene");
		}
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

	void OnGUI(){
		if(showMessage){
			GUI.Label(new Rect(0, 0, 100, 100), "Some Random Text");
		}
	}

	void onBlacksmithClicked()
	{
		isBlacksmithClicked = true;
	}

	void onMedicalCenterClicked()
	{
		isMedicalCenterClicked = true;
	}

	void onTrainingCenterClicked()
	{
		isTrainingCenterClicked = true;
	}
	
	void onCarpenterClicked()
	{
		isCarpenterClicked = true;
	}

	void onMissionsClicked()
	{
		isMissionsClicked = true;
	}

	void OnSaveGameClicked()
	{
		isSaveGameClicked = true;
	}

	void OnTarget1Clicked()
	{
		if (localPlayer.Game [0] == Player.WON) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already won this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else if (localPlayer.Game [0] == Player.LOST) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already lost this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else {
			if (EditorUtility.DisplayDialog ("Start Mission 1", //title
			                                 "Are you sure you want to start the Mission 1?", // text
			                                 "OK", "Cancel")) { // yes, no
				Application.LoadLevel ("mission1Scene");
			}
		}
	}

	void OnTarget2Clicked()
	{
		if (localPlayer.Game [1] == Player.WON) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already won this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else if (localPlayer.Game [1] == Player.LOST) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already lost this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else {
			if (EditorUtility.DisplayDialog ("Start Mission 2", //title
			                                 "Are you sure you want to start the Mission 1?", // text
			                                 "OK", "Cancel")) { // yes, no
				Application.LoadLevel ("mission2Scene");
			}
		}
	}

	void OnTarget3_1Clicked()
	{
		if (localPlayer.Game [2] == Player.WON) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already won this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else if (localPlayer.Game [2] == Player.LOST) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already lost this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else {
			if (EditorUtility.DisplayDialog ("Start Mission 1", //title
			                                 "Are you sure you want to start the Mission 1?", // text
			                                 "OK", "Cancel")) { // yes, no
				Application.LoadLevel ("mission3_1Scene");
			}
		}
	}

	void OnTarget3_2Clicked()
	{
		if (localPlayer.Game [3] == Player.WON) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already won this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else if (localPlayer.Game [3] == Player.LOST) {
			if (EditorUtility.DisplayDialog ("Mission not playable", //title
			                                 "You already lost this mission!", // text
			                                 "OK")) { // yes, no
				//isTarget1Clicked = true;
			}
		} else {
			if (EditorUtility.DisplayDialog ("Start Mission 1", //title
			                                 "Are you sure you want to start the Mission 1?", // text
			                                 "OK", "Cancel")) { // yes, no
				Application.LoadLevel ("mission3_2Scene");
			}
		}
	}

	void awake(){
		DontDestroyOnLoad (music);
	}
}
#endif