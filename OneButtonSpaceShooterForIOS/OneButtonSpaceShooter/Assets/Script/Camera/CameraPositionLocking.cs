using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionLocking : MonoBehaviour {
	public Transform target;

	private float startZ;

	// Start is called before the first frame update
	private void Start() {
		target = GameObject.FindWithTag("Player").transform;
		startZ = transform.position.z;
	}

	// Update is called once per frame
	private void Update() {
		Vector3 offset = target.position;
		offset.z = startZ;

		transform.position = offset;
	}
}
