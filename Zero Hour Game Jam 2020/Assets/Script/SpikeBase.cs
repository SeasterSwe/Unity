using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBase : MonoBehaviour
{
    public float speed = 3;
    void Start()
    {
        if (transform.position.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, -90);
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            speed *= -1;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
