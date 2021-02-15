using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
            bottomSquare = GameObject.Find("Square").gameObject;
        if (topSquare == null)
            topSquare = GameObject.Find("Square (1)").gameObject;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    void Reload()
    {

    }

}
