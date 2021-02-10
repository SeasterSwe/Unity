using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CPSDisplayer : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ScoreManager.bought += UpdateCPSText;
    }

    public void UpdateCPSText()
    {
        string text = ($"CPS : {ScoreManager.CPS}");
        this.text.text = text;
    }
}
