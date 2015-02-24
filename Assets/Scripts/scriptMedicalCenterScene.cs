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

	private int soldierType;
	private int feridos;
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

		isBtnCuraClicked = false;

		information = GameObject.Find ("SelectedSoldiersTypeStatus");
		unitysSelectedTypeStatus = GameObject.Find ("soldiersStatus");
		btnCurar = GameObject.Find ("btnCurar");

		information.SetActive (false);
		unitysSelectedTypeStatus.SetActive (false);
		btnCurar.SetActive (false);

		localPlayer = GameManager.player;

		soldierType = 0;
		feridos = 0;
		custo = 0;
		
		isBtnVoltarClicked = false;
		voltar = GameObject.Find ("btnRetornar");
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
			updateSelectedSoldiersTypeStatus("Swordman",GetSelectedUnitType(1));

			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha

				information.SetActive (true);
				unitysSelectedTypeStatus.SetActive (true);
				//btnCurar.SetActive (true);
			}

			soldierType = 1;
			isBtnKnightsClicked = false;
		}else if(isBtnLancersClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedSoldiersTypeStatus("Knight",GetSelectedUnitType(2));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				unitysSelectedTypeStatus.SetActive (true);
				//btnCurar.SetActive (true);
			}

			soldierType = 2;
			isBtnLancersClicked = false;
		}else if(isBtnArchersClicked) {
			//passar os status do tipo de soldado selecionado
			updateSelectedSoldiersTypeStatus("Archer",GetSelectedUnitType(3));
			
			if(!information.activeInHierarchy) {
				//inserir as informaçoes na telinha
				
				information.SetActive (true);
				unitysSelectedTypeStatus.SetActive (true);
				//btnCurar.SetActive (true);
			}

			soldierType = 3;
			isBtnArchersClicked = false;
		}else if(isBtnCuraClicked) {
			//atualizar informaçoes das tropas curadas

			if((feridos > 0) && (localPlayer.Gold >= custo)) {
				foreach(Unit u in localPlayer.Units.Units) {
					if(soldierType == 1) {
						if(u.Type.Equals("Swordman") && (u.Health < 100)) {
							u.Health = 100;
						}
					}else if(soldierType == 2) {
						if(u.Type.Equals("Knight") && (u.Health < 100)) {
							u.Health = 100;
						}
					}else if(soldierType == 3) {
						if(u.Type.Equals("Archer") && (u.Health < 100)) {
							u.Health = 100;
						}
					}
				}

				localPlayer.Gold -= custo;

				information.SetActive(false);
				unitysSelectedTypeStatus.SetActive(false);
				btnCurar.SetActive(false);
			}

			isBtnCuraClicked = false;
		}else if(isBtnVoltarClicked) {
			isBtnVoltarClicked = false;

			GameManager.player = localPlayer;
			Application.LoadLevel("mainScene");
		}
	}


	int GetSelectedUnitType(int type) {

		feridos = 0;

		if (type == 1) {
			foreach(Unit u in localPlayer.Units.Units) {
				if(u.Type.Equals("Swordman") && (u.Health < 100)) {
					feridos++;
				}
			}
		}else if (type == 2) {
			foreach(Unit u in localPlayer.Units.Units) {
				if(u.Type.Equals("Knight") && (u.Health < 100)) {
					feridos++;
				}
			}
		}else if (type == 3) {
			foreach(Unit u in localPlayer.Units.Units) {
				if(u.Type.Equals("Archer") && (u.Health < 100)) {
					feridos++;
				}
			}
		}

		return feridos;
	}

	void updateSelectedSoldiersTypeStatus(string type, int feridos) {
		UILabel content = unitysSelectedTypeStatus.GetComponent<UILabel> ();

		custo = 0;

		if(type.Equals("Swordman")) custo = feridos*5;
		if(type.Equals("Knight")) custo = feridos*10;
		else custo = feridos*3;

		if (feridos == 0) {
			content.text = type + "\n\n" +
				"There aren't \nwounded soldiers!\n";

			btnCurar.SetActive(false);
		}else if (feridos == 1) {
			content.text = type + "\n\n" +
				"Wounded quantity: " + feridos + " soldier\n" +
					"Price: " + custo + " gold\n";

			btnCurar.SetActive(true);
		} else {
			content.text = type + "\n\n" +
				"Wounded quantity: " + feridos + " soldiers\n" +
					"Price: " + custo + " gold\n";

			btnCurar.SetActive(true);
		}
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

	void onBtnVoltarClicked()
	{
		isBtnVoltarClicked = true;
	}
}
