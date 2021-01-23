using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHealth = 2;
    public float currentHealth;
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    Color originalColors;
    public Color blinkColor;
    public float blinkTime = 0.15f;
    private bool isBlinking = false;
    private void Start()
    {
        if (blinkColor == new Color())
            blinkColor = Color.white;

        currentHealth = startHealth;
        
        foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            sprites.Add(child);

        sprites.Add(GetComponent<SpriteRenderer>());
        originalColors = sprites[0].color;
    }
    public bool TakeDmgAndCheckIfAlive(float amount = 1)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            return false;

        if(isBlinking == false && gameObject != null)
            StartCoroutine(Blink(blinkColor, blinkTime));

        return true;
    }
    public IEnumerator Blink(Color color, float time)
    {
        isBlinking = true;
        foreach(SpriteRenderer sprite in sprites)
            sprite.color = color;

        yield return new WaitForSeconds(time);

        foreach (SpriteRenderer sprite in sprites)
            sprite.color = originalColors;

        isBlinking = false;
    }
}
