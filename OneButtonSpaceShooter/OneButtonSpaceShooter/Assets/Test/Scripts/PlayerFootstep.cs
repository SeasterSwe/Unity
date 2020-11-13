using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour {
	public AudioClip[] footSteps;
	AudioSource audioSource;

	private Joystick playerMove;
	void Start() {
		audioSource = GetComponent<AudioSource>();
		playerMove = transform.parent.GetComponent<PlayerJoyStickMovement>().moveStick;
	}

	// Update is called once per frame
	void Update() {
		Vector2 move = new Vector2(playerMove.Horizontal, playerMove.Vertical);
		if (move != Vector2.zero && audioSource.isPlaying == false) {
			int r = Random.Range(0, footSteps.Length);
			audioSource.clip = footSteps[r];
			audioSource.Play();
		}
	}
}
