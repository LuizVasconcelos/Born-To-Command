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
	private bool isArrowsClicked;

	private int EQUIPMENT_ID;
	private int SELECTED_ITEM_ID;

	private bool isItemOneClicked;
	private bool isItemTwoClicked;
	private bool isItemThreeClicked;

	private GameObject itemOne;
	private GameObject itemTwo;
	private GameObject itemThree;

	private GameObject information;
	private GameObject itemInfo;

	private List<Weapon> Weapons;

	private Player localPlayer;

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
		isArrowsClicked = false;

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

		information = GameObject.Find ("SelectedEquipmentInformation");
		itemInfo = GameObject.Find ("itemInfo");

		information.SetActive (false);
		itemInfo.SetActive (false);

		inicializeWeaponsList ();
	}

	void inicializeWeaponsList() {

		Weapons = new List<Weapon> ();
		
		for(int i = 1; i < 10; i++) {
			if(i < 4) Weapons.Add(new Weapon("Sword",i,5.0f*(float)(i),5.0f*((float)(i))/2));
			else if (i < 7) Weapons.Add(new Weapon("Bow",i,5.0f*(float)(i),5.0f*((float)(i))/2));
			else Weapons.Add(new Weapon("Arrow",i,1.0f,5.0f*((float)(i-6))/2));

			Debug.Log("Arma " + i + ": " + Weapons[i-1].GetTypeWeapon());
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

			}else if(isArrowsClicked) {

				if(information.activeInHierarchy)  {
					itemInfo.SetActive(false);
					information.SetActive(false);
				}
				
				if(itemOne.activeInHierarchy) {
					//apenas modificar o que ja estiver la pelos sprites corretos
					
				}else {
					//alterar os sprites para as imagens corretas
					
					itemOne.SetActive(true);
					itemTwo.SetActive(true);
					itemThree.SetActive(true);
				}

				EQUIPMENT_ID = 5;
				isArrowsClicked = false;

			}else if(isItemOneClicked) {
				//passar as informaçoes do item selecionado

				updateItemInformation(GetSelectedItem(EQUIPMENT_ID,0));

				if(!information.activeInHierarchy) {
					//inserir as informaçoes na telinha

					itemInfo.SetActive(true);
					information.SetActive(true);

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
					
				}else {
					//atualizar as informaçoes na telinha
				}

				isItemThreeClicked = false;
			}
		}
	}



	object GetSelectedItem(int equipType, int position) {
		List<Weapon> specificTypesWeapons = new List<Weapon> ();
		//List<Weapon> specificTypesDefenses = new List<Weapon> ();

		if (equipType == 1) {
			foreach(Weapon w in Weapons) {
				if(w.GetTypeWeapon().Equals("Sword")) {
					specificTypesWeapons.Add(w);
					Debug.Log("Weapons quantity: " + specificTypesWeapons.Count);
				}
			}
		}else if (equipType == 2) {
			
		}else if (equipType == 3) {
			
		}else if (equipType == 4) {
			foreach(Weapon w in Weapons) {
				if(w.GetTypeWeapon().Equals("Bow")) {
					specificTypesWeapons.Add(w);
					Debug.Log("Weapons quantity: " + specificTypesWeapons.Count);
				}
			}
		}else if (equipType == 5) {
			foreach(Weapon w in Weapons) {
				if(w.GetTypeWeapon().Equals("Arrow")) {
					specificTypesWeapons.Add(w);
					Debug.Log("Weapons quantity: " + specificTypesWeapons.Count);
				}
			}
		}
	
		return specificTypesWeapons [position];
	}

	void updateItemInformation(object generic){
		UILabel content = itemInfo.GetComponent<UILabel> ();

		Weapon selected = null;

		if ((EQUIPMENT_ID != 2) && (EQUIPMENT_ID != 3)) {
			selected = generic as Weapon;

			Debug.Log("Selected Weapon: " + selected.GetPriceOfPurchase());
		
			content.text = selected.GetTypeWeapon() + "\n\n" +
				"Price: " + selected.GetPriceOfPurchase() + "\n" +
				"Payload: " + selected.GetPriceOfSelling() + "\n" +
					"Description: huehue" + /*archers +*/ "\n";
		}
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

	void onArrowsClicked()
	{
		isArrowsClicked = true;
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
}
