using UnityEngine;

public class ItemDecay : MonoBehaviour
{
    float itemLife = 10f;
    float decayTime = 10f;
    float startTime;

    bool decaying;

    Item item;

    SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        startTime = Time.time;
        item = GetComponent<Item>();
        decaying = false;
    }

    private void Update()
    {
        if(decaying)
        {
            Decay();
            return;
        }
        if(Time.time > startTime + itemLife && !decaying)
        {
            decaying = true;
            startTime = Time.time;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Decay()
    {
        float alpha = 0;
        Color color = spriteRenderer.material.color;
        if ((startTime + decayTime) - Time.time > 0) 
        {
            alpha = 255 * (((startTime + decayTime) - Time.time) / decayTime);
        }
        color.a = alpha;
        spriteRenderer.material.color = color;
        if (Time.time > startTime + decayTime)
        {
            Destroy(item.gameObject);
        }
    }
}
