using UnityEngine;
using System.Collections;

public class scriptTrainingCenterScene : MonoBehaviour {

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
	
	private bool isBtnTreinarOneClicked;
	private bool isBtnTreinarTwoClicked;
	private bool isBtnTreinarThreeClicked;
	
	private GameObject information;
	private GameObject soldiersType;
	private GameObject unitysSelectedTypeQuantity1;
	private GameObject btnTreinar1;
	private GameObject unitysSelectedTypeQuantity2;
	private GameObject btnTreinar2;
	private GameObject unitysSelectedTypeQuantity3;
	private GameObject btnTreinar3;
	
	private Player localPlayer;

	private int soldierType;
	private int custo;

	private bool isBtnVoltarClicked;
	private GameObject voltar;

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
		
		isBtnTreinarOneClicked = false;
		isBtnTreinarTwoClicked = false;
		isBtnTreinarThreeClicked = false;
		
		information = GameObject.Find ("SelectedSoldiersTypeInformation");
		soldiersType = GameObject.Find ("soldiersType");
		unitysSelectedTypeQuantity1 = GameObject.Find ("soldiersQuantity1");
		btnTreinar1 = GameObject.Find ("btnTreinar1");
		unitysSelectedTypeQuantity2 = GameObject.Find ("soldiersQuantity2");
		btnTreinar2 = GameObject.Find ("btnTreinar2");
		unitysSelectedTypeQuantity3 = GameObject.Find ("soldiersQuantity3");
		btnTreinar3 = GameObject.Find ("btnTreinar3");
		
		information.SetActive (false);
		soldiersType.SetActive (false);
		unitysSelectedTypeQuantity1.SetActive (false);
		btnTreinar1.SetActive (false);
		unitysSelectedTypeQuantity2.SetActive (false);
		btnTreinar2.SetActive (false);
		unitysSelectedTypeQuantity3.SetActive (false);
		btnTreinar3.SetActive (false);

		localPlayer = GameManager.player;

		soldierType = 0;
		custo = 0;

		isBtnVoltarClicked = false;
		voltar = GameObject.Find ("btnReturn");
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
			updateSelectedSoldiersTypeStatus(GetSelectedSoldierType(1));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				soldiersType.SetActive (true);
				unitysSelectedTypeQuantity1.SetActive (true);
				btnTreinar1.SetActive (true);
				unitysSelectedTypeQuantity2.SetActive (true);
				btnTreinar2.SetActive (true);
				unitysSelectedTypeQuantity3.SetActive (true);
				btnTreinar3.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}

			soldierType = 1;
			isBtnKnightsClicked = false;
		}else if(isBtnLancersClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedSoldiersTypeStatus(GetSelectedSoldierType(2));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				soldiersType.SetActive (true);
				unitysSelectedTypeQuantity1.SetActive (true);
				btnTreinar1.SetActive (true);
				unitysSelectedTypeQuantity2.SetActive (true);
				btnTreinar2.SetActive (true);
				unitysSelectedTypeQuantity3.SetActive (true);
				btnTreinar3.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}

			soldierType = 2;
			isBtnLancersClicked = false;
		}else if(isBtnArchersClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedSoldiersTypeStatus(GetSelectedSoldierType(3));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				soldiersType.SetActive (true);
				unitysSelectedTypeQuantity1.SetActive (true);
				btnTreinar1.SetActive (true);
				unitysSelectedTypeQuantity2.SetActive (true);
				btnTreinar2.SetActive (true);
				unitysSelectedTypeQuantity3.SetActive (true);
				btnTreinar3.SetActive (true);
			}else {
				//atualizar as informaçoes na telinha
			}

			soldierType = 3;
			isBtnArchersClicked = false;
		}else if(isBtnTreinarOneClicked) {
			//atualizar a quantidade de tropas
			if(localPlayer.Gold >= custo) {
				if(soldierType == 1) {
					localPlayer.Units.Units.Add(Unit.newSwordman());
				}else if(soldierType == 2) {
					localPlayer.Units.Units.Add(Unit.newKnight());
				}else if(soldierType == 3) {
					localPlayer.Units.Units.Add(Unit.newArcher());
				}

				localPlayer.Gold = localPlayer.Gold - custo;

				information.SetActive(false);
				soldiersType.SetActive (false);
				unitysSelectedTypeQuantity1.SetActive (false);
				btnTreinar1.SetActive (false);
				unitysSelectedTypeQuantity2.SetActive (false);
				btnTreinar2.SetActive (false);
				unitysSelectedTypeQuantity3.SetActive (false);
				btnTreinar3.SetActive (false);
			}
			
			isBtnTreinarOneClicked = false;
		}else if(isBtnTreinarTwoClicked) {
			//atualizar a quantidade de tropas
			
			if(localPlayer.Gold >= (custo*10)) {
				for(int i = 0; i < 10; i++) {
					if(soldierType == 1) {
						localPlayer.Units.Units.Add(Unit.newSwordman());
					}else if(soldierType == 2) {
						localPlayer.Units.Units.Add(Unit.newKnight());
					}else if(soldierType == 3) {
						localPlayer.Units.Units.Add(Unit.newArcher());
					}
				}
				
				localPlayer.Gold = localPlayer.Gold - (custo*10);
				
				information.SetActive(false);
				soldiersType.SetActive (false);
				unitysSelectedTypeQuantity1.SetActive (false);
				btnTreinar1.SetActive (false);
				unitysSelectedTypeQuantity2.SetActive (false);
				btnTreinar2.SetActive (false);
				unitysSelectedTypeQuantity3.SetActive (false);
				btnTreinar3.SetActive (false);
			}
			
			isBtnTreinarTwoClicked = false;
		}else if(isBtnTreinarThreeClicked) {
			//atualizar a quantidade de tropas
			
			if(localPlayer.Gold >= (custo*25)) {
				for(int i = 0; i < 25; i++) {
					if(soldierType == 1) {
						localPlayer.Units.Units.Add(Unit.newSwordman());
					}else if(soldierType == 2) {
						localPlayer.Units.Units.Add(Unit.newKnight());
					}else if(soldierType == 3) {
						localPlayer.Units.Units.Add(Unit.newArcher());
					}
				}
				
				localPlayer.Gold -= (custo*25);
				
				information.SetActive(false);
				soldiersType.SetActive (false);
				unitysSelectedTypeQuantity1.SetActive (false);
				btnTreinar1.SetActive (false);
				unitysSelectedTypeQuantity2.SetActive (false);
				btnTreinar2.SetActive (false);
				unitysSelectedTypeQuantity3.SetActive (false);
				btnTreinar3.SetActive (false);
			}
			
			isBtnTreinarThreeClicked = false;
		}else if(isBtnVoltarClicked) {
			isBtnVoltarClicked = false;

			GameManager.player = localPlayer;
			Application.LoadLevel ("mainScene");

		}
	}


	string GetSelectedSoldierType(int type) {
		if (type == 1) {
			return "Swordmans";
		}else if (type == 2) {
			return "Knights";
		}else {
			return "Archers";
		}
	}

	void updateSelectedSoldiersTypeStatus(string selected) {
		UILabel content = soldiersType.GetComponent<UILabel> ();				
		content.text = selected;

		custo = 0;
		
		if (selected.Equals("Swordmans")) {
			custo = 25;
		}if (selected.Equals("Knights")) {
			custo = 50;
		}else if (selected.Equals("Archers")) {
			custo = 15;
		}

		UILabel contentOne = unitysSelectedTypeQuantity1.GetComponent<UILabel> ();				

		contentOne.text = /*selected + "\n\n" +*/
			"Quantity: 1 soldier\n" +
				"Price: " + custo + " gold\n";

		UILabel contentTwo = unitysSelectedTypeQuantity2.GetComponent<UILabel> ();
		
		contentTwo.text = /*selected + "\n\n" +*/
			"Quantity: 10 soldiers\n" +
				"Price: " + (custo*10) + " gold\n";

		UILabel contentThree = unitysSelectedTypeQuantity3.GetComponent<UILabel> ();
		
		contentThree.text = /*selected + "\n\n" +*/
			"Quantity: 25 soldiers\n" +
				"Price: " + (custo*25) + " gold\n";
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
	
	void onBtnTreinarOneClicked()
	{
		isBtnTreinarOneClicked = true;
	}

	void onBtnTreinarTwoClicked()
	{
		isBtnTreinarTwoClicked = true;
	}

	void onBtnTreinarThreeClicked()
	{
		isBtnTreinarThreeClicked = true;
	}

	void onBtnVoltarClicked()
	{
		isBtnVoltarClicked = true;
	}
}
