using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemySorts;
    public float spawnRate = 1f;
    private float nextSpawn = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawn < Time.time)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnRandomEnemy();
        }
    }

    void SpawnRandomEnemy()
    {
        int size = enemySorts.Length;
        var typeOfEnemy = Random.Range(0, size);
        float yVal = Random.Range(-2.7f, 2.7f);
        GameObject enemy = Instantiate(enemySorts[typeOfEnemy], new Vector2(yVal, gameObject.transform.position.y),
            enemySorts[typeOfEnemy].transform.rotation);
    }
}
