using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScript : MonoBehaviour
{
    static bool pausedGame;
    public GameObject ui;
    private EventSystem eventSystem;
    void Awake()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        ui = transform.Find("UI").gameObject;
        ResumeGame();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(pausedGame == false)
            {
                ui.SetActive(true);
                eventSystem.SetSelectedGameObject(ui.transform.Find("Resume").gameObject);
                pausedGame = true;
                Time.timeScale = 0f;
                return;
            }
            ResumeGame();
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausedGame = false;
        ui.SetActive(false);
    }
}
