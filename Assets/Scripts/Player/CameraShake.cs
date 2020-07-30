using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera cam;
    Vector3 originalPos;
    Vector3 shakePos;

    float startShakeTime = 0f;
    float shakeTime = 0f;
    float minShakeTime = 0.3f;
    float maxShakeTime = 0.6f;

    float lerpSpeed = 0.1f;

    public float shakeAmount = 0.25f;

    private void OnEnable()
    {
        originalPos = cam.transform.localPosition;
    }

    public void Shake()
    {
        if(Time.time > startShakeTime + shakeTime)
        {
            shakePos = originalPos + Random.insideUnitSphere * shakeAmount;
            startShakeTime = Time.time;
            shakeTime = Random.Range(minShakeTime, maxShakeTime);
        }
        else
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, shakePos, lerpSpeed);
        }
    }

    private void OnDisable()
    {
        cam.transform.localPosition = originalPos;
    }
}
