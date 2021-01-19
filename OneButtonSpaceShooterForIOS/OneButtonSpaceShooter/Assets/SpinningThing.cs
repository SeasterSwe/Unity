using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningThing : MonoBehaviour
{
    float speed = -45;
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
