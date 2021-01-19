using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject particleObj;
    public Transform shootPos;
    private Particle particle;
    private float nextFire;
    public float fireRate;
    private void Awake()
    {
        particle = gameObject.AddComponent<Particle>();
        particle.particle = particleObj;
        particleObj.transform.SetParent(shootPos);
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            particle.PlayParticle(shootPos.position);
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject bullet = BulletObjectPool.SharedInstance.GetPooledObject();
        if(bullet != null)
        {
            bullet.transform.position = shootPos.position;
            bullet.transform.rotation = shootPos.rotation;
            bullet.SetActive(true);
        }
    }
}
