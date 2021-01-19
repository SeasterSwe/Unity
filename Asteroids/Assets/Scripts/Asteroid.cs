using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 velocity;
    public Vector2 startTarget;

    float speed = 2;

    float astRadX = 2;
    float astRadY = 2;

    public int hitsToDeath = 5;
    float warpDelay;

    OutOfBound outOfBound;
    Health health;
    Particle particle;
    public void SetRandomSize(float min, float max)
    {
        astRadX = Random.Range(min, max);
        astRadY = Random.Range(min, max);
    }
    public void SetVals(float x, float y, int split)
    {
        astRadX = x;
        astRadY = y;
        hitsToDeath = split;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        outOfBound = gameObject.AddComponent<OutOfBound>();
        health = gameObject.AddComponent<Health>();
        health.startHealth = hitsToDeath;
        SetRandomSize(3f, 4f);
        particle = GetComponent<Particle>();
        particle.ParticleName = "Explotion Variant";
    }

    void Start()
    {
        if (startTarget != Vector2.zero)
            SetStartVelocity(startTarget);

        transform.localScale = new Vector3(astRadX, astRadY, 1);
    }

    public void SetStartVelocity(Vector2 vel)
    {
        rb.AddForce(vel * speed, ForceMode2D.Impulse);
        velocity = rb.velocity;
    }

    Vector2 GetStartTarget()
    {
        float x = Random.Range(-Boarder.xBoarder, Boarder.xBoarder);
        float y = Random.Range(-Boarder.yBoarder, Boarder.yBoarder);
        Vector2 startTarget = new Vector2(x, y).normalized;

        return startTarget;
    }


    void Update()
    {
        OutOfBounds();
        rb.velocity = velocity.normalized * speed;
    }

    void OutOfBounds()
    {
        if (warpDelay < Time.time)
        {
            Vector3 pos = transform.position;
            if (outOfBound.OutOfBoundsX(astRadX * 1.5f))
            {
                warpDelay = Time.time + 0.5f;
                pos.x *= -1;
                transform.position = pos;
            }

            if (outOfBound.OutOfBounds(astRadY * 1.5f))
            {
                warpDelay = Time.time + 0.5f;
                pos.y *= -1;
                transform.position = pos;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Bullet")
        {
            if (health.TakeDmgAndCheckIfAlive())
            {
                SoundManager.PlaySound(SoundManager.Sound.HitSound, transform.position, Random.Range(0.7f, 1.4f), 0.5f);
                return;
            }

            Split();
        }
    }
    void Split()
    {
        hitsToDeath -= 1;
        SoundManager.PlaySound(SoundManager.Sound.Explotion, transform.position, Random.Range(0.7f, 1.4f), 0.5f);
        ScoreManager.AddScore(20);
        if (astRadX + astRadY > 1f)
        {
            astRadX *= 0.5f;
            astRadY *= 0.5f;
            transform.localScale = new Vector3(astRadX, astRadY, 1);
            GameObject asteriod = Instantiate(gameObject);
            asteriod.GetComponent<Asteroid>().SetVals(astRadX, astRadY, hitsToDeath);
            asteriod.GetComponent<Asteroid>().SetStartVelocity(GetStartTarget());
            particle.PlayParticle(MiddleOf(gameObject, asteriod));
            return;
        }
        particle.PlayParticle(transform.position);
        Destroy(gameObject);
    }
    Vector3 MiddleOf(GameObject one, GameObject two)
    {
        //josh.position.x + (mark.position.x - josh.position.x) / 2;
        float x = one.transform.position.x + (one.transform.position.x - two.transform.position.x) * 0.5f;
        float y = one.transform.position.y + (one.transform.position.y - two.transform.position.y) * 0.5f;
        return new Vector3(x, y, one.transform.position.z);
    }

}
