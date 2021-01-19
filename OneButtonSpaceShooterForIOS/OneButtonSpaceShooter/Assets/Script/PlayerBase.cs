using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public GameObject boom;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDmg()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if(tag == "Enemy")
        {
            TakeDmg();
            GameObject enemyHitÉffekt = Instantiate(boom, collision.GetContact(0).point, boom.transform.rotation);
        }
    }
}
