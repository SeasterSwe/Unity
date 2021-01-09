using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public int maxStones;
    private int currentStones = 0;
    public float spawnDelay;
    public GameObject[] stones;
    float timeToNextStone = 0;
    Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(Time.time > timeToNextStone && currentStones < maxStones)
        {
            timeToNextStone = Time.time + spawnDelay;
            Vector2 spawnPos = new Vector2(Random.Range(-60f, 60f), Random.Range(-33f, 33f));
            float dx = playerTransform.position.x - spawnPos.x;
            float dy = playerTransform.position.y - spawnPos.y;
            if (Mathf.Abs(dx) < 9 || Mathf.Abs(dy) < 7)
                return;

            currentStones += 1;
            GameObject rock = Instantiate(stones[Random.Range(0, stones.Length)], spawnPos, transform.rotation);
        }
    }
    public void RemoveStone()
    {
        currentStones -= 1;
    }
}
