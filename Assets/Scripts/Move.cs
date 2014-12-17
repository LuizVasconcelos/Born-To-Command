using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed;
	public float factor;
	
	// Use this for initialization
	void Start () {
		speed = 100000000000;
		factor = 25;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			float step = speed * Time.deltaTime;
			Transform target = gameObject.transform;
			target.position = new Vector3(target.position.x+factor, target.position.y, target.position.z);
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, step);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			float step = speed * Time.deltaTime;
			Transform target = gameObject.transform;
			target.position = new Vector3(target.position.x-factor, target.position.y, target.position.z);
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, step);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			float step = speed * Time.deltaTime;
			Transform target = gameObject.transform;
			target.position = new Vector3(target.position.x, target.position.y+factor, target.position.z);
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, step);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			float step = speed * Time.deltaTime;
			Transform target = gameObject.transform;
			target.position = new Vector3(target.position.x, target.position.y-factor, target.position.z);
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, step);
		}
	}
}
