using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBag : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        Vector3 startPos = transform.position;
        Vector3 centerPos = GameObject.Find("Bottom").transform.Find("Bag").position + (Vector3.up * 2);
        float a = Random.Range(0, 1f) * Mathf.PI * 2;
        float r = 2 * Mathf.Sqrt(Random.Range(0, 1f));
        float x = r * Mathf.Cos(a);
        float y = r * Mathf.Sin(a);

        x += centerPos.x;
        y += centerPos.y;

        Vector3 pos = new Vector3(x, y, 0);
        float t = 0;
        float rSpeed = Random.Range(0.4f, 1.2f);
        while (t <= 1)
        {

            t += Time.deltaTime * rSpeed;
            transform.position = Vector3.Lerp(startPos, pos + (Vector3.right * Mathf.Cos(Time.time)) + (Vector3.up * Mathf.Sin(Time.time)), t);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(Move());

    }
}
