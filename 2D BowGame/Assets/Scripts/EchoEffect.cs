using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawn;
    public GameObject echo;

    public bool IsAPlayerEffect = false;
    private PlayerMove playerMove;
    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    void Update()
    {
        if (playerMove.movement != Vector2.zero)
        {
            if (timeBtwSpawns <= 0)
            {
                GameObject echoClone = Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(echoClone, 1f);
                timeBtwSpawns = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }

    }
}
