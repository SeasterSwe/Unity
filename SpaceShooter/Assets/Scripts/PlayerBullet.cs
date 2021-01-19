using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //public int bouncesAllowed = 3;
    //int bounceCount = 0;
    public GameObject explotion;
    int dmg = 1;
    float speed = 15;
    float timeAlive = 1.3f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        StartCoroutine(DestroyBullet(timeAlive));
    }
    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    IEnumerator DestroyBullet(float after)
    {
        yield return new WaitForSeconds(after);
        Instantiate(explotion, transform.position, explotion.transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") //eller player, man skotten kan ej krocka med player pga layers
            return;

        if (collision.gameObject.tag == "Asteroid")  
            collision.gameObject.GetComponent<Asteriod>().SplitAsteroid();

        Instantiate(explotion, transform.position, explotion.transform.rotation);
        Destroy(gameObject);
        /*
       if (bounceCount >= bouncesAllowed)
           Destroy(gameObject);
       bounceCount++;
        */
    }
}
