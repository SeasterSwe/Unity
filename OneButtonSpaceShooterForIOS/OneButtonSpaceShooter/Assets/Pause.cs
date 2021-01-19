using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameObject pasueUI;
    public int touches = 1;
    private void Start()
    {
        pasueUI = GameObject.FindGameObjectWithTag("Safezone");
        pasueUI.SetActive(false);
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.touchCount > touches)
            PauseGame();
    }

    public void PauseGame()
    {
        pasueUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pasueUI.SetActive(false);
    }
}
