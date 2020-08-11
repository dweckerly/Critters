using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDecay : MonoBehaviour
{
    float itemLife = 10f;
    float decayTime = 10f;
    float startTime;

    bool decaying;

    Item item;

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
        }
    }

    void Decay()
    {
        float scaleFactor = 0;
        if ((startTime + decayTime) - Time.time > 0) 
        {
            scaleFactor = ((startTime + decayTime) - Time.time) / decayTime;
        }
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * scaleFactor, scale.y * scaleFactor, scale.z);
        if(Time.time > startTime + decayTime)
        {
            Destroy(item.gameObject);
        }
    }
}
