using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool SharedInstance;
    public GameObject bulletPrefab;
    public int maxBullets = 10;

    private List<GameObject> pooledBullets = new List<GameObject>();

    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < maxBullets; i++)
        {
            temp = Instantiate(bulletPrefab);
            bulletPrefab.SetActive(false);
            pooledBullets.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
                return pooledBullets[i];
        }
        return null;
    }
}
