using UnityEngine;
using System.Collections.Generic;

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

	private bool isBtnConstruirClicked;
	
	private GameObject information;
	private GameObject shipInformation;
	private GameObject btnConstruir;
	
	private Player localPlayer;

	private List<Ship> Ships; 

	private Ship shipToBuy;

	private bool isBtnVoltarClicked;
	private GameObject btnVoltar;

	private int custo;

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

		isBtnConstruirClicked = false;
		
		information = GameObject.Find ("SelectedShipInformation");
		shipInformation = GameObject.Find ("shipInformation");
		btnConstruir = GameObject.Find ("btnConstruir");
		
		information.SetActive (false);
		shipInformation.SetActive (false);
		btnConstruir.SetActive (false);

		localPlayer = GameManager.player;

		inicializeShipsList ();

		shipToBuy = null;

		isBtnVoltarClicked = false;
		btnVoltar = GameObject.Find ("btnReturn");

		custo = 0;
	}


	void inicializeShipsList() {
		Ships = new List<Ship> ();

		Ships.Add(new Ship(10));
		Ships.Add(new Ship(50));
		Ships.Add(new Ship(100));
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
			updateSelectedShipInformation(GetSelectedShip(1), shipsTypeQuantity(1));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				shipInformation.SetActive (true);
				btnConstruir.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}

			isBtnShipOneClicked = false;
		}else if(isBtnShipTwoClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedShipInformation(GetSelectedShip(2), shipsTypeQuantity(2));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				shipInformation.SetActive (true);
				btnConstruir.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}
			
			isBtnShipTwoClicked = false;
		}else if(isBtnShipThreeClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedShipInformation(GetSelectedShip(3), shipsTypeQuantity(3));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				shipInformation.SetActive (true);
				btnConstruir.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}
			
			isBtnShipThreeClicked = false;
		}else if(isBtnConstruirClicked) {
			//realizar compra do barco selecionado
			if(localPlayer.Gold >= custo) {
				localPlayer.Units.Ships.Add(shipToBuy);

				localPlayer.Gold -= custo;

				information.SetActive(false);
				shipInformation.SetActive(false);
				btnConstruir.SetActive(false);
			}
			
			isBtnConstruirClicked = false;
		}else if(isBtnVoltarClicked) {
			isBtnVoltarClicked = false;

			GameManager.player = localPlayer;
			Application.LoadLevel("mainScene");
		}
	}


	Ship GetSelectedShip(int position) {
		return Ships[position-1];
	}

	int shipsTypeQuantity(int type) {
		int qtt = 0;

		if (type == 1) {
			foreach(Ship s in localPlayer.Units.Ships) {
				if(s.Capacity == 10) {
					qtt++;
				}
			}

			custo = 75;
		}else if (type == 2) {
			foreach(Ship s in localPlayer.Units.Ships) {
				if(s.Capacity == 50) {
					qtt++;
				}
			}

			custo = 125;
		}else if (type == 3) {
			foreach(Ship s in localPlayer.Units.Ships) {
				if(s.Capacity == 100) {
					qtt++;
				}
			}

			custo = 225;
		}

		return qtt;
	}


	void updateSelectedShipInformation(Ship selected, int sameShipTypeQuantity) {
		UILabel content = shipInformation.GetComponent<UILabel> ();

		if (sameShipTypeQuantity == 1) {
			content.text = "Owned: " + sameShipTypeQuantity + " ship\n" +
				"Capacity: " + selected.Capacity + " soldiers\n" + 
					"Price: " + custo + " gold\n";
		}else {
			content.text = "Owned: " + sameShipTypeQuantity + " ships\n" +
				"Capacity: " + selected.Capacity + " soldiers\n" +
					"Price: " + custo + " gold\n";
		}

		shipToBuy = selected;
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

	void onBtnConstruirClicked()
	{
		isBtnConstruirClicked = true;
	}

	void onBtnVoltarClicked()
	{
		isBtnVoltarClicked = true;
	}
}
