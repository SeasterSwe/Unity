using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutoMiner", menuName = "AutoMiner")]
public class ScriptableAuto : ScriptableObject
{
    public string autoName;
    public Sprite autoImage;
    public float startCost;
    public float costMutli;
    public float cookiesPerSec;
}
