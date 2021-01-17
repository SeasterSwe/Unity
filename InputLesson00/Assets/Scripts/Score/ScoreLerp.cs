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
    public GameObject highScoreList;
    public GameObject scoreParent;
    public GameObject cornerPos;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        StartCoroutine(LerpScore(ScoreManager.score, effectSpeed));
        
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

        //?!?!?!?!?!?!?!?!?!?!?!?!?!?!?!?!?!??!?!?!?!?!?!?!?!?!?!?!?!?!?!??!!?!??!!?!?!?!?!?!?!?!?!?
        rectTransform.DOMove(cornerPos.transform.position, 0.5f).OnComplete(ActivateScoreList);
        rectTransform.DOScale(Vector3.one * 2.8f, 0.5f);
    }
    void ActivateScoreList()
    {
        scoreParent.SetActive(true);
        highScoreList.SetActive(true);
    }
}
