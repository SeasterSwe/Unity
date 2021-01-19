using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;
    float nextSpawn;
    public float spawnSpeed = 3;
    void Update()
    {
        if(nextSpawn < Time.time)
        {
            nextSpawn = Time.time + spawnSpeed;
            SpawnAsteroid();
        }
    }
    
    void SpawnAsteroid()
    {
        GameObject asteroidClone = Instantiate(asteroid, OutSidePos(), asteroid.transform.rotation);
        asteroidClone.GetComponent<Asteroid>().startTarget = startTarget.normalized;
    }

    Vector2 startTarget;
    Vector2 OutSidePos()
    {
       int r = Random.Range(0, 4);
        switch (r)
        {
            case 1:
                startTarget = new Vector2(-Boarder.xBoarder + 2, Random.Range(-Boarder.yBoarder, Boarder.yBoarder));
                return new Vector2(Boarder.xBoarder + 2, Random.Range(-Boarder.yBoarder, Boarder.yBoarder));
                break;
            case 2:
                startTarget = new Vector2(Boarder.xBoarder + 2, Random.Range(-Boarder.yBoarder, Boarder.yBoarder));
                return new Vector2(-Boarder.xBoarder - 2, Random.Range(-Boarder.yBoarder, Boarder.yBoarder));
                break;
            case 3:
                startTarget = new Vector2(Random.Range(-Boarder.xBoarder, Boarder.xBoarder), -Boarder.yBoarder + 2);
                return new Vector2(Random.Range(-Boarder.xBoarder, Boarder.xBoarder), Boarder.yBoarder + 2);
                break;
            default:
                startTarget = new Vector2(Random.Range(-Boarder.xBoarder, Boarder.xBoarder), Boarder.yBoarder + 2);
                return new Vector2(Random.Range(-Boarder.xBoarder, Boarder.xBoarder), -Boarder.yBoarder - 2);
                break;
        }
    }
}
