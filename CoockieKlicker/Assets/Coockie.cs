using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Coockie : MonoBehaviour
{

    public Save save;
    private CoockieData coockieData;
    public float coockiesPerClick = 2;
    private void Awake()
    {
        coockieData = new CoockieData();
        save.LoadCoockies(name);
    }
    private void Start()
    {
    }
    public void Clicked()
    {
        ScoreManager.AddCoockie(coockiesPerClick);
    }

    float addDelay = 0.1f;
    float nextAdd;
    private void Update()
    {
        if (nextAdd < Time.time)
        {
            nextAdd = Time.time + addDelay;
            ScoreManager.AddCoockie(ScoreManager.CPS / 10f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            coockieData.coockieAmount = ScoreManager.coockies;
            coockieData.coockiesPerSec = ScoreManager.CPS;
            save.SaveInfos(coockieData, name);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            save.LoadCoockies(name);
        }

    }

    public void LoadCoockies(CoockieData data)
    {
        coockieData = data;
        ScoreManager.CPS = coockieData.coockiesPerSec;
        ScoreManager.coockies = coockieData.coockieAmount;
    }
}
[Serializable]
public class CoockieData
{
    public float coockieAmount = 0;
    public float coockiesPerSec = 0;
}
