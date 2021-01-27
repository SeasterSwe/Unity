using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coockie : MonoBehaviour
{
    public float coockiesPerClick = 2;
    public void Clicked()
    {
        ScoreManager.AddCoockie(coockiesPerClick);
    }

    float addDelay = 0.1f;
    float nextAdd;
    private void Update()
    {
        if(nextAdd < Time.time)
        {
            nextAdd = Time.time + addDelay;
            ScoreManager.AddCoockie(ScoreManager.CPS / 10f);
        }
    }
}
