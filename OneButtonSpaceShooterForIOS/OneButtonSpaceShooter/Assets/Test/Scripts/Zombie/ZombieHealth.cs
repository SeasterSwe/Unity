using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZombieHealth : MonoBehaviour {
	public int startHealth = 3;
	private float currentHealth;
	[SerializeField] GameObject deathVFX;
	[SerializeField] GameObject transformationVFX;
	public AudioClip hurtSound;
	public AudioClip deathSound;
	AudioSource audioSource;
	SpriteRenderer renderer;
	Color startColor;
	public Color hurtColor;

	private void Start() {
		audioSource = GetComponent<AudioSource>();
		currentHealth = startHealth;
		renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
		startColor = renderer.color;
	}

	public void DealDamage(float damage) {
		currentHealth -= damage;
		if (currentHealth <= 0) {
			TriggerDeathVFX();
			return;
		}

		StartCoroutine(SpriteBlink(hurtColor));
		audioSource.clip = hurtSound;
		audioSource.Play();
	}

	private void TriggerDeathVFX() {
		if (!deathVFX)
			return;

		GameObject deathVFXObject =
			Instantiate(deathVFX, transform.position, deathVFX.transform.rotation);

		var deathAS = deathVFXObject.GetComponent<AudioSource>();
		deathAS.clip = deathSound;
		deathAS.Play();

		Destroy(gameObject);
	}

	IEnumerator SpriteBlink(Color blinkColor, float dur = 0.2f) {
		renderer.color = blinkColor;
		yield return new WaitForSeconds(dur);
		renderer.color = startColor;
	}
}
