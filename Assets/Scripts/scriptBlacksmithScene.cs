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

	private Equipment itemToBuy;

	private Player localPlayer;

	private bool isBtnVoltarClicked;
	private GameObject btnVoltar;

	private GameObject spt11;
	private GameObject spt12;
	private GameObject spt13;
	private GameObject spt14;
	private GameObject spt21;
	private GameObject spt22;
	private GameObject spt23;
	private GameObject spt24;
	private GameObject spt31;
	private GameObject spt32;
	private GameObject spt33;
	private GameObject spt34;

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

		spt11 = itemOne.transform.FindChild ("Sprite (item1.1)").gameObject;
		spt12 = itemOne.transform.FindChild ("Sprite (item1.2)").gameObject;
		spt13 = itemOne.transform.FindChild ("Sprite (item1.3)").gameObject;
		spt14 = itemOne.transform.FindChild ("Sprite (item1.4)").gameObject;
		spt21 = itemTwo.transform.FindChild ("Sprite (item2.1)").gameObject;
		spt22 = itemTwo.transform.FindChild ("Sprite (item2.2)").gameObject;
		spt23 = itemTwo.transform.FindChild ("Sprite (item2.3)").gameObject;
		spt24 = itemTwo.transform.FindChild ("Sprite (item2.4)").gameObject;
		spt31 = itemThree.transform.FindChild ("Sprite (item3.1)").gameObject;
		spt32 = itemThree.transform.FindChild ("Sprite (item3.2)").gameObject;
		spt33 = itemThree.transform.FindChild ("Sprite (item3.3)").gameObject;
		spt34 = itemThree.transform.FindChild ("Sprite (item3.4)").gameObject;
	}

	void inicializeEquipmentsList() {

		Equipments = new List<Equipment> ();
		
		Equipments.Add(new Equipment("Sword",1.2f,0.0f,275));
		Equipments.Add(new Equipment("Sword",1.5f,0.0f,725));
		Equipments.Add(new Equipment("Sword",1.8f,0.0f,1350));
		Equipments.Add(new Equipment("Armor",0.0f,1.15f,320));
		Equipments.Add(new Equipment("Armor",0.0f,1.4f,965));
		Equipments.Add(new Equipment("Armor",0.0f,1.6f,1510));
		Equipments.Add(new Equipment("Shield",0.0f,2.0f,200));
		Equipments.Add(new Equipment("Shield",0.0f,4.0f,525));
		Equipments.Add(new Equipment("Shield",0.0f,5.0f,830));
		Equipments.Add(new Equipment("Bow",1.25f,0.0f,215));
		Equipments.Add(new Equipment("Bow",1.45f,0.0f,595));
		Equipments.Add(new Equipment("Bow",1.6f,0.0f,870));
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
					spt11.SetActive(true);
					spt12.SetActive(false);
					spt13.SetActive(false);
					spt14.SetActive(false);
					spt21.SetActive(true);
					spt22.SetActive(false);
					spt23.SetActive(false);
					spt24.SetActive(false);
					spt31.SetActive(true);
					spt32.SetActive(false);
					spt33.SetActive(false);
					spt34.SetActive(false);

				}else {
					//alterar os sprites para as imagens corretas

					spt11.SetActive(true);
					spt12.SetActive(false);
					spt13.SetActive(false);
					spt14.SetActive(false);
					spt21.SetActive(true);
					spt22.SetActive(false);
					spt23.SetActive(false);
					spt24.SetActive(false);
					spt31.SetActive(true);
					spt32.SetActive(false);
					spt33.SetActive(false);
					spt34.SetActive(false);

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
					spt11.SetActive(false);
					spt12.SetActive(true);
					spt13.SetActive(false);
					spt14.SetActive(false);
					spt21.SetActive(false);
					spt22.SetActive(true);
					spt23.SetActive(false);
					spt24.SetActive(false);
					spt31.SetActive(false);
					spt32.SetActive(true);
					spt33.SetActive(false);
					spt34.SetActive(false);
					
				}else {
					//alterar os sprites para as imagens corretas
					spt11.SetActive(false);
					spt12.SetActive(true);
					spt13.SetActive(false);
					spt14.SetActive(false);
					spt21.SetActive(false);
					spt22.SetActive(true);
					spt23.SetActive(false);
					spt24.SetActive(false);
					spt31.SetActive(false);
					spt32.SetActive(true);
					spt33.SetActive(false);
					spt34.SetActive(false);

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
					spt11.SetActive(false);
					spt12.SetActive(false);
					spt13.SetActive(true);
					spt14.SetActive(false);
					spt21.SetActive(false);
					spt22.SetActive(false);
					spt23.SetActive(true);
					spt24.SetActive(false);
					spt31.SetActive(false);
					spt32.SetActive(false);
					spt33.SetActive(true);
					spt34.SetActive(false);
					
				}else {
					//alterar os sprites para as imagens corretas
					spt11.SetActive(false);
					spt12.SetActive(false);
					spt13.SetActive(true);
					spt14.SetActive(false);
					spt21.SetActive(false);
					spt22.SetActive(false);
					spt23.SetActive(true);
					spt24.SetActive(false);
					spt31.SetActive(false);
					spt32.SetActive(false);
					spt33.SetActive(true);
					spt34.SetActive(false);
					
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
					spt11.SetActive(false);
					spt12.SetActive(false);
					spt13.SetActive(false);
					spt14.SetActive(true);
					spt21.SetActive(false);
					spt22.SetActive(false);
					spt23.SetActive(false);
					spt24.SetActive(true);
					spt31.SetActive(false);
					spt32.SetActive(false);
					spt33.SetActive(false);
					spt34.SetActive(true);
					
				}else {
					//alterar os sprites para as imagens corretas
					spt11.SetActive(false);
					spt12.SetActive(false);
					spt13.SetActive(false);
					spt14.SetActive(true);
					spt21.SetActive(false);
					spt22.SetActive(false);
					spt23.SetActive(false);
					spt24.SetActive(true);
					spt31.SetActive(false);
					spt32.SetActive(false);
					spt33.SetActive(false);
					spt34.SetActive(true);
					
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
					//btnComprar.SetActive(true);
				}

				isItemOneClicked = false;

			}else if(isItemTwoClicked) {
				//passar as informaçoes do item selecionado

				updateItemInformation(GetSelectedItem(EQUIPMENT_ID,1));

				if(!information.activeInHierarchy) {
					//inserir as informaçoes na telinha

					itemInfo.SetActive(true);
					information.SetActive(true);
					//btnComprar.SetActive(true);	
				}

				isItemTwoClicked = false;
			}else if(isItemThreeClicked) {
				//passar as informaçoes do item selecionado

				updateItemInformation(GetSelectedItem(EQUIPMENT_ID,2));

				if(!information.activeInHierarchy) {
					//inserir as informaçoes na telinha

					itemInfo.SetActive(true);
					information.SetActive(true);
					//btnComprar.SetActive(true);	
				}

				isItemThreeClicked = false;
			}else if(isBtnCompraClicked) {
				//dar o item ao player
				if(localPlayer.Gold >= itemToBuy.GetPriceOfPurchase()) {
					if((itemToBuy.GetType().Equals("Sword")) || (itemToBuy.GetType().Equals("Bow"))) {
						localPlayer.UpgradeWeapon(new Item(itemToBuy.GetAttack()));
					}else if(itemToBuy.GetType().Equals("Armor")) {
						localPlayer.UpgradeArmor(new Item(itemToBuy.GetDefense()));
					}else {
						localPlayer.UpgradeShield(new Item(itemToBuy.GetDefense()));
					}

					localPlayer.Gold -= itemToBuy.GetPriceOfPurchase();

					itemInfo.SetActive(false);
					information.SetActive(false);
					btnComprar.SetActive(false);
				}

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
		
			if((localPlayer.Items[1].Val == selected.GetDefense()) || (localPlayer.Items[2].Val == selected.GetDefense())) {
				content.text = selected.GetTypeEquipment() + "s\n\n" +
					"Attack: " + selected.GetAttack() + " rate\n" +
						//"Defense: " + selected.GetDefense() + "\n" +
						"Price: " + selected.GetPriceOfPurchase() + " gold\n\n\n" + 
						"Owned!";

				if(btnComprar.activeInHierarchy) {
					btnComprar.SetActive(false);
				}
			}else {
				content.text = selected.GetTypeEquipment() + "s\n\n" +
					"Attack: " + selected.GetAttack() + " rate\n" +
						//"Defense: " + selected.GetDefense() + "\n" +
						"Price: " + selected.GetPriceOfPurchase() + " gold\n";

				if(!btnComprar.activeInHierarchy) {
					btnComprar.SetActive(true);
				}
			}
		}else {

			if((localPlayer.Items[1].Val == selected.GetDefense()) || (localPlayer.Items[2].Val == selected.GetDefense())) {
				content.text = selected.GetTypeEquipment() + "s\n\n" +
					//"Attack: " + selected.GetAttack() + "\n" +
					"Defense: " + selected.GetDefense() + " rate\n" +
						"Price: " + selected.GetPriceOfPurchase() + " gold\n\n\n" + 
						"Owned!";

				if(btnComprar.activeInHierarchy) {
					btnComprar.SetActive(false);
				}
			}else {
				content.text = selected.GetTypeEquipment() + "s\n\n" +
					//"Attack: " + selected.GetAttack() + "\n" +
					"Defense: " + selected.GetDefense() + " rate\n" +
						"Price: " + selected.GetPriceOfPurchase() + " gold\n";

				if(!btnComprar.activeInHierarchy) {
					btnComprar.SetActive(true);
				}
			}
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
