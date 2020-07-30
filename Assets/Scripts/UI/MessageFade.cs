using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageFade : MonoBehaviour
{
    float fadeDelay = 2f;
    float fadeTime = 0.1f;
    float alphaDecrement = 0.01f;
    CanvasGroup cg;
    
    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        StartCoroutine(FadeStart());
    }

    IEnumerator FadeStart()
    {
        yield return new WaitForSeconds(fadeDelay);
        StartCoroutine(Fade());

    }

    IEnumerator Fade()
    {
        while(cg.alpha > 0)
        {
            cg.alpha -= alphaDecrement;
            yield return new WaitForSeconds(fadeTime);
        }
    }
}
