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

    public int splitTimes = 5;
    float warpDelay;

    OutOfBound outOfBound;
    Health health;
    public void SetRandomSize(float min, float max)
    {
        astRadX = Random.Range(min, max);
        astRadY = Random.Range(min, max);
    }
    public void SetVals(float x, float y, int split)
    {
        astRadX = x;
        astRadY = y;
        splitTimes = split;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        outOfBound = gameObject.AddComponent<OutOfBound>();
        health = gameObject.AddComponent<Health>();
        SetRandomSize(2, 4f);
    }

    void Start()
    {
        if(startTarget != Vector2.zero)
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
                return;

            Split();
        }
    }
    void Split()
    {
        if (astRadX + astRadY > 1f)
        {
            astRadX *= 0.5f;
            astRadY *= 0.5f;
            transform.localScale = new Vector3(astRadX, astRadY, 1);
            GameObject asteriod = Instantiate(gameObject);
            asteriod.GetComponent<Asteroid>().SetVals(astRadX, astRadY, splitTimes);
            asteriod.GetComponent<Asteroid>().SetStartVelocity(GetStartTarget());
            return;
        }
        Destroy(gameObject);
    }

}
