using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scriptBlacksmithScene : MonoBehaviour {

	private float current;
	private float target;
	private float currentVelocity;
	private float smoothTime;

	private bool isSwordsClicked;
	private bool isArmorsClicked;
	private bool isShieldsClicked;
	private bool isBowsClicked;

	private int EQUIPMENT_ID;
	private int SELECTED_ITEM_ID;

	private bool isItemOneClicked;
	private bool isItemTwoClicked;
	private bool isItemThreeClicked;

	private GameObject itemOne;
	private GameObject itemTwo;
	private GameObject itemThree;

	private bool isBtnCompraClicked;

	private GameObject information;
	private GameObject itemInfo;
	private GameObject btnComprar;

	private List<Equipment> Equipments;

	private object itemToBuy;

	private Player localPlayer;

	private bool isBtnVoltarClicked;
	private GameObject btnVoltar;

	// Use this for initialization
	void Start () 
	{
		target = 1.1f;
		currentVelocity = 0.0f;
		smoothTime = 1.0f;

		isSwordsClicked = false;
		isArmorsClicked = false;
		isShieldsClicked = false;
		isBowsClicked = false;

		EQUIPMENT_ID = 0;

		isItemOneClicked = false;
		isItemTwoClicked = false;
		isItemThreeClicked = false;

		itemOne = GameObject.Find ("btnItem1");
		itemTwo = GameObject.Find ("btnItem2");
		itemThree = GameObject.Find ("btnItem3");

		itemOne.SetActive (false);
		itemTwo.SetActive (false);
		itemThree.SetActive (false);

		isBtnCompraClicked = false;

		information = GameObject.Find ("SelectedEquipmentInformation");
		itemInfo = GameObject.Find ("itemInfo");
		btnComprar = GameObject.Find ("btnComprar");

		information.SetActive (false);
		itemInfo.SetActive (false);
		btnComprar.SetActive (false);

		localPlayer = GameManager.player;

		inicializeEquipmentsList ();

		itemToBuy = null;

		isBtnVoltarClicked = false;
		btnVoltar = GameObject.Find ("btnReturn");
	}

	void inicializeEquipmentsList() {

		Equipments = new List<Equipment> ();
		
		for(int i = 1; i < 13; i++) {
			if(i < 4) Equipments.Add(new Equipment("Sword", "Espadinha " + i, i*2.0f, 0.0f, i,5.0f*(float)(i),5.0f*((float)(i))/2, "Espadinha " + i + " e muito linda!"));
			else if (i < 7) Equipments.Add(new Equipment("Armor", "Armadurinha " + (i-3), 0.0f, i*2.0f, i,5.0f*(float)(i),5.0f*((float)(i))/2, "Armadurinha " + (i-3) + " e muito linda!"));
			else if (i < 10) Equipments.Add(new Equipment("Shield", "Escudinho " + (i-6), 0.0f, i*2.0f, i,5.0f*(float)(i),5.0f*((float)(i))/2, "Escudinho " + (i-6) + " e muito lindo!"));
			else Equipments.Add(new Equipment("Bow", "Arquinho " + (i-9), i*2.5f, 0.0f, i,5.0f*(float)(i),5.0f*((float)(i))/2, "Arquinho " + (i-9) + " e muito lindo!"));

			Debug.Log("Equipamento " + i + ": " + Equipments[i-1].GetNameEquipment());
		}
	}


	// Update is called once per frame
	void Update () 
	{

		if (Camera.main.camera.orthographicSize <= (target - 0.01f)) {

			// current camera depth
			current = Camera.main.camera.orthographicSize;

			// zoom phase
			Camera.main.camera.orthographicSize = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);
				
		}else {
			if(isSwordsClicked) {

				if(information.activeInHierarchy) {
					itemInfo.SetActive(false);
					information.SetActive(false);
					btnComprar.SetActive(false);
				}

				if(itemOne.activeInHierarchy) {
					//apenas modificar o que ja estiver la pelos sprites corretos

				}else {
					//alterar os sprites para as imagens corretas

					itemOne.SetActive(true);
					itemTwo.SetActive(true);
					itemThree.SetActive(true);
				}

				EQUIPMENT_ID = 1;
				isSwordsClicked = false;

			}else if(isArmorsClicked) {

				if(information.activeInHierarchy) {
					itemInfo.SetActive(false);
					information.SetActive(false);
					btnComprar.SetActive(false);
				}
				
				if(itemOne.activeInHierarchy) {
					//apenas modificar o que ja estiver la pelos sprites corretos
					
				}else {
					//alterar os sprites para as imagens corretas
					
					itemOne.SetActive(true);
					itemTwo.SetActive(true);
					itemThree.SetActive(true);
				}

				EQUIPMENT_ID = 2;
				isArmorsClicked = false;

			}else if(isShieldsClicked) {

				if(information.activeInHierarchy) {
					itemInfo.SetActive(false);
					information.SetActive(false);
					btnComprar.SetActive(false);
				}
				
				if(itemOne.activeInHierarchy) {
					//apenas modificar o que ja estiver la pelos sprites corretos
					
				}else {
					//alterar os sprites para as imagens corretas
					
					itemOne.SetActive(true);
					itemTwo.SetActive(true);
					itemThree.SetActive(true);
				}

				EQUIPMENT_ID = 3;
				isShieldsClicked = false;

			}else if(isBowsClicked) {

				if(information.activeInHierarchy) {
					itemInfo.SetActive(false);
					information.SetActive(false);
					btnComprar.SetActive(false);
				}
				
				if(itemOne.activeInHierarchy) {
					//apenas modificar o que ja estiver la pelos sprites corretos
					
				}else {
					//alterar os sprites para as imagens corretas
					
					itemOne.SetActive(true);
					itemTwo.SetActive(true);
					itemThree.SetActive(true);
				}

				EQUIPMENT_ID = 4;
				isBowsClicked = false;

			}else if(isItemOneClicked) {
				//passar as informaçoes do item selecionado

				updateItemInformation(GetSelectedItem(EQUIPMENT_ID,0));

				if(!information.activeInHierarchy) {
					//inserir as informaçoes na telinha

					itemInfo.SetActive(true);
					information.SetActive(true);
					btnComprar.SetActive(true);

				}else {
					//atualizar as informaçoes na telinha
				}

				isItemOneClicked = false;

			}else if(isItemTwoClicked) {
				//passar as informaçoes do item selecionado

				updateItemInformation(GetSelectedItem(EQUIPMENT_ID,1));

				if(!information.activeInHierarchy) {
					//inserir as informaçoes na telinha

					itemInfo.SetActive(true);
					information.SetActive(true);
					btnComprar.SetActive(true);
					
				}else {
					//atualizar as informaçoes na telinha
				}

				isItemTwoClicked = false;
			}else if(isItemThreeClicked) {
				//passar as informaçoes do item selecionado

				updateItemInformation(GetSelectedItem(EQUIPMENT_ID,2));

				if(!information.activeInHierarchy) {
					//inserir as informaçoes na telinha

					itemInfo.SetActive(true);
					information.SetActive(true);
					btnComprar.SetActive(true);
					
				}else {
					//atualizar as informaçoes na telinha
				}

				isItemThreeClicked = false;
			}else if(isBtnCompraClicked) {
				//dar o item ao player

				itemInfo.SetActive(false);
				information.SetActive(false);
				btnComprar.SetActive(false);

				isBtnCompraClicked = false;
			}else if(isBtnVoltarClicked) {
				isBtnVoltarClicked = false;

				GameManager.player = localPlayer;
				Application.LoadLevel("mainScene");
			}
		}
	}

	object GetSelectedItem(int equipType, int position) {
		List<Equipment> specificTypesEquipments = new List<Equipment> ();

		if (equipType == 1) {
			foreach(Equipment w in Equipments) {
				if(w.GetTypeEquipment().Equals("Sword")) {
					specificTypesEquipments.Add(w);
					Debug.Log("Swords quantity: " + specificTypesEquipments.Count);
				}
			}
		}else if (equipType == 2) {
			foreach(Equipment w in Equipments) {
				if(w.GetTypeEquipment().Equals("Armor")) {
					specificTypesEquipments.Add(w);
					Debug.Log("Armors quantity: " + specificTypesEquipments.Count);
				}
			}
		}else if (equipType == 3) {
			foreach(Equipment w in Equipments) {
				if(w.GetTypeEquipment().Equals("Shield")) {
					specificTypesEquipments.Add(w);
					Debug.Log("Shields quantity: " + specificTypesEquipments.Count);
				}
			}
		}else if (equipType == 4) {
			foreach(Equipment w in Equipments) {
				if(w.GetTypeEquipment().Equals("Bow")) {
					specificTypesEquipments.Add(w);
					Debug.Log("Bows quantity: " + specificTypesEquipments.Count);
				}
			}
		}
	
		return specificTypesEquipments[position];
	}

	void updateItemInformation(object generic){
		UILabel content = itemInfo.GetComponent<UILabel> ();

		Equipment selected = generic as Equipment;

		if ((EQUIPMENT_ID != 2) && (EQUIPMENT_ID != 3)) {

			Debug.Log("Selected Weapon: " + selected.GetNameEquipment());
		
			content.text = selected.GetNameEquipment() + "\n\n" +
				"Attack: " + selected.GetAttack() + "\n" +
				//"Defense: " + selected.GetDefense() + "\n" +
				"Payload: " + selected.GetPayload() + "\n" +
				"Price: " + selected.GetPriceOfPurchase() + "\n";
		}else {
			
			Debug.Log("Selected Defender: " + selected.GetNameEquipment());
			
			content.text = selected.GetNameEquipment() + "\n\n" +
				//"Attack: " + selected.GetAttack() + "\n" +
				"Defense: " + selected.GetDefense() + "\n" +
				"Payload: " + selected.GetPayload() + "\n" +
				"Price: " + selected.GetPriceOfPurchase() + "\n";
		}

		itemToBuy = selected;
	}


	void onSwordsClicked()
	{
		isSwordsClicked = true;
	}

	void onArmorsClicked()
	{
		isArmorsClicked = true;
	}

	void onShieldsClicked()
	{
		isShieldsClicked = true;
	}

	void onBowsClicked()
	{
		isBowsClicked = true;
	}

	void onItemOneClicked()
	{
		isItemOneClicked = true;
	}

	void onItemTwoClicked()
	{
		isItemTwoClicked = true;
	}

	void onItemThreeClicked()
	{
		isItemThreeClicked = true;
	}

	void onBtnComprarClicked()
	{
		isBtnCompraClicked = true;
	}

	void onBtnVoltarClicked()
	{
		isBtnVoltarClicked = true;
	}

}
