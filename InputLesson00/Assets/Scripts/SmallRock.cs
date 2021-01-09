using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : MonoBehaviour
{
    [HideInInspector] public float size = 1f;
    [HideInInspector] public float speed = 10;
    Rigidbody2D rb;
    void Start()
    {
        transform.localScale = (Vector3.one * size);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var objTag = collision.gameObject.tag;
        if (objTag == "Enemy")
        {
            collision.gameObject.GetComponent<BaseEnemy>().takeDmg(100);
        }
    }
}
