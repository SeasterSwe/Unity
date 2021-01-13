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
    void Awake()
    {
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
        rectTransform.DOScale(startScale + (Vector3.one * 0.7f), 0.2f);
    }
    void ReScale()
    {
        rectTransform.DOScale(startScale, 0.2f);
        hasScaled = false;
    }

    Vector3 scaleBeforePress;
    public void Pressed()
    {
        scaleBeforePress = rectTransform.localScale;
        rectTransform.DOScale(scaleBeforePress + (Vector3.one * 0.6f), 0.12f).OnComplete(ReScaleAfterPress);
    }
    void ReScaleAfterPress()
    {
        rectTransform.DOScale(scaleBeforePress, 0.12f);
    }


}
