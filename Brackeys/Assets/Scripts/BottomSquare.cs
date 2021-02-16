using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSquare : MonoBehaviour, IExplodeable
{
    private Rigidbody2D rb;
    Vector3 target;
    public bool canMove;
    public float launchSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canMove)
            DrawLine(targetPos());

        if(Input.GetMouseButtonUp(0) && canMove)
        {
            GetComponent<LineRenderer>().positionCount = 0;
            StartCoroutine(Launch(targetPos()));
        }
    }

    void DrawLine(Vector3 endPos)
    {
        GetComponent<LineRenderer>().positionCount = 2;
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, endPos);
    }

    IEnumerator Launch(Vector3 target)
    {
        GameManager.Instance.MadeAMove();
        canMove = false;
        Vector3 startPos = transform.position;
        var launchDir = target - startPos;
        rb.velocity = launchDir.normalized * launchSpeed;
        //float t = 0;
        //while (t <= 1)
        //{
        //    t += Time.deltaTime * 0.5f;
        //    transform.position = Vector3.Lerp(startPos, target, t);
        //}
        yield return new WaitForEndOfFrame();
    }
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        GameManager.Instance.topSquare.GetComponent<TopSquare>().SetPosition(transform.Find("Target").position);
    }
    public void StopMovement(Transform fixedPos)
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(FixPosition(fixedPos.position));
    }

    Vector3 targetPos()
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    IEnumerator FixPosition(Vector3 target)
    {
        canMove = false;
        Vector3 startPos = transform.position;
        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime  *2f;
            transform.position = Vector3.Lerp(startPos, target, t);
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.topSquare.GetComponent<TopSquare>().SetPosition(transform.Find("Target").position);
    }
    public GameObject explotion;
    public void Explode()
    {
        GameObject explotionClone = Instantiate(explotion, transform.position, explotion.transform.rotation);
        Destroy(explotionClone, 2f);
        Destroy(gameObject);
    }
}
