using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRoundMusicLoop : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] songs;
    float songDuration;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomSong());
    }
    IEnumerator PlayRandomSong()
    {
        int r = Random.Range(0, songs.Length);

        audioSource.clip = songs[r];
        songDuration = audioSource.clip.length;
        audioSource.Play();

        yield return new WaitForSeconds(songDuration);
        StartCoroutine(PlayRandomSong());
    }
}
