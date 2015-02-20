using UnityEngine;
using System.Collections;

public class ToolTipVillage2 : MonoBehaviour {

	void OnHover(bool isOver) {
		
		GameObject scroll = GameObject.Find("tooltipVillage");
		GameObject text = GameObject.Find ("tooltextVillage");
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