using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
	[SerializeField] float minSpawnDelay = 1f;
	[SerializeField] float maxSpawnDelay = 3f;
	[SerializeField] GameObject zombie;
	[SerializeField] private int maxZombies = 10;

	bool spawn = true;
	private GameObject[] spawnedZombies;

	void Start() {
		spawnedZombies = new GameObject[maxZombies];

		StartCoroutine(ZombieSpawn());
	}

	IEnumerator ZombieSpawn() {
		while (spawn) {
			yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
			if (spawn)
				SpawnZombie();
		}
	}

	private void SpawnZombie() {
		for (int i = 0; i < spawnedZombies.Length; i++) {
			if (!spawnedZombies[i]) {
				GameObject newZombie = Instantiate(zombie, transform.position, transform.rotation);
				newZombie.transform.parent = transform;
				spawnedZombies[i] = newZombie;
				break;
			}
		}
	}

	private void OnBecameInvisible() {
		spawn = true;
		StartCoroutine(ZombieSpawn());
	}

	private void OnBecameVisible() {
		spawn = false;
	}
}
