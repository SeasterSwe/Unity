using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    float speed;
    Vector3 astroidDir;
    Rigidbody2D rb;
    Color32 color;
    public GameObject boom;
    protected float scoreToGive = 10;
    protected int health = 1;
    public virtual void Awake()
    {
        float r = Random.Range(1f, 3f);
        transform.localScale = new Vector3(r, r, r);

        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(2f, 3f);
        rb.velocity = RandomDirection().normalized * speed;
        var rad = GetComponent<CircleCollider2D>().radius;
        rad = r;

        color = new Color32((byte)Random.Range(0, 162), (byte)Random.Range(192, 236), (byte)Random.Range(20, 142f), 255);
        GetComponent<SpriteRenderer>().color = color;
    }
    private void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    void AsteroidDied()
    {
        Destroy(gameObject);
    }

    public void SplitAsteroid()
    {
        health -= 1;
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(scoreToGive);
            transform.localScale *= 0.5f;
            var scale = transform.localScale;

            if (scale.z < 0.5)
            {
                //Instantiate(boom, transform.position, boom.transform.rotation); 
                Destroy(gameObject);
            }


            else
            {
                GameObject ast = Instantiate(gameObject, transform.position, transform.rotation);
                ast.transform.localScale = transform.localScale;
                ast.GetComponent<SpriteRenderer>().color = color;
            }
        }


    }

    private Vector3 RandomDirection()
    {
        float x = Random.Range(-9, 10);
        float y = Random.Range(-6, 7);
        Vector3 vector = new Vector3(x, y, 0);
        return vector;
    }

    //debugSakISch
    float yeah;
    private void OnCollisionStay2D(Collision2D collision)
    {
        yeah += Time.deltaTime;
        if (yeah > 1)
            rb.velocity = RandomDirection().normalized * speed;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        yeah = 0;
    }
}
