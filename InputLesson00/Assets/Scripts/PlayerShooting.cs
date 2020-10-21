using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private GameObject player;
    public GameObject cannonPoint;
    public GameObject bullet;
    public GameObject muzzleFlash;

    private float timeToNextBullet = 0;
    [Range(0f, 1f)]
    public float fireRate = 1;
    [Range(1, 36)]
    public int amountOfBullets = 1;
    [Range(0f, 360f)]
    public float angleToSpread = 360;

    [SerializeField]
    private bool autoMatedSpread;
    private ScoreManager scoreManger;
    float scoreToUnlockBullet = 200;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreManger = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }
    void Update()
    {
        if (Time.time > timeToNextBullet && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            Instantiate(muzzleFlash, cannonPoint.transform.position, muzzleFlash.transform.rotation);
            MutliShoot(amountOfBullets, angleToSpread);
            timeToNextBullet = Time.time + fireRate;
            FireKnockBack();
        }
        if (scoreManger.score >= scoreToUnlockBullet)
        {
            amountOfBullets += 1;
            scoreToUnlockBullet *= 2;
        }
    }

    void MutliShoot(int n, float angleToDivide)
    {
        if (autoMatedSpread)
            angleToDivide = n * angleToSpread;

        float spreadAngle = angleToDivide / n;
        for (int i = 0; i < n; i++)
        {
            float startAngle = (n - 1) * spreadAngle / 2;
            GameObject clone =
            Instantiate(bullet, cannonPoint.transform.position, cannonPoint.transform.rotation * Quaternion.Euler(0, 0, -startAngle + spreadAngle * i));
        }
    }
    void FireKnockBack()
    {
        player.GetComponent<Rigidbody2D>().AddForce(player.transform.up * (-1000 * fireRate), ForceMode2D.Force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Asteroid" && collision.gameObject.tag != "Enemy")
            return;

        return; //Kan kommentera ut ifall man inte vill kunnda dö å ba skjuta
        amountOfBullets -= 1;
        if (amountOfBullets <= 0)
            print("died");
    }
}
