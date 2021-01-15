using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreLerp : MonoBehaviour
{
    float targetScore = 200;
    float displayScore = 0f;
    private TextMeshProUGUI text;
    RectTransform rectTransform;
    public float endScale = 1.5f;
    public float startScale = 0.5f;
    public float effectSpeed;
    Canvas canvas;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        StartCoroutine(LerpScore(targetScore, effectSpeed));
        
    }
    IEnumerator LerpScore(float targetScore, float speed)
    {
        float totalTime = 1f / speed;
        float lerpVal = 0f;
        rectTransform.localScale = Vector3.one * startScale;
        rectTransform.DOScale(Vector3.one * endScale, totalTime + speed);
        while (lerpVal <= 1f)
        {
            lerpVal += Time.deltaTime * speed;

            displayScore = Mathf.Lerp(0, targetScore, lerpVal);
            text.text = Mathf.RoundToInt(displayScore).ToString();
            yield return null;
        }

        float xValOutOfScreen = canvas.GetComponent<RectTransform>().position.x;
        xValOutOfScreen = xValOutOfScreen + 1300;//width på text
        Vector3 spawnPos = new Vector3(xValOutOfScreen, rectTransform.position.y, rectTransform.transform.position.z);
        rectTransform.DOMove(spawnPos, 0.5f);
    }
}
