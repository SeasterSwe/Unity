using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    static float score;
    float smoothScore;
    float smoothVelocity = 0.3f;
    void Start()
    {
        score = 0;
    }
    private void Update()
    {
        smoothScore = Mathf.SmoothDamp(smoothScore, (float)score, ref smoothVelocity, 0.15f, Mathf.Infinity, Time.deltaTime);
        int displayInt = (int)Mathf.Round(smoothScore);
        if (displayInt < score + 1)
        {
            scoreText.text = displayInt.ToString();
        }
    }

    public static void AddScore(float amount = 10)
    {
        score += amount;
    }

    public static float ReturnCurrentScore()
    {
        return score;
    }

}
