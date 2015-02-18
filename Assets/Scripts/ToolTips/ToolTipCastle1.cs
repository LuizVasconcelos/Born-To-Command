using UnityEngine;
using System.Collections;

public class ToolTipCastle1 : MonoBehaviour {
	
	void OnHover(bool isOver) {
		
		GameObject scroll = GameObject.Find("tooltipCastle");
		GameObject text = GameObject.Find ("tooltextCastle");
		Vector3 pos;
		
		if (isOver) {
			pos = new Vector3 (scroll.transform.position.x, scroll.transform.position.y, -1);
			scroll.transform.position = pos;
			text.transform.position = pos;
		} else {
			pos = new Vector3 (scroll.transform.position.x, scroll.transform.position.y, 1.7f);
			scroll.transform.position = pos;
			text.transform.position = pos;
		}
	}
}