using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    public void Blink(GameObject obj, Color color, int amountOfTimes, float delay)
    {
        StartCoroutine(BlinkEffekt(obj, color, amountOfTimes, delay));
    }
    IEnumerator BlinkEffekt(GameObject obj, Color color, int amountOfTimes, float delay)
    {
        SpriteRenderer rend = obj.GetComponent<SpriteRenderer>();
        Color ogColor = rend.color;
        float blinkDuration = delay;
        if (amountOfTimes > 1)
            blinkDuration = delay / 2;

        for (int i = 0; i < amountOfTimes; i++)
        {
            rend.color = color;
            yield return new WaitForSeconds(blinkDuration);
            rend.color = ogColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
