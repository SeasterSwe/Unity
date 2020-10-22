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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed *= -1;
            Fire();
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, speed, ref speedSmoothVelocity, smoothVal);
        transform.Translate(transform.right * currentSpeed * Time.deltaTime, Space.World);

        if (transform.position.y > 6.5f)
            speed = brainDeadSpeedBug;
        else if(transform.position.y < -6.5f)
            speed = -brainDeadSpeedBug;
    }

    void Fire()
    {
        PlaySound(shootSound, true);
        Instantiate(muzzleFlash, cannonPoint.transform.position, muzzleFlash.transform.rotation);
        MutliShoot(amountOfBullets);
    }

    void Burst(int n)
    {

    }

    IEnumerable SpawnBullet(float nextSpawn)
    {
        yield return new WaitForSeconds(nextSpawn);
    }
    void MutliShoot(int n)
    {
        float angleToDivide = n * angleBetweenBullets;
        float spreadAngle = angleToDivide / n;
        for (int i = 0; i < n; i++)
        {
            float startAngle = (n - 1) * spreadAngle / 2;
            GameObject clone =
            Instantiate(bullet, cannonPoint.transform.position, cannonPoint.transform.rotation * Quaternion.Euler(0, 0, -startAngle + spreadAngle * i));
        }
    }

    void PlaySound(AudioClip clip, bool pitchSound = true)
    {
        audioSource.clip = clip;
        if(pitchSound)
            PitchSound.pitchSound(gameObject, 0.8f, 1.6f);
        
        audioSource.Play();
    }
}
