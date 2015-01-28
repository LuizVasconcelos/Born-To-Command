using UnityEngine;
using System.Collections;

public class scriptMainScene : MonoBehaviour {

	private bool isBlacksmithClicked;
	private bool isMedicalCenterClicked;
	private bool isTrainingCenterClicked;
	private bool isCarpenterClicked;
	private bool isSaveGameClicked;

	private bool showMessage;

	// Use this for initialization
	void Start () {
		isBlacksmithClicked = false;
		isMedicalCenterClicked = false;
		isTrainingCenterClicked = false;
		isCarpenterClicked = false;
		isSaveGameClicked = false;

		showMessage = false;
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

	void OnSaveGameClicked()
	{
		isSaveGameClicked = true;
	}
}
