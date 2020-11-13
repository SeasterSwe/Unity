using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour {
	public float swingSpeed = 6;
	public float swingAngle1 = 90;
	public float swingAngle2 = 0;
	public bool goBack = false;

	public AudioClip swordWing;
	public GameObject zombieSlashEffect;
	private AudioSource audioSource;

	bool isSwinging = false;

	private void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space) && !isSwinging) {
			audioSource.clip = swordWing;
			audioSource.Play();
			StartCoroutine(Swong(swingSpeed, swingAngle1, swingAngle2, goBack));
		}
	}

	bool swingRight = true;

	//lmao kanske borde animeras men blev triggrad som satan på unity animator så gjorde animationen i kod :D
	IEnumerator Swong(
		float swingSpeed, float swingAngle = 90, float swingAngle2 = -90, bool returnSword = false) {
		isSwinging = true;
		GetComponent<Collider2D>().enabled = true;

		var point1 = transform.forward * swingAngle;
		var point2 = transform.forward * -swingAngle2;

		if (!swingRight) {
			var tempPoint = point1;
			point1 = point2;
			point2 = tempPoint;
			swingRight = true;
		}
		else
			swingRight = false;

		float t = 0;
		while (t < 1) {
			t += Time.deltaTime * swingSpeed;
			transform.localEulerAngles = Vector3.Lerp(point1, point2, t);
			yield return null;
		}

		if (returnSword)
			StartCoroutine(Swong(swingSpeed, swingAngle, swingAngle2, false));
		else {
			isSwinging = false;
			GetComponent<Collider2D>().enabled = false;
		}

		yield return new WaitForEndOfFrame();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		var tag = collision.gameObject.tag;
		if (tag == "Zombie") {
			collision.gameObject.GetComponent<ZombieHealth>().DealDamage(1);

			foreach (ContactPoint2D contact in collision.contacts) {
				Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
				Vector3 pos = contact.point;
				GameObject zombieEffekt = Instantiate(zombieSlashEffect, pos, rot);
			}

			return;
		}
	}
}
