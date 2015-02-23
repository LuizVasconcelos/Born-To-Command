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

	// mission 1 objs
	private bool isTarget1Clicked;
	private GameObject btnTarget1;
	private GameObject panel;

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

		panel = GameObject.Find ("Panel");

		if (scroll != null) {
			scroll.SetActive (false);
		}

		int[] game = new int[]{Player.ENABLED, Player.DISABLED,Player.DISABLED,Player.DISABLED};
		if(GameManager.player == null){
			localPlayer = new Player (1570, 6, Player.generateTroop(50,50,50), 0, 0,game);
			GameManager.player = localPlayer;
		}
		localPlayer = GameManager.player;
	}
	
	// Update is called once per frame
	void Update () {
		if (localPlayer.Game [0] == 1) {

		}

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
		} else if (isTarget1Clicked){
			Application.LoadLevel ("mission1Scene");
		}
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
		if (EditorUtility.DisplayDialog ("Start Mission 1", //title
		                            "Are you sure you want to start the Mission 1?", // text
		                            "OK", "Cancel")) { // yes, no
						isTarget1Clicked = true;
				}
	}
}
#endif