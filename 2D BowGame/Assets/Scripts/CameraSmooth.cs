using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{
	public float smoothingTime = 0.8f;

	public Transform target;

	// Update is called once per frame
	void Start()
    {
		if (target == null)
			target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	void Update()
	{
		//Calculate the vector between where we are and where we want to be
		Vector3 cameraMovement = target.position - transform.position;

		//Calculate how long it will take to reach our goal.
		cameraMovement.x = cameraMovement.x * smoothingTime;
		cameraMovement.y *= smoothingTime;
		cameraMovement.z = 0;

		//move closer over time
		transform.Translate(cameraMovement * Time.deltaTime);
	}
}