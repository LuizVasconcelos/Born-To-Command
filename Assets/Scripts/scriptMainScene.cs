using UnityEngine;
using System.Collections;

public class scriptMainScene : MonoBehaviour {

	private bool isBlacksmithClicked;
	private bool isMedicalCenterClicked;
	private bool isTrainingCenterClicked;
	private bool isCarpenterClicked;
	private bool isMissionsClicked;
	private bool isSaveGameClicked;

	private GameObject btnMissions;
	private Vector3 currentRotation;

	private bool showMessage;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (isBlacksmithClicked) 
		{
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
			if (Camera.main.camera.orthographicSize <= (target+0.01f)) 
			{
				Application.LoadLevel ("blacksmithScene");
			}
		} 
		else if (isMedicalCenterClicked) 
		{
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
			if (Camera.main.camera.orthographicSize <= (target+0.01f)) 
			{
				Application.LoadLevel ("medicalCenterScene");
			}
		}
		else if (isTrainingCenterClicked) 
		{
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
			if (Camera.main.camera.orthographicSize <= (target+0.01f)) 
			{
				Application.LoadLevel ("trainingCenterScene");
			}
		}
		else if (isCarpenterClicked) 
		{
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
			if (Camera.main.camera.orthographicSize <= (target+0.01f)) 
			{
				Application.LoadLevel ("carpenterScene");
			}
		}
		else if (isMissionsClicked) 
		{
			if(btnMissions.transform.rotation.eulerAngles.z <= 89) 
			{
				btnMissions.transform.Rotate(new Vector3(currentRotation.x,currentRotation.y,90)*Time.deltaTime);
			}
			else
			{
				Vector3 currentPosition = btnMissions.transform.position;
				Vector3 currentScale = btnMissions.transform.localScale;
				Vector3 target = new Vector3(1.5f, 0.0f, currentPosition.z);
				Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
				float currentVelocity = 0.0f;
				float smoothTime = 0.2f;

				btnMissions.transform.position = Vector3.SmoothDamp(currentPosition, target, ref velocity, smoothTime);

				Debug.Log("x: " + btnMissions.transform.position.x);

				if((btnMissions.transform.position.x >= 1.49f) && 
					(btnMissions.transform.position.y >= (-0.01f)))
				{

					target = new Vector3(3.0f, 0.9f, currentPosition.z);

					btnMissions.transform.localScale = Vector3.SmoothDamp(currentScale, target, ref velocity, smoothTime);

					if((btnMissions.transform.localScale.x >= 2.99f) &&
					   (btnMissions.transform.localScale.y >= 0.89f))
					{
						isMissionsClicked = false;
					}
				}
			}
		}
		else if (isSaveGameClicked)
		{
			isSaveGameClicked = false;
			Debug.Log ("Save Game");
			/*
			if(UnityEditor.EditorUtility.DisplayDialog("Game Over", "Again?", "Restart", "Exit")){
				Debug.Log ("Yes");
				isSaveGameClicked = false;
			}else{
				Debug.Log ("No");
				isSaveGameClicked = false;
			}
			*/
			// TODO Salvar as configuraçoes atuais do jogo
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
}
