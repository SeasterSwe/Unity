using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTest
{
    Health health;
    [SetUp]
    public void Setup()
    {
        health = new Health(4, 10);
        health.health = 4;
    }
    [TearDown]
    public void TearDown()
    {
        health.health = 0;
        health = null;
    }
    [Test]
    public void CanSetHealth()
    {
        health.health = 5;
        Assert.AreEqual(5, health.health);
    }
    [Test]
    [TestCase(1,4)]
    [TestCase(10, 4)]
    [TestCase(5, 4)]
    public void CanAddHealth(int startHealth, int add)
    {
        health.health = startHealth;
        health.Add(add);
        if (startHealth + add >= health.maxHealth)
            Assert.AreEqual(health.health, health.maxHealth);
        else
            Assert.AreEqual(health.health, startHealth + add);
    }
    [Test]
    [TestCase(1, 4)]
    [TestCase(10, 4)]
    [TestCase(5, 4)]
    public void CanRemoveHealth(int startHealth, int add)
    {
        health.health = startHealth;
        health.Remove(add);
        Assert.AreEqual(health.health, startHealth - add);
    }

    [Test]
    [TestCase(1, 4)]
    [TestCase(10, 4)]
    [TestCase(5, 4)]
    public void NegativeNumbersHeals(int startHealth, int add)
    {
        health.health = startHealth;
        health.Add(-add);

        if (startHealth + add >= health.maxHealth)
            Assert.AreEqual(health.health, health.maxHealth);
        else
            Assert.AreEqual(health.health, startHealth + add);
    }

    [Test]
    [TestCase(1, 4)]
    [TestCase(10, 4)]
    [TestCase(5, 4)]
    public void NegativeNumbersDealDamage(int startHealth, int add)
    {
        health.health = startHealth;
        health.Remove(-add);
        Assert.AreEqual(health.health, startHealth - add);
    }

    [Test]
    [TestCase(4)]
    public void CanAddMaxHealth(int add)
    {
        health.AddToMaxHealth(add);
        Assert.Greater(health.maxHealth, add);
    }

    [Test]
    [TestCase(4)]
    public void CanRemoveMaxHealth(int add)
    {
        float startMax = health.maxHealth;
        health.RemoveFromMaxHealth(add);
        Assert.AreEqual(health.maxHealth, startMax - add);
    }
    [Test]
    public void CanSetHealhToMaxHealth()
    {
        health.SetHealthToMaxHealth();
        Assert.AreEqual(health.health, health.maxHealth);
    }

    [Test]
    public void CanResetMaxHealth()
    {
        health.AddToMaxHealth(10);
        health.ResetMaxHealth();
        Assert.AreEqual(health.maxHealth, health.startMax);
    }
    [Test]
    [TestCase(200)]
    public void HealthIsNotGreaterThenMax(int add)
    {
        health.Add(add);
        Assert.GreaterOrEqual(health.maxHealth, health.health);
    }
}
