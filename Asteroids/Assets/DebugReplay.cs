using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugReplay : MonoBehaviour
{
    public GameObject replay;
    Health health;
    private void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    float activateTime = 3;
    bool doOnce = false;
    void Update()
    {
        if (health.currentHealth <= 0 && !doOnce)
        {
            StartCoroutine(Activate());
        }
    }
    IEnumerator Activate()
    {
        doOnce = true;
        yield return new WaitForSeconds(activateTime);
        print("YUea");
        replay.SetActive(true);
    }
}
