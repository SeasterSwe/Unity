using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float speed = 10;

    public float startHealth = 4;
    private float currentHealth;

    public Color color1;
    public Color color2;
    private Color currentColor;

    private Transform playerTransform;

    public GameObject explotion;
    void Start()
    {
        currentHealth = startHealth;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float alphaVal = Random.Range(0.5f, 1f);
        color1.a = alphaVal;
        color2.a = alphaVal;
        GetComponent<SpriteRenderer>().color = color1;
        speed += alphaVal;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        var targetPos = playerTransform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero; //sätt efter takeDMG å ngn millisec
    }

    void DashAttack()
    {
        //if dist > x
        //get player pos
        //dash to it
        //waitforcoldown
        //loop
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "Player")
            return;

        if(obj.tag == "Bullet")
            takeDmg();
    }

    void takeDmg(float amount = 1)
    {
        currentHealth -= 1;
        GetComponent<AudioSource>().Play();
        if (currentHealth <= 0)
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(10 * startHealth);
            DestroyEnemy();
            return;
        }    
        else
        {
            float lerpVal = 1 - (currentHealth / startHealth);
            currentColor = Color.Lerp(color1, color2, lerpVal);
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        StartCoroutine(Blink(Color.white, 0.15f));
    }

    void DestroyEnemy()
    {
        Instantiate(explotion, transform.position, explotion.transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator Blink(Color color, float time)
    {
        Color tempColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = color;

        yield return new WaitForSeconds(time);

        GetComponent<SpriteRenderer>().color = tempColor;
    }
}
