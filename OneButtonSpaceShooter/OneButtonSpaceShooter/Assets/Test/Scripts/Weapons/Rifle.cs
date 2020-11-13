using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : ShootWeapon {
	public float swayStrength;

	protected override void Shoot() {
		GameObject bulletClone = Instantiate(bullet, shootPoint.position,
			shootPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-swayStrength, swayStrength)));

		SetVelocity(bulletClone);
	}
}
