using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStickMovement : MonoBehaviour
{
    public Joystick moveStick;
    public Joystick rotaionStick;
    [Range(0, 1f)] public float smoothVal = 0.5f;
    [Range(0, 10f)] public float speed = 5f;

    float currentSpeed;
    float speedSmoothVelocity;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = Vector3.zero;
        Move(moveStick.Horizontal, moveStick.Vertical);
        Rotate(rotaionStick.Horizontal, rotaionStick.Vertical);
    }

    void Move(float x, float y)
    {
        Vector3 dir = new Vector3(x, y, 0);
        currentSpeed = Mathf.SmoothDamp(currentSpeed, speed, ref speedSmoothVelocity, smoothVal);
        transform.Translate(dir * currentSpeed * Time.deltaTime, Space.World);
    }
    void Rotate(float x, float y)
    {
        if (Mathf.Abs(x) > 0.01f || Mathf.Abs(y) > 0.01f)
        {
            float angle = Mathf.Atan2(-x, y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

}
