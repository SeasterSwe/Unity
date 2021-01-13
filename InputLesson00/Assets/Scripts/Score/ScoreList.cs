using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour
{
    List<float> topScores = new List<float>();
    public int amountOfSavedScores = 5;
    public GameObject parentObj;
    public TextMeshProUGUI scoreText;
    public Color[] textColors;
    public GameObject canvas;
    public float tweenDelay = 1f;

    void Start()
    {
        if(canvas == null)
            canvas = GameObject.Find("Canvas");

        if (!PlayerPrefs.HasKey("Score0"))
        {
            SetScoresToZero();
        }
        LoadScores();
        CheckIfHighScore(ScoreManager.score);
        StartCoroutine(DisplayScores());
    }

    void LoadScores()
    {
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            topScores.Insert(i, PlayerPrefs.GetFloat("Score" + i.ToString()));
        }
        topScores.Sort();
    }

    void SetScoresToZero()
    {
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            PlayerPrefs.SetFloat("Score" + i.ToString(), 0);
        }
    }

    void SetAndSavePrefs()
    {
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            PlayerPrefs.SetFloat("Score" + i.ToString(), topScores[i]);
        }
        PlayerPrefs.Save();
    }

    IEnumerator DisplayScores()
    {
        float xValOutOfScreen = -canvas.GetComponent<RectTransform>().position.x;
        xValOutOfScreen -= 345;//width på text
        Vector3 spawnPos = new Vector3(xValOutOfScreen, parentObj.transform.position.y, parentObj.transform.position.z);
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            TextMeshProUGUI score = Instantiate(scoreText, spawnPos - (Vector3.up * (i + 1) * 100), scoreText.rectTransform.rotation);
            score.name = "score" + i.ToString();
            score.gameObject.transform.SetParent(parentObj.transform);

            int n = (amountOfSavedScores - i - 1);
            string text = (i + 1).ToString() + "TH    " + topScores[n].ToString();
            score.text = text;
            score.color = textColors[i];
            score.rectTransform.DOMoveX(parentObj.transform.position.x, 0.3f);

            yield return new WaitForSeconds(0.4f);
        }
        StartCoroutine(TweenTheScoreTexts());
    }

    IEnumerator TweenTheScoreTexts()
    {
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            var scoreObj = parentObj.transform.GetChild(i).transform;
            scoreObj.DOScale(scoreObj.localScale + (Vector3.one * 0.2f), 0.2f);
            yield return new WaitForSeconds(0.2f);
            scoreObj.DOScale((Vector3.one), 0.2f);
        }
    }

    public void CheckIfHighScore(float scoreCheck)
    {
        if (scoreCheck >= topScores[0])
        {
            topScores.Remove(topScores[0]);
            topScores.Add(scoreCheck);
            topScores.Sort();
            SetAndSavePrefs();
        }
    }
}
