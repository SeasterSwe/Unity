using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public float smoothingTime = 10;
    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
    void Update()
    {
        transform.Translate(Position() * Time.deltaTime);
    }

    Vector3 Position()
    {
        Vector3 temp = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        temp.x = temp.x * smoothingTime;
        temp.y = temp.y * smoothingTime;
        temp.z = 0;
        return temp;
    }
}
