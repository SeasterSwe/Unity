using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : LandBase
{
    public int targetAmount = 2;
    private int enterd = 0;
    public override void PlayerHit(GameObject gameObject)
    {
        base.PlayerHit(gameObject);
        enterd++;
        if (targetAmount <= enterd)
        {
            Debug.Log("Win");
        }
    }
}
