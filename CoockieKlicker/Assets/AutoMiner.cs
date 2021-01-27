using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class AutoInfo
{
    public float cost = 10;
    public int amount = 10;
}
public class AutoMiner : MonoBehaviour
{
    public ScriptableAuto auto;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI coockiesPSText;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI amountText;
    CoockieDisplayer coockieDisplayer;

    public Save save;
    AutoInfo autoInfo;
    private void Awake()
    {
        coockieDisplayer = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<CoockieDisplayer>();
        if (autoInfo == null)
        {
            autoInfo = new AutoInfo();
            autoInfo.amount = 0;
            autoInfo.cost = auto.startCost;
        }
    }
    private void Start()
    {
        autoInfo = save.Load(name);
        GetTexts();
        SetTexts();
        //autoInfo = save.Load(name);
    }

    void GetTexts()
    {
        costText = transform.Find("Cost").GetComponent<TextMeshProUGUI>();
        coockiesPSText = transform.Find("CoockiesPerSec").GetComponent<TextMeshProUGUI>();
        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        amountText = transform.Find("AmountText").GetComponent<TextMeshProUGUI>();
    }
    void SetTexts()
    {
        costText.text = ($"Cost:{Mathf.CeilToInt(autoInfo.cost)}");
        coockiesPSText.text = ($"CPS:{auto.cookiesPerSec}");
        nameText.text = auto.autoName;
        amountText.text = ($"N = {autoInfo.amount}");
    }

    public void Clicked()
    {
        if (canBuy())
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
        ScoreManager.AddCoockie(-autoInfo.cost);
        autoInfo.cost *= auto.costMutli;
        autoInfo.amount += 1;
    }

    void CantBuy()
    {
        Debug.Log("To Poor");
    }

    bool canBuy()
    {
        if (ScoreManager.coockies >= autoInfo.cost)
            return true;
        else
            return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            save.SaveInfos(autoInfo, name);
        if (Input.GetKeyDown(KeyCode.L))
        {
            autoInfo = save.Load(name);
            SetTexts();
        }
    }

}
