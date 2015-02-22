using UnityEngine;
using System.Collections;

public class DialogUtils : MonoBehaviour {

	// Main Panel
	GameObject dialogPanel;
	Vector3 invisibleVector;
	Vector3 visibleVector;
	
	// Message
	GameObject dialogMessage;
	Vector3 visibleVectorMessage;
	
	// Button1
	GameObject option1;
	GameObject button1;
	Vector3 visibleVectorOption1;
	
	// Button2
	GameObject option2;
	GameObject button2;
	Vector3 visibleVectorOption2;

	// Single Button
	GameObject singleOption;
	GameObject singleButton;
	Vector3 visibleVectorSingleButton;

	bool chosenOption;
	
	public DialogUtils(){
		chosenOption = false;
		// Panel
		dialogPanel = GameObject.Find("DialogPanel");
		visibleVector = dialogPanel.transform.position;
		invisibleVector = new Vector3(400f,400f,400f);
		
		// Message
		dialogMessage = GameObject.Find("DialogMessage");
		visibleVectorMessage = dialogMessage.transform.position;
		
		// Button 1 
		option1 = GameObject.Find ("LabelMessage1");
		button1 = GameObject.Find("DialogButton1");
		visibleVectorOption1 = button1.transform.position;
		
		// Button 2
		option2 = GameObject.Find ("LabelMessage2");
		button2 = GameObject.Find ("DialogButton2");
		visibleVectorOption2 = button2.transform.position;

		// Single Button
		singleOption = GameObject.Find ("LabelMessage1");
		singleButton = GameObject.Find ("DialogButton1");
		visibleVectorSingleButton = (visibleVectorOption1 + visibleVectorOption1)/2;

		// Hide initially the dialog message
		hideDialog();
	}

	public void showSingleDialogMessage(string title, string message, string option){
		singleButton.SetActive(true);
		dialogPanel.transform.position = visibleVector;
		dialogMessage.transform.position = visibleVectorMessage;
		singleButton.transform.position = visibleVectorSingleButton;
		// Setting title, message and button's labels
		UILabel content = dialogMessage.GetComponent<UILabel> ();
		content.text = "\t" + title + "\n" + message;
		GameObject option1Label = GameObject.Find("LabelMessage1");
		option1Label.GetComponent<UILabel>().text = option;
	}

	public void showDialogMessage(string title, string message, string option1, string option2){
		button1.SetActive(true);
		button2.SetActive (true);

		// Making dialog visible
		dialogPanel.transform.position = visibleVector;
		dialogMessage.transform.position = visibleVectorMessage;
		button1.transform.position = visibleVectorOption1;
		button2.transform.position = visibleVectorOption2;

		// Setting title, message and button's labels
		UILabel content = dialogMessage.GetComponent<UILabel> ();
		content.text = "\t" + title + "\n" + message;
		GameObject option1Label = GameObject.Find("LabelMessage1");
		option1Label.GetComponent<UILabel>().text = option1;
		GameObject option2Label = GameObject.Find("LabelMessage2");
		option2Label.GetComponent<UILabel>().text = option2;

	}
	
	public void hideDialog(){
		dialogPanel.transform.position = invisibleVector;
		dialogMessage.transform.position = invisibleVector;
		button1.transform.position = invisibleVector;
		button2.transform.position = invisibleVector;
		button1.SetActive(false);
		button2.SetActive (false);
	}
	


}

