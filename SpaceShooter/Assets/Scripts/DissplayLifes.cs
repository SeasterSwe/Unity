using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissplayLifes : MonoBehaviour
{
    public GameObject image;
    PlayerShooting ps;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
    }

}
