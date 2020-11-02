using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeeleAttack : MonoBehaviour
{
    private Collider2D collider;
    bool canAttack = true;
    Animator playerAnimator;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        playerAnimator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canAttack)
        {
            playerAnimator.SetTrigger("Attack");
            StartCoroutine(Attack());

            if (transform.parent.GetComponent<SpriteRenderer>().flipX == true)
                transform.localPosition = new Vector3(-0.75f, 0, 0);
            else
                transform.localPosition = new Vector3(0.75f, 0, 0);
        }
    }
   IEnumerator Attack()
    {
        canAttack = false;
        collider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        collider.enabled = false;
        canAttack = true;
    }
}
