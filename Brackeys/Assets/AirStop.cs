using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStop : LandBase
{
    public override void PlayerHit(GameObject gameObject)
    {
        var b = gameObject.GetComponent<BottomSquare>();
        var t = gameObject.GetComponent<TopSquare>();

        if (b != null)
            b.StopMovement(transform);
    }
}
