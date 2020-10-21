using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public float score;
    float time;
    float smoothScore;
    float smoothVelocity;

    private float[] highScores = new float[6];
    TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Score: ";
    }
    public void AddScore(float amount)
    {
        score += amount;
    }
    private void Update()
    {
        smoothScore = Mathf.SmoothDamp(smoothScore, (float)score, ref smoothVelocity, 0.15f, Mathf.Infinity, Time.deltaTime);
        int displayInt = (int)Mathf.Round(smoothScore);
        if (displayInt < score + 1)
        {
            text.text = "Score: " + displayInt;
        }
    }
}
