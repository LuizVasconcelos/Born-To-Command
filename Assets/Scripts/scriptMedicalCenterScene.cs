using UnityEngine;
using System.Collections;

public class scriptMedicalCenterScene : MonoBehaviour {

	private float current;
	private float target;
	private float currentVelocity;
	private float smoothTime;

	private bool isBtnKnightsClicked;
	private bool isBtnLancersClicked;
	private bool isBtnArchersClicked;

	private GameObject knights;
	private GameObject lancers;
	private GameObject archers;

	private bool isBtnCuraClicked;

	private GameObject information;
	private GameObject unitysSelectedTypeStatus;
	private GameObject btnCurar;

	private Player localPlayer;

	// Use this for initialization
	void Start () 
	{
		target = 1.1f;
		currentVelocity = 0.0f;
		smoothTime = 1.0f;

		isBtnKnightsClicked = false;
		isBtnLancersClicked = false;
		isBtnArchersClicked = false;

		knights = GameObject.Find ("btnKnight");
		lancers = GameObject.Find ("btnLancer");
		archers = GameObject.Find ("btnArcher");

		isBtnCuraClicked = false;

		information = GameObject.Find ("SelectedSoldiersTypeStatus");
		unitysSelectedTypeStatus = GameObject.Find ("soldiersStatus");
		btnCurar = GameObject.Find ("btnCurar");

		information.SetActive (false);
		unitysSelectedTypeStatus.SetActive (false);
		btnCurar.SetActive (false);

		localPlayer = GameManager.player;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Camera.main.camera.orthographicSize <= (target - 0.01f)) {
			
			// current camera depth
			current = Camera.main.camera.orthographicSize;
			
			// zoom phase
			Camera.main.camera.orthographicSize = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);
			
		}else if(isBtnKnightsClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedSoldiersTypeStatus(1);

			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha

				information.SetActive (true);
				unitysSelectedTypeStatus.SetActive (true);
				btnCurar.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}

			isBtnKnightsClicked = false;
		}else if(isBtnLancersClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedSoldiersTypeStatus(2);
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				unitysSelectedTypeStatus.SetActive (true);
				btnCurar.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}
			
			isBtnLancersClicked = false;
		}else if(isBtnArchersClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedSoldiersTypeStatus(3);
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				unitysSelectedTypeStatus.SetActive (true);
				btnCurar.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}
			
			isBtnArchersClicked = false;
		}else if(isBtnCuraClicked) {
			//atualizar informaçoes das tropas curadas

			information.SetActive(false);
			unitysSelectedTypeStatus.SetActive(false);
			btnCurar.SetActive(false);

			isBtnCuraClicked = false;
		}
	}




	void updateSelectedSoldiersTypeStatus(int type) {
		UILabel content = unitysSelectedTypeStatus.GetComponent<UILabel> ();

		/*content.text = selected.GetTypeWeapon() + "\n\n" +
			"Price: " + selected.GetPriceOfPurchase() + "\n" +
				"Payload: " + selected.GetPriceOfSelling() + "\n";*/
	}


	void onKnightsClicked()
	{
		isBtnKnightsClicked = true;
	}
	
	void onLancersClicked()
	{
		isBtnLancersClicked = true;
	}
	
	void onArchersClicked()
	{
		isBtnArchersClicked = true;
	}
	
	void onBtnCurarClicked()
	{
		isBtnCuraClicked = true;
	}
}
