using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBase : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var hitObj = collision.gameObject;
        if (!(hitObj.CompareTag("Player")))
            return;

        PlayerHit(hitObj);
    }
    public virtual void PlayerHit(GameObject gameObject)
    {
        var b = gameObject.GetComponent<BottomSquare>();
        var t = gameObject.GetComponent<TopSquare>();

        if (b != null)
            b.StopMovement();
    }
}
