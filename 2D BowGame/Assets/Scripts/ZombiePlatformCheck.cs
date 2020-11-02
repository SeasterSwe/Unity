using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePlatformCheck : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.tag != "Arrow")
            transform.parent.GetComponent<Zombie>().ChangeDirection();
    }
}
