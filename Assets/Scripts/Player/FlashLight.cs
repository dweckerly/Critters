using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public PlayerInterface playerInterface;
    public Light flashLight;

    void Update()
    {
        if(playerInterface.playerInput.FlashLightButton())
        {
            if(flashLight.enabled)
            {
                flashLight.enabled = false;
            }
            else
            {
                flashLight.enabled = true;
            }
        }
    }
}
