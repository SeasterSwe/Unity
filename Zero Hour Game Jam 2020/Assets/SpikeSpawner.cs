using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    public float spawnRate = 1;
    float nextSpawn =  0;
    public GameObject spike;
    
    void Update()
    {
        if(nextSpawn < Time.time)
        {
            nextSpawn = spawnRate + Time.time;
            
            int r = Random.Range(0, 3);
            float side = Random.Range(-1f, 1f);

            float x = -8.3f;
            if (side > 0)
                x = 8.3f;

            Vector3 pos = new Vector3(x, 6.5f, 0);

            for (int i = 0; i < r; i++)
            {
                GameObject spikeClone = Instantiate(spike, pos + (Vector3.up * i * 1), spike.transform.rotation);
            }
        }
    }
}
