﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 10;

    Rigidbody2D rb2d;
    [HideInInspector]
    public Vector2 movement = new Vector2();
    bool grounded = false;
    bool jump = false;
    private PlayerSoundManager soundManager;
    public GameObject landEffekt;
    public AudioClip landSound;
    public AudioClip jumpSound;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        soundManager = GetComponent<PlayerSoundManager>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        movement = new Vector2(x * speed, rb2d.velocity.y);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movement;

        if (jump)
        {
            soundManager.PlayPlayerSound(jumpSound);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        soundManager.PlayPlayerSound(landSound);
        Instantiate(landEffekt, pos, rot);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(JumpTimer(0.2f));
    }
    IEnumerator JumpTimer(float time)
    {
        yield return new WaitForSeconds(time);
        grounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }
}
