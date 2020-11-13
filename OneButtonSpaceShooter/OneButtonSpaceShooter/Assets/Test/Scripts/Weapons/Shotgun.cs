using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : ShootWeapon {
	public int bullets;

	protected override void Shoot() {
		SpreadShoot(bullets + Random.Range(-2, 3));
	}

	void SpreadShoot(int n, float angleToDivide = 70) {
		float spreadAngle = angleToDivide / n; //blir vinkel mellan bullets;
		float
			startAngle =
				(n - 1) * spreadAngle /
				2; //gör så att det går att spawna på båda sidor -1 för mitten ska va 0
		for (int i = 0; i < n; i++) {
			GameObject bulletClone = Instantiate(bullet, shootPoint.position,shootPoint.rotation * Quaternion.Euler(0, 0, -startAngle + spreadAngle * i));

			SetVelocity(bulletClone);
		}
	}
}
