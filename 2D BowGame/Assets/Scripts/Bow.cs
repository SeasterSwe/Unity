using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public float launchForce;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    [Range(0f,1f)]
    public float spaceBetweenPoints;
    Vector2 direction;

    public AudioClip shootSound;
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }
    void Update()
    {
        Vector2 bowPos = transform.position;
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - bowPos;
        transform.right = direction;

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }

    void Shoot()
    {
        GetComponentInParent<PlayerSoundManager>().PlayPlayerSound(shootSound);
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        if (launchForce >= 10)
            newArrow.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t*t); //formelSak
        return position;
    }
}
