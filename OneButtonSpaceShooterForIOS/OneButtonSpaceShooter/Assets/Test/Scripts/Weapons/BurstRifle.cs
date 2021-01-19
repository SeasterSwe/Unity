using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRifle : ShootWeapon {
	public int amountOfBullets;

	protected override void Shoot() {
		StartCoroutine(FireBurst(bullet, amountOfBullets, fireRate / (float) amountOfBullets));
	}

	public IEnumerator FireBurst(GameObject bulletPrefab, int burstSize, float rateOfFire) {
		float bulletDelay = rateOfFire;
		for (int i = 0; i < burstSize; i++) {
			if (i > 0)
				PlayAudioClip(shootSound);
			GameObject bulletClone = Instantiate(bulletPrefab, shootPoint.position, transform.rotation);
			SetVelocity(bulletClone);

			GameObject muzzleClone =
				Instantiate(muzzleFlash, shootPoint.position, muzzleFlash.transform.rotation);
			muzzleClone.transform.parent = transform.parent;

			yield return new WaitForSeconds(bulletDelay);
		}
	}
}
