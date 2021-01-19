using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Color blinkColor = Color.white;
    public float invunableTime = 1f;
    public GameObject boom;
    public AudioClip takeDmgSound;
    private Color startColor;
    private SpriteRenderer spriteRenderer;
    PlayPlayerSound playerSound;
    [SerializeField] private bool canTakeDmg = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        playerSound = GetComponent<PlayPlayerSound>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Bullet")
            return;
      
        //var zzz = collision.gameObject.GetComponent<EnemyBase>().scoreGiven;
        //GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(-(100 + zzz));
        ContactPoint2D contact = collision.GetContact(0);
        TakeDmg(contact.point);
    }

    void TakeDmg(Vector3 expPos)
    {
        if (canTakeDmg)
        {
            StartCoroutine(BlinkEffect(3, 0.3f));
            GetComponent<AudioSource>().pitch = 1;
            playerSound.PlaySound(takeDmgSound, false, 0.5f);
            StartCoroutine(ImmuneToDmg());
        }
        Instantiate(boom, expPos, boom.transform.rotation);
    }

    public IEnumerator BlinkEffect(int blinkTimes, float timeBetweenBlinks)
    {
        float blinkDuration = timeBetweenBlinks / 2;
        for (int i = 0; i < blinkTimes; i++)
        {
            StartCoroutine(Blink(blinkDuration));
            yield return new WaitForSeconds(timeBetweenBlinks);
        }
    }
    IEnumerator Blink(float duration = 0.2f)
    {
        spriteRenderer.color = blinkColor;
        var childSpriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        childSpriteRenderer.color = blinkColor;

        yield return new WaitForSeconds(duration);

        spriteRenderer.color = startColor;
        childSpriteRenderer.color = startColor;
    }

    IEnumerator ImmuneToDmg()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(invunableTime);
        canTakeDmg = true;
    }
}
