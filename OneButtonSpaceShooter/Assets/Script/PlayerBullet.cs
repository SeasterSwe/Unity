using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [HideInInspector]
    public GameObject hitEffekt;
    public float speed = 15;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
        if (YeetedOfScreen())
            Destroy(gameObject);
    }

    bool YeetedOfScreen()
    {
        if (transform.position.x > 16)
            return true;
        else
            return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag != "Bullet")
        {
            Instantiate(hitEffekt, transform.position, hitEffekt.transform.rotation);
            Destroy(gameObject);
        }
    }

}
