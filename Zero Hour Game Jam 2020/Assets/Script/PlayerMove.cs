using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10;
    public bool canMove = true;
    public GameObject jumpParticle;
    public GameObject hitWallParticle;
    public AudioClip swoshSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            Instantiate(jumpParticle, transform.position, jumpParticle.transform.rotation);
            canMove = false;
            speed *= -1;
            PlayPlayerSound(swoshSound);
        }

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -8.073f, 8.073f);
        transform.position = pos;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            canMove = true;
            Instantiate(hitWallParticle, transform.position, hitWallParticle.transform.rotation);
        }
    }
    public void PlayPlayerSound(AudioClip clip, bool pitchSound = true, float volume = 0.13f)
    {
        audioSource.clip = clip;
        if (pitchSound)
            PitchSound.pitchSound(gameObject, 0.8f, 1.6f);

        audioSource.volume = volume;
        audioSource.Play();
    }

}
