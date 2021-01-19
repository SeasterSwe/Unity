using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHealth = 2;
    float currentHealth;
    private void Start()
    {
        currentHealth = startHealth;
    }
    public bool TakeDmgAndCheckIfAlive(float amount = 1)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            return false;

        return true;
    }
}
