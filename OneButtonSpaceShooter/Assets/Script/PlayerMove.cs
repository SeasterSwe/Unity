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
    public GameObject muzzleFlash;
    public GameObject cannonPoint;
    public GameObject bullet;
    void Start()
    {
        brainDeadSpeedBug = speed;
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
        Instantiate(muzzleFlash, cannonPoint.transform.position, muzzleFlash.transform.rotation);
        MutliShoot(amountOfBullets);
    }
    void MutliShoot(int n)
    {
        float angleToDivide = n * 10;
        float spreadAngle = angleToDivide / n;
        for (int i = 0; i < n; i++)
        {
            float startAngle = (n - 1) * spreadAngle / 2;
            GameObject clone =
            Instantiate(bullet, cannonPoint.transform.position, cannonPoint.transform.rotation * Quaternion.Euler(0, 0, -startAngle + spreadAngle * i));
        }
    }
}
