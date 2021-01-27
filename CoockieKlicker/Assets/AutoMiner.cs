using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoMiner : MonoBehaviour
{
    public ScriptableAuto auto;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI coockiesPSText;
    private TextMeshProUGUI nameText;
    private float cost;
    CoockieDisplayer coockieDisplayer;
    private void Awake()
    {
        coockieDisplayer = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<CoockieDisplayer>();
    }
    private void Start()
    {
        cost = auto.startCost;
        GetTexts();
        SetTexts();
    }

    void GetTexts()
    {
        costText = transform.Find("Cost").GetComponent<TextMeshProUGUI>();
        coockiesPSText = transform.Find("CoockiesPerSec").GetComponent<TextMeshProUGUI>();
        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    }
    void SetTexts()
    {
        costText.text = ($"Cost:{cost}");
        coockiesPSText.text = ($"CPS:{auto.cookiesPerSec}");
        nameText.text = auto.autoName;
    }

    public void Clicked()
    {
        if(canBuy())
        {
            Buy();
            SetTexts();
            coockieDisplayer.UpdateText();
        }
        else
        {
            CantBuy();
        }
    }

    void Buy()
    {
        ScoreManager.AddCPS(auto.cookiesPerSec);
        ScoreManager.AddCoockie(-cost);
        cost *= auto.costMutli;
    }

    void CantBuy()
    {
        Debug.Log("To Poor");
    }

    bool canBuy()
    {
        if (ScoreManager.coockies > cost)
            return true;
        else
            return false;
    }
}
