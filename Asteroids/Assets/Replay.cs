using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Replay : MonoBehaviour
{
    public TextMeshProUGUI gameover;
    void Start()
    {
        gameover.gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
