using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int startMax;
    public int maxHealth;
    public int health;
    public Health(int startHealth, int setMaxHealth)
    {
        health = startHealth;
        startMax = setMaxHealth;
        maxHealth = setMaxHealth;
    }
    public void Add(int v)
    {
        v = Mathf.Abs(v);
        health += v;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void Remove(int v)
    {
        v = Mathf.Abs(v);
        health -= v;
    }

    public void AddToMaxHealth(int add)
    {
        maxHealth += add;
    }

    public void RemoveFromMaxHealth(int add)
    {
        maxHealth -= add;
    }

    public void SetHealthToMaxHealth()
    {
        health = maxHealth;
    }

    public void ResetMaxHealth()
    {
        maxHealth = startMax;
    }
}
