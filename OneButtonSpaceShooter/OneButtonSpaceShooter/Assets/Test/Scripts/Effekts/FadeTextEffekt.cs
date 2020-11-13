using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeTextEffekt : MonoBehaviour {
	Vector3 startScale;

	private void Start() {
		startScale = transform.localScale;
	}

	public void UpdateText(string text) {
		GetComponent<TextMeshPro>().text = text;
		StartCoroutine(lerp());
	}

	IEnumerator lerp(float fadeSpeed = 0.8f) {
		var text = GetComponent<TextMeshPro>();
		float visability = 1f;
		Color color = text.color;
		color.a = visability;
		while (visability >= 0) {
			visability -= fadeSpeed * Time.deltaTime;
			color.a = visability;
			text.color = color;
			yield return null;
		}

		visability = 0;
		color.a = visability;
		text.color = color;
		Destroy(gameObject);
		yield return new WaitForEndOfFrame();
	}
}
