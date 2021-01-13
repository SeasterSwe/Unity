using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleUpAndDown : MonoBehaviour
{
    public float scaleVal;
    public float scaleSpeed;
    Vector3 startScale;
    void Start()
    {
        startScale = transform.localScale;
        ScaleUp();
    }

    void ScaleUp()
    {
        transform.DOScale(startScale + (Vector3.one * scaleVal), scaleSpeed).OnComplete(ScaleDown);
    }
    void ScaleDown()
    {
        transform.DOScale(startScale, scaleSpeed).OnComplete(ScaleUp);
    }
}

