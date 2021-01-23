using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public GameObject replay;
    public HealthUi healthUi;
    Health health;
    bool DoOnce = false;
    private void Start()
    {
        health = GetComponent<Health>();
        health.blinkTime = 0.3f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            return;

        healthUi.UpdateHearths();
        if (!health.TakeDmgAndCheckIfAlive(1) && !DoOnce)
        {
            DoOnce = true;
            SoundManager.PlaySound(SoundManager.Sound.GameOverSound);
            GetComponent<SpriteRenderer>().color = Color.red;
            transform.FindChild("Cannon").GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerShoot>().enabled = false;
            //GetComponent<Health>().enabled = false;
            StartCoroutine(Death());

        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.HurtSound);
        }
    }

    IEnumerator Death()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<AsteroidSpawner>().enabled = false;
        yield return new WaitForEndOfFrame();
        var astroids = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in astroids)
        {
            obj.GetComponent<Asteroid>().Explotion();
            yield return new WaitForEndOfFrame();
        }
        replay.SetActive(true);
    }
}
