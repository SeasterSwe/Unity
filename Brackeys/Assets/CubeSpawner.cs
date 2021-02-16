using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;
    public void SpawnCubes(Transform obj, float spawnRadius)
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3 centerPos = obj.transform.position + (Vector3.up * 2);
            float a = Random.Range(0, 1f) * Mathf.PI * 2;
            float r = spawnRadius * Mathf.Sqrt(Random.Range(0, 1f));
            float x = r * Mathf.Cos(a);
            float y = r * Mathf.Sin(a);
            
            x += centerPos.x;
            y += centerPos.y;

            Vector3 pos = new Vector3(x, y, 0);
            GameObject cubeClone = Instantiate(cube, pos, Quaternion.identity);
        }
    }
}
