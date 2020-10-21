using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //list av enemys?
    public float spawnRate = 1;
    float timeToNextEnemy = 0;
    public GameObject enemy;
    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time > timeToNextEnemy)
        {
            timeToNextEnemy = Time.time + spawnRate + Random.Range(-0.8f, 2f);
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1, 1f);
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(v3Boundry());
            GameObject EnemyClone = Instantiate(enemy, v3Pos, enemy.transform.rotation);
        }
    }
    Vector3 v3Boundry()
    {
        float r = Random.Range(0, 4);
        if (r == 0)
        {
            //bot
            float x = Random.Range(-1f, 1f);
            float y = -1;
            return new Vector3(x, y, 0);
        }
        if (r == 1)
        {
            //top
            float x = Random.Range(-1f, 1f);
            float y = 2;
            return new Vector3(x, y, 0);
        }
        if (r == 2)
        {
            //left
            float y = Random.Range(-1f, 1f);
            float x = -1;
            return new Vector3(x, y, 0);
        }
        if (r == 3)
        {
            //right
            float y = Random.Range(-1f, 1f);
            float x = 2;
            return new Vector3(x, y, 0);
        }
        return Vector3.zero;
    }
}
