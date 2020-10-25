using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 5;
    public float invunableTime = 1f;
    public GameObject boom;
    public AudioClip takeDmgSound;
    private Color startColor;
    private SpriteRenderer spriteRenderer;
    public GameObject[] livesSprite;


    [SerializeField] private bool canTakeDmg = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Wall")
            return;

        ContactPoint2D contact = collision.GetContact(0);
        TakeDmg(contact.point);
    }

    void TakeDmg(Vector3 expPos)
    {
        if (canTakeDmg)
        {
            lives -= 1;
            if(lives <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }


            Destroy(livesSprite[lives]);
            StartCoroutine(BlinkEffect(3, 0.3f));
            GetComponent<AudioSource>().pitch = 1;
            GetComponent<PlayerMove>().PlayPlayerSound(takeDmgSound, false, 0.5f);
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
        spriteRenderer.color = Color.white;
        //var childSpriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //childSpriteRenderer.color = Color.white;

        yield return new WaitForSeconds(duration);

        spriteRenderer.color = startColor;
        //childSpriteRenderer.color = startColor;
    }

    IEnumerator ImmuneToDmg()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(invunableTime);
        canTakeDmg = true;
    }
}
