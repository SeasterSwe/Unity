using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerShooting : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject cannonPoint;
    public GameObject bullet;
    public GameObject muzzleFlash;

    private float timeToNextBullet = 0;
    [Range(0f, 1f)]
    public float fireRate = 1;
    [Range(1, 36)]
    public int amountOfBullets = 1;
    [Range(0f, 360f)]
    public float angleToSpread = 360;

    [SerializeField]
    private bool autoMatedSpread;
    private ScoreManager scoreManger;
    float scoreToUnlockBullet = 200;
    public Vector3 punchScale;
    public float punchSpeed = 0.15f;

    public AudioClip takeDmg;
    public AudioClip death;
    public AudioClip upgrade;
    private AudioSource audioSource;
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    Color originalColors;
    public Color powerColor;
    public Color deathColor;
    private void Start()
    {
        ScoreManager.score = 0;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            sprites.Add(child);

        sprites.Add(GetComponent<SpriteRenderer>());
        originalColors = sprites[0].color;
    }
    void Update()
    {
        if (Time.time > timeToNextBullet && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            PunchScale();
            Instantiate(muzzleFlash, cannonPoint.transform.position, muzzleFlash.transform.rotation);
            MutliShoot(amountOfBullets, angleToSpread);
            timeToNextBullet = Time.time + fireRate;
            FireKnockBack();
        }
        if (ScoreManager.score >= scoreToUnlockBullet)
        {
            PlaySound(upgrade);
            StartCoroutine(FadeSprite(0.3f, 6, powerColor));
            amountOfBullets += 1;
            scoreToUnlockBullet *= 2;
        }
    }

    void MutliShoot(int n, float angleToDivide)
    {
        if (autoMatedSpread)
            angleToDivide = n * angleToSpread;

        float spreadAngle = angleToDivide / n;
        for (int i = 0; i < n; i++)
        {
            float startAngle = (n - 1) * spreadAngle / 2;
            GameObject clone =
            Instantiate(bullet, cannonPoint.transform.position, cannonPoint.transform.rotation * Quaternion.Euler(0, 0, -startAngle + spreadAngle * i));
            clone.transform.DOPunchScale(bullet.transform.localScale + (Vector3.one * 0.002f), 0.1f);
        }
    }
    void FireKnockBack()
    {
        rb.AddForce(rb.transform.up * (-1000 * fireRate), ForceMode2D.Force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Asteroid" && collision.gameObject.tag != "Enemy")
            return;

        amountOfBullets -= 1;
        if (amountOfBullets <= 0)
        {
            StartCoroutine(FadeSprite(0.3f, 10, deathColor));
            PlaySound(death);
            StartCoroutine(Death());
        }
        else
        {
            PlaySound(takeDmg);
            StartCoroutine(FadeSprite(0.3f, 4, deathColor));
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BaseEnemy>().DestroyEnemy();
        }
    }
    void PunchScale()
    {
        transform.Find("Cannon").DOPunchScale(punchScale, punchSpeed);
    }

    IEnumerator Death()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyManager>().enabled = false;
        StartCoroutine(Spin());
        yield return new WaitForEndOfFrame();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            e.GetComponent<BaseEnemy>().DestroyEnemy();
            yield return new WaitForSeconds(0.2f);
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSceneManager>().LoadSceneWithTransition("End");
        yield return new WaitForSeconds(0.8f);
        spin = false;
    }
    private bool spin;
    IEnumerator Spin()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        GetComponent<PlayerMove>().enabled = false;
        rb.AddForce(dir.normalized * 4, ForceMode2D.Impulse);
        float z = transform.rotation.z;
        z += 360;
        spin = true;
        while (spin)
        {
            z += 10;
            transform.Rotate(Vector3.forward * z * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator FadeSprite(float delay, int amount, Color color)
    {
        float t = (delay / 2f);
        for (int i = 0; i < amount; i++)
        {
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = color;

            yield return new WaitForSeconds(t);
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = originalColors;

            yield return new WaitForSeconds(t);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
