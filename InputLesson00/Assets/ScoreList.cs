using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour
{
    public List<float> topScores = new List<float>();
    public int amountOfSavedScores = 5;
    public GameObject parentObj;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        if (topScores.Count == 0)
            SetScores();

        LoadScores();
        DisplayScores();
    }

    void LoadScores()
    {
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            topScores.Insert(i, PlayerPrefs.GetFloat("Score" + i.ToString()));
        }
        topScores.Sort();
    }

    void SetScores()
    {
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            PlayerPrefs.SetFloat("Score" + i.ToString(), Random.Range(0,100));
        }
    }

    void DisplayScores()
    {
        for (int i = 0; i < amountOfSavedScores; i++)
        {
            TextMeshProUGUI score = Instantiate(scoreText, parentObj.transform.position - (Vector3.up * i * 100), scoreText.rectTransform.rotation);
            score.name = "score" + i.ToString();
            score.gameObject.transform.SetParent(parentObj.transform);
            score.text = topScores[amountOfSavedScores - i - 1].ToString();
        }
    }
}
