using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSquare : MonoBehaviour
{
    Vector3 target;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
           StartCoroutine(Launch(targetPos()));
    }

    IEnumerator Launch(Vector3 target)
    {
        canMove = false;
        Vector3 startPos = transform.position;
        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime * 0.5f;
            transform.position = Vector3.Lerp(startPos, target, t);
            yield return new WaitForEndOfFrame();
        }
        transform.position = target;
        GameManager.Instance.topSquare.GetComponent<TopSquare>().SetPosition(transform.Find("Target").position);
        canMove = true;
    }

    Vector3 targetPos()
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }
}
