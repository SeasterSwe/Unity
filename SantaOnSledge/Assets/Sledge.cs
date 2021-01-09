using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sledge : MonoBehaviour
{
    private enum State
    {
        Normal,
        Rolling,
    }

    public Transform relativeTransform;
    public float rollSpeed;
    public float moveSpeed = 5;
    Rigidbody rb;
    Vector3 moveDir;
    Vector3 rollDir;
    Vector3 lastMoveDir;
    private bool isDashButtonDown;
    private State state;
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        state = State.Normal;
        animator = GetComponent<Animator>();
        if (relativeTransform == null)
            relativeTransform = transform;
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.forward;
        switch (state)
        {
            case State.Normal:
                float moveX = 0;
                float moveZ = 0;
                if (Input.GetKey(KeyCode.A))
                {
                    moveDirection += -relativeTransform.right;
                    moveX += 1;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    moveDirection += relativeTransform.right;
                    moveX -= 1;
                }

                transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

                if (moveX != 0 || moveZ != 0)
                {
                    lastMoveDir = moveDirection;
                }

                if (Input.GetKeyDown(KeyCode.F)) //insta dash
                {
                    isDashButtonDown = true;
                }
                if (Input.GetKeyDown(KeyCode.Space)) //roll
                {
                    rollDir = lastMoveDir;
                    rollSpeed = 20f;
                    state = State.Rolling;
                }
                break;

            case State.Rolling: //roll damn
                float rollSpeedMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedMultiplier * Time.deltaTime;
                float rollspeedmin = 2;
                if (rollSpeed < rollspeedmin)
                {
                    state = State.Normal;
                }
                break;
        }       
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                rb.velocity = moveDir * moveSpeed; //walking
                if (isDashButtonDown)
                {
                    float dashPower = 4; //dash
                    Vector3 dashPos = transform.position + lastMoveDir * dashPower;
                    RaycastHit ray;
                    if (Physics.Raycast(transform.position, lastMoveDir, out ray, dashPower) && ray.collider.tag != "Player")
                    {
                        dashPos = ray.point;
                    }

                    rb.MovePosition(dashPos);
                    isDashButtonDown = false;
                }
                break;
            case State.Rolling:
                rb.velocity = rollDir * rollSpeed; //roll physics
                break;
        }
    }
}
