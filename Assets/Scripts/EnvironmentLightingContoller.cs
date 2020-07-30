using UnityEngine;

public class EnvironmentLightingContoller : MonoBehaviour
{
    public Light mainLight;
    public Camera mainCamera;

    float mainLightMaxIntensity = 0.3f;

    float dayToNightTime = 300f;
    float lightRotationTime;
    float startLightRotationTime;
    float rotationTimeDif;
    float timeDif = 0f;
    float startTime;

    float maxLightRoation = 160f;
    float minLightRotation = 10f;

    bool day = true;

    private void Awake()
    {
        startTime = Time.time;
        lightRotationTime = dayToNightTime * 2;
        startLightRotationTime = dayToNightTime + Time.time;
    }

    private void LateUpdate()
    {
        rotationTimeDif = startLightRotationTime - Time.time;
        if(day)
        {
            timeDif = (startTime + dayToNightTime) - Time.time;
        }
        else
        {
            timeDif = Time.time - startTime;
        }
        
        if(timeDif > dayToNightTime)
        {
            timeDif = dayToNightTime;
            startTime = Time.time;
            day = true;
        }
        else if(timeDif < 0)
        {
            timeDif = 0;
            startTime = Time.time;
            day = false;
            startLightRotationTime = lightRotationTime + Time.time;
        }
        ChangeLighting();
    }

    void ChangeLighting()
    {
        Color lightingColor = new Color((timeDif / dayToNightTime), (timeDif / dayToNightTime), (timeDif / dayToNightTime), 1.0f);
        mainLight.intensity = (timeDif / dayToNightTime) * mainLightMaxIntensity;
        float lightRotationAmount = (maxLightRoation * (rotationTimeDif / lightRotationTime)) + minLightRotation; 
        if(lightRotationAmount < minLightRotation)
        {
            lightRotationAmount = minLightRotation;
        }
        else if(lightRotationAmount > maxLightRoation)
        {
            lightRotationAmount = maxLightRoation;
        }
        mainLight.transform.localEulerAngles = new Vector3(lightRotationAmount, 0, 0);
        RenderSettings.ambientLight = mainCamera.backgroundColor = lightingColor;
    }
}
