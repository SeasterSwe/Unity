using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoockieDisplayer : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    float smoothScore;
    float smoothVelocity = 0.3f;
    private void Start()
    {
        scoreText = scoreText ?? GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        smoothScore = Mathf.SmoothDamp(smoothScore, ScoreManager.coockies, ref smoothVelocity, 0.15f, Mathf.Infinity, Time.deltaTime);
        int displayInt = (int)Mathf.Round(smoothScore);
        if (displayInt < ScoreManager.coockies + 1)
        {
            scoreText.text = displayInt.ToString();
        }
    }

    public void UpdateText()
    {
        scoreText.text = Mathf.Round(ScoreManager.coockies).ToString();
    }
}
