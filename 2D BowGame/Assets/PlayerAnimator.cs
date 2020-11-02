using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMove playerMove;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", math.abs(x));
        animator.SetFloat("Fall", rb.velocity.y);

        if (x > 0.1f)
            spriteRenderer.flipX = false;
        else if (x < -0.1f)
            spriteRenderer.flipX = true;

        if (Input.GetButtonDown("Jump") && playerMove.grounded)
            animator.SetTrigger("Jump");

        if (playerMove.sliding)
            animator.SetBool("Sliding", true);
        else
            animator.SetBool("Sliding", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Landed");
    }
}
