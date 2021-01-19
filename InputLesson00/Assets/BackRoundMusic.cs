using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRoundMusic : MonoBehaviour
{
    private static BackRoundMusic instance;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
