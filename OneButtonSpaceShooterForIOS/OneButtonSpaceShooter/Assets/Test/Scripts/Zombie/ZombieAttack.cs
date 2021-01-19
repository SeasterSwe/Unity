using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {
	[SerializeField] private int damage = 1;


	private ZombieMovement controller;

	private void Start() {
		controller = GetComponent<ZombieMovement>();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (!other.gameObject.CompareTag("Player"))
			return;

		if (controller.gameObject != other.gameObject)
			controller.target = other.gameObject.transform;
	}
}
