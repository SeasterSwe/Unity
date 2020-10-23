using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int scoreGiven;
    public float speed = 4;
    public int startHealth = 1;
    public GameObject deathEffekt;
    float currentHealth;
    private Color startColor;
    void Start()
    {
        //startHealth += Mathf.RoundToInt(Time.time / 10);
        currentHealth = startHealth;
        startColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        transform.Translate(transform.right * -speed * Time.deltaTime);
    }
    protected virtual void TakeDmg()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
            Death();
        else
        {
            StartCoroutine(Blink());
            PitchSound.pitchSound(gameObject, 0.4f, 1.3f);
            GetComponent<AudioSource>().Play();
        }


    }

    protected virtual void Death(bool instanceParticle = true, bool givesScore = true)
    {
        if(instanceParticle)
            Instantiate(deathEffekt, transform.position, deathEffekt.transform.rotation);
        if(givesScore)
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(scoreGiven);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Bullet")
        {
            TakeDmg();
            return;
        }
        else if (tag == "Enemy")
            return;
        Death(false, false);
    }

    IEnumerator Blink(float duration = 0.1f)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = startColor;
    }
}
