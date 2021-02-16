using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSquare : MonoBehaviour, IExplodeable
{
    Transform baseTarget;
    public float rotationSpeed = -90;

    void Start()
    {
        baseTarget = GameManager.Instance.bottomSquare.transform.Find("Target");
        transform.position = baseTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        //https://pastebin.com/nrc5FBPc   
    }

    public void SetPosition(Vector3 endPos)
    {
        //transform.position = position;
        spinning = MoveinArc(endPos);
        StartCoroutine(spinning);
    }
    public IEnumerator spinning;
    IEnumerator MoveinArc(Vector3 endPosr)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = endPosr;
        Vector3 center = (startPos + endPos) / 2;
        var parent = transform.parent;
        if (parent != null)
        {
            transform.SetParent(null);
            Destroy(parent.gameObject);
        }

        GameObject centerObj = new GameObject();
        centerObj.transform.position = center;
        transform.SetParent(centerObj.transform);

        bool spin = true;
        float rotationSpeed = this.rotationSpeed;
        float t = 0;

        if (startPos.x > endPos.x)
        {
            rotationSpeed *= -1;
        }

        while (spin)
        {
            t += Time.deltaTime;
            if (t >= Mathf.Abs((180 / rotationSpeed)))
                spin = false;

            centerObj.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.position = baseTarget.position;
        GameManager.Instance.bottomSquare.GetComponent<BottomSquare>().canMove = true;
        yield return null;
    }

    public GameObject explotion;
    public void Explode()
    {
        StopCoroutine(spinning);
        GameObject explotionClone = Instantiate(explotion, transform.position, explotion.transform.rotation);
        Destroy(explotionClone, 2f);
        Destroy(gameObject);
    }
}
