using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBase : MonoBehaviour
{
    public float rotationSpeed = 15;
    public float startHealth = 20;
    float currentHealth;
    private Color ogColor;
    public GameObject smalRock;
    public GameObject explotionSound;
    bool canTakeDmg = true;
    private void Start()
    {
        currentHealth = startHealth;
        float r = Random.Range(0f, 1f);
        if (r <= 0.5f)
            rotationSpeed *= -1;

        ogColor = GetComponent<SpriteRenderer>().color;
        //random color
        //random size
    }
    void Update()
    {
        RotateStone();
    }

    void RotateStone()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var objTag = collision.gameObject.tag;
        if(objTag == "Bullet")
        {
            TakeDmg();
            StartCoroutine(Blink(Color.white, 0.15f));
        }
    }

    void TakeDmg()
    {
        if (!canTakeDmg)
            return;

        canTakeDmg = false;
        currentHealth -= 1;
        if (currentHealth <= 0)
            DestroyStone();
    }


    void DestroyStone()
    {
        Split();
        GameObject explotion = Instantiate(explotionSound, transform.position, explotionSound.transform.rotation);
        Destroy(explotion, 1f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StoneSpawner>().RemoveStone();
        Destroy(gameObject);
    }
    void Split()
    {
        int n = 12;
        float spreadAngle = (360f / n);

        for (int i = 0; i < n; i++)
        {
            float startAngle = (n - 1) * spreadAngle / 2;
            GameObject splittedRock = Instantiate(smalRock, transform.position, 
                transform.rotation * Quaternion.Euler(0, 0, -startAngle + spreadAngle * i));
            if (splittedRock.GetComponent<SmallRock>() != null)
            {
                SmallRock rock = splittedRock.GetComponent<SmallRock>();
                rock.speed = 10f;
                rock.size = (transform.localScale.x / 4f);
                Destroy(splittedRock, 8f);
            }
        }
    }

    IEnumerator Blink(Color color, float time)
    {     
        GetComponent<SpriteRenderer>().color = color;

        yield return new WaitForSeconds(time);

        GetComponent<SpriteRenderer>().color = ogColor;
        canTakeDmg = true;
    }
}
