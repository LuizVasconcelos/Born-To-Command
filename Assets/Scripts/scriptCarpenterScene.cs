using UnityEngine;
using System.Collections;

public class scriptCarpenterScene : MonoBehaviour {

	private float current;
	private float target;
	private float currentVelocity;
	private float smoothTime;

	private bool isBtnShipOneClicked;
	private bool isBtnShipTwoClicked;
	private bool isBtnShipThreeClicked;
	
	private GameObject shipOne;
	private GameObject shipTwo;
	private GameObject shipThree;
	
	private bool isBtnUpgradeClicked;
	private bool isBtnConstruirClicked;
	
	private GameObject information;
	private GameObject shipInformation;
	private GameObject btnUpgrade;
	private GameObject btnConstruir;
	
	private Player localPlayer;

	// Use this for initialization
	void Start () 
	{
		target = 1.1f;
		currentVelocity = 0.0f;
		smoothTime = 1.0f;

		isBtnShipOneClicked = false;
		isBtnShipTwoClicked = false;
		isBtnShipThreeClicked = false;
		
		shipOne = GameObject.Find ("btnShip1");
		shipTwo = GameObject.Find ("btnShip2");
		shipThree = GameObject.Find ("btnShip3");
		
		isBtnUpgradeClicked = false;
		isBtnConstruirClicked = false;
		
		information = GameObject.Find ("SelectedShipInformation");
		shipInformation = GameObject.Find ("shipInformation");
		btnUpgrade = GameObject.Find ("btnUpgrade");
		btnConstruir = GameObject.Find ("btnConstruir");
		
		information.SetActive (false);
		shipInformation.SetActive (false);
		btnUpgrade.SetActive (false);
		btnConstruir.SetActive (false);

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
			
		}else if(isBtnShipOneClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedShipInformation(1);
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				shipInformation.SetActive (true);
				btnUpgrade.SetActive (true);
				btnConstruir.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}
			
			isBtnShipOneClicked = false;
		}else if(isBtnShipTwoClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedShipInformation(2);
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				shipInformation.SetActive (true);
				btnUpgrade.SetActive (true);
				btnConstruir.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}
			
			isBtnShipTwoClicked = false;
		}else if(isBtnShipThreeClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedShipInformation(3);
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				shipInformation.SetActive (true);
				btnUpgrade.SetActive (true);
				btnConstruir.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}
			
			isBtnShipThreeClicked = false;
		}else if(isBtnUpgradeClicked) {
			//realizar upgrade do barco selecionado
			
			information.SetActive(false);
			shipInformation.SetActive(false);
			btnUpgrade.SetActive(false);
			btnConstruir.SetActive(false);
			
			isBtnUpgradeClicked = false;
		}else if(isBtnConstruirClicked) {
			//realizar compra do barco selecionado
			
			information.SetActive(false);
			shipInformation.SetActive(false);
			btnUpgrade.SetActive(false);
			btnConstruir.SetActive(false);
			
			isBtnConstruirClicked = false;
		}
	}


	void updateSelectedShipInformation(int type) {
		UILabel content = shipInformation.GetComponent<UILabel> ();
		
		/*content.text = selected.GetTypeWeapon() + "\n\n" +
			"Price: " + selected.GetPriceOfPurchase() + "\n" +
				"Payload: " + selected.GetPriceOfSelling() + "\n";*/
	}


	void onShipOneClicked()
	{
		isBtnShipOneClicked = true;
	}
	
	void onShipTwoClicked()
	{
		isBtnShipTwoClicked = true;
	}
	
	void onShipThreeClicked()
	{
		isBtnShipThreeClicked = true;
	}
	
	void onBtnUpgradeClicked()
	{
		isBtnUpgradeClicked = true;
	}

	void onBtnConstruirClicked()
	{
		isBtnConstruirClicked = true;
	}
}
