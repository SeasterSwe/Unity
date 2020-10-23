using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Range(0, 1f)] public float smoothVal = 0.5f;
    [Range(4,10f)] public float speed = 5f;
    float brainDeadSpeedBug;
    float currentSpeed;
    float speedSmoothVelocity;

    [Range(1, 10)] public int amountOfBullets;
    [Range(1, 45)] public int angleBetweenBullets;

    public GameObject muzzleFlash;
    public GameObject cannonPoint;
    public GameObject bullet;
    public GameObject hitEffekt;

    private AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip takeDmgSound;
    void Start()
    {
        brainDeadSpeedBug = speed;
        audioSource = GetComponent<AudioSource>();
        bullet.GetComponent<PlayerBullet>().hitEffekt = hitEffekt;
        speedTemp = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            speed *= -1;
            Fire();
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, speed, ref speedSmoothVelocity, smoothVal);
        transform.Translate(transform.right * currentSpeed * Time.deltaTime, Space.World);

        if (transform.position.x > 2.8f)
            speed = -brainDeadSpeedBug;
        else if(transform.position.x < -2.8f)
            speed = brainDeadSpeedBug;
    }

    void Fire()
    {
        PlayPlayerSound(shootSound, true);
        MutliShoot(amountOfBullets);
        //StartCoroutine(FireBurst(bullet, 3, 0.1f));
    }

    public IEnumerator FireBurst(GameObject bulletPrefab, int burstSize, float rateOfFire)
    {
        float bulletDelay = rateOfFire;
        StartCoroutine(MinHjärnaDog(burstSize, rateOfFire));
        Vector3 cannon = cannonPoint.transform.position;
        for (int i = 0; i < burstSize; i++)
        {
            Instantiate(muzzleFlash, cannon, muzzleFlash.transform.rotation);
            GameObject bullet = Instantiate(bulletPrefab, cannon, transform.rotation); 
            yield return new WaitForSeconds(bulletDelay); 
        }
    }

    public float speedTemp;
    IEnumerator MinHjärnaDog(float burstSize, float rateOfFire)
    {
        float time = (burstSize-1) * rateOfFire;
        speed = 0;
        smoothVal = 0.01f;
        yield return new WaitForSeconds(time);
        speed = speedTemp *=-1;
        smoothVal = 0.08f;
    }

    void MutliShoot(int n)
    {
        Instantiate(muzzleFlash, cannonPoint.transform.position, muzzleFlash.transform.rotation);
        float angleToDivide = n * angleBetweenBullets;
        float spreadAngle = angleToDivide / n;
        for (int i = 0; i < n; i++)
        {
            float startAngle = (n - 1) * spreadAngle / 2;
            GameObject clone =
            Instantiate(bullet, cannonPoint.transform.position, cannonPoint.transform.rotation * Quaternion.Euler(0, 0, -startAngle + spreadAngle * i));
        }
    }

    public void PlayPlayerSound(AudioClip clip, bool pitchSound = true, float volume = 0.13f)
    {
        audioSource.clip = clip;
        if(pitchSound)
            PitchSound.pitchSound(gameObject, 0.8f, 1.6f);

        audioSource.volume = volume;
        audioSource.Play();
    }
}
