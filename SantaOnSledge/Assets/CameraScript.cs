using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float smoothTime = 0.8f;
    public float zOff = 0;
    Vector3 offset;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    void Update()
    {
        Vector3 cameraMovement = (player.position + offset + (Vector3.forward * zOff)) - transform.position;
        cameraMovement.z = cameraMovement.z * smoothTime;
        cameraMovement.x = offset.x;
        cameraMovement.y = 0;
        transform.Translate(cameraMovement * Time.deltaTime);
    }
}
