using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Bought();
static class ScoreManager
{
    public static float coockies;
    public static float CPS = 0;
    public static Bought bought;
    public static void AddCoockie(float n)
    {
        coockies += n;
        bought?.Invoke();
    }
    public static void AddCPS(float n)
    {
        CPS += n;
        bought?.Invoke();
    }
}
