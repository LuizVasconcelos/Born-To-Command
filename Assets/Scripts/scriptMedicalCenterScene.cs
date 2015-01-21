using UnityEngine;
using System.Collections;

public class scriptMedicalCenterScene : MonoBehaviour {

	private float current;
	private float target;
	private float currentVelocity;
	private float smoothTime;

	// Use this for initialization
	void Start () 
	{
		target = 1.1f;
		currentVelocity = 0.0f;
		smoothTime = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Camera.main.camera.orthographicSize < target) 
		{
			
			// current camera depth
			current = Camera.main.camera.orthographicSize;
			
			// zoom phase
			Camera.main.camera.orthographicSize = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime);
			
		}
	}
}
