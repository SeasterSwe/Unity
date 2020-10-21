using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaAst : Asteriod
{
    Color32 color;
    private void Start()
    {
        color = new Color32((byte)Random.Range(1, 142f), (byte)Random.Range(66, 90f), (byte)Random.Range(207, 255f), 255);
        GetComponent<SpriteRenderer>().color = color;
        scoreToGive = 20;
        health = (int)Random.Range(2,4);
    }
}
