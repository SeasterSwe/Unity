using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    private float walkSpeed = 5;
    private float runSpeed = 8;
    float currentSpeed;
    float speedSmoothVelocity;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(x, y, 0);

        if (input.sqrMagnitude > 1)
            input = input.normalized;

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * input.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, 0.3f);
        transform.Translate(input * currentSpeed * Time.deltaTime, Space.World);

        EdgeClamp();
        rb.velocity = Vector3.zero;
        LookAtMouse();
    }

    void EdgeClamp()
    {
        //funkat isch för kanter
        //ska skaffa raycast som clampar
        float x = Mathf.Clamp(transform.position.x, -Boarder.xBoarder, Boarder.xBoarder);
        float y = Mathf.Clamp(transform.position.y, -Boarder.yBoarder, Boarder.yBoarder);
        transform.position = new Vector3(x, y, 0);
    }
    void LookAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
