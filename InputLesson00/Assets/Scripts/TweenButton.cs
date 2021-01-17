using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TweenButton : MonoBehaviour
{
    private Vector3 startScale;
    EventSystem eventSystem;
    RectTransform rectTransform;
    Button button;
    bool hasScaled = false;

    public AudioClip selected;
    public AudioClip pressed;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        startScale = transform.localScale;
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        rectTransform = GetComponent<RectTransform>();
        button = rectTransform.GetComponent<Button>();
    }
    void Update()
    {
        if (eventSystem.currentSelectedGameObject == gameObject)
        {
            if (hasScaled == false)
            {

                Selected();
                hasScaled = true;
            }
        }
        else
            ReScale();
    }

    void Selected()
    {
        if (Time.timeSinceLevelLoad > 0.2f)
        {
            PlaySound(selected, 0.3f);
        }
        rectTransform.DOScale(startScale + (Vector3.one * 0.7f), 0.2f).SetUpdate(true);
    }
    void ReScale()
    {
        rectTransform.DOScale(startScale, 0.2f).SetUpdate(true);
        hasScaled = false;
    }

    Vector3 scaleBeforePress;
    public void Pressed()
    {
        PlaySound(pressed);
        scaleBeforePress = rectTransform.localScale;
        rectTransform.DOScale(scaleBeforePress + (Vector3.one * 0.6f), 0.12f).OnComplete(ReScaleAfterPress).SetUpdate(true);
    }
    void ReScaleAfterPress()
    {
        rectTransform.DOScale(scaleBeforePress, 0.12f).SetUpdate(true);
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    void PlaySound(AudioClip clip, float pitch)
    {
        audioSource.pitch = Random.Range(1f - pitch, 1f + pitch);
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.pitch = 1;
    }


}
