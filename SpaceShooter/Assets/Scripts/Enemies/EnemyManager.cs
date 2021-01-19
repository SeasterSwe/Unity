using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //list av enemys?
    public float spawnRate = 1.3f;
    float timeToNextEnemy = 0;
    public GameObject enemy;
    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time > timeToNextEnemy)
        {
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(v3Boundry());
            if (Mathf.Abs(v3Pos.x) > 64 || Mathf.Abs(v3Pos.y) > 37)
                return;

            spawnRate -= 0.00005f;
            Spawn(v3Pos);
        }
    }

    void Spawn(Vector3 v3Pos)
    {
        timeToNextEnemy = Time.time + spawnRate + Random.Range(-0.8f, 0.8f);
        GameObject EnemyClone = Instantiate(enemy, v3Pos, enemy.transform.rotation);
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
