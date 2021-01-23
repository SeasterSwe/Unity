using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class HealthUi : MonoBehaviour
{
    public Image hearth;
    public Health health;
    private float currentHealth;
    public float distBetween;
    Image[] hearths;
    private int dmg;
    void Start()
    {
        currentHealth = health.startHealth;
        dmg = 0;
        InstansiateHearths();
    }
    void InstansiateHearths()
    {
        hearths = new Image[(int)currentHealth];
        float leftDist = -distBetween;

        Vector3 position = transform.position;
        for (int i = 0; i < currentHealth; i++)
        {
            hearths[i] = Instantiate(hearth, position + (Vector3.right * leftDist) + (Vector3.right * i * distBetween), transform.rotation);
            hearths[i].transform.SetParent(transform);
        }
    }
    public void UpdateHearths()
    {
        if (dmg != currentHealth)
        {
            dmg += 1;
            int n = (int)health.startHealth - dmg;
            hearths[n].enabled = false;
        }
    }
}
