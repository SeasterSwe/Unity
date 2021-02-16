using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IExplodeable>().Explode();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IExplodeable>().Explode();
            StartCoroutine(ReloadAfter(1.34f));
        }
    }
    IEnumerator ReloadAfter(float t)
    {
        yield return new WaitForSeconds(t);
        GameManager.Instance.Reload();
    }
}
