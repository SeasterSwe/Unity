using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI moveCounter;
    private int counter;

    public GameObject bottomSquare;
    public GameObject topSquare;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        if (bottomSquare == null)
            bottomSquare = GameObject.Find("Bottom").gameObject;
        if (topSquare == null)
            topSquare = GameObject.Find("Top").gameObject;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            Reload();
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {

    }

    public void MadeAMove()
    {
        counter += 1;
        moveCounter.text = ($"Move Counter : {counter}");
    }

}
