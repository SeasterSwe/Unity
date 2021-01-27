using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class ScoreManager
{
    public static float coockies;
    public static float CPS = 0;
    public static void AddCoockie(float n)
    {
        coockies += n;
    }
    public static void AddCPS(float n)
    {
        CPS += n;
    }
}
