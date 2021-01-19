using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public float spawnTime = 0.5f;
    float nextSpawn = 0;
    public GameObject asteroid;
    public GameObject hexStroid;
 
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;
            Spawn();
        }
    }

    void Spawn()
    {
        int n = (int)Random.Range(1, 3);
        for (int i = 0; i < n; i++)
        {
            if(i % 2 == 0)
                Instantiate(asteroid, SpawnPosAsteroid(), asteroid.transform.rotation);
            else
                Instantiate(hexStroid, SpawnPosAsteroid(), asteroid.transform.rotation);
        }
    }

    Vector3 SpawnPosAsteroid()
    {
        // x = 8.3
        // y = 4,3 && 4,7
        var r = Random.Range(0, 4);
        if (r == 0)
            return new Vector3(-8.3f, Random.Range(-4.7f, 4.3f));
        else if (r == 1)
            return new Vector3(8.3f, Random.Range(-4.7f, 4.3f));
        else if (r == 2)
            return new Vector3(Random.Range(-8.3f,8.3f), 4.3f);
        else if (r == 3)
            return new Vector3(Random.Range(-8.3f, 8.3f), -4.7f);
        return Vector3.zero;
    }
}
