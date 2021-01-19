using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
	Vector3 orginalPosition;

	// Start is called before the first frame update
	void Start() {
		orginalPosition = transform.localPosition;
	}

	public void ShakeCamera(float duration = 0.5f, float strength = 0.06f) {
		StartCoroutine(Shake(duration, strength));
	}

	IEnumerator Shake(float d, float s) {
		float activeTime = 0;
		while (d > activeTime) {
			transform.localPosition = orginalPosition + Random.insideUnitSphere * s;
			activeTime += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = orginalPosition;
		yield return new WaitForEndOfFrame();
	}
}
