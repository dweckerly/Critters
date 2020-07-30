using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneak : MonoBehaviour
{
    public PlayerInterface player;
    public Camera cam;
    float camAdjustSpeed = 4f;

    void Update()
    {
        if (player.playerInput.SneakInput())
        {
            if(!player.isSneaking)
            {
                player.isSneaking = true;
                player.playerMovement.currentSpeed = player.playerMovement.sneakSpeed;

            }
            if(cam.transform.localPosition.y > 0f)
            {
                cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, 0f, cam.transform.localPosition.z), camAdjustSpeed * Time.deltaTime);
                if (cam.transform.localPosition.y < 0f)
                {
                    cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, 0f, cam.transform.localPosition.z);
                }
            }
        }
        else if (cam.transform.localPosition.y < 0.5f)
        {
            if (player.isSneaking)
            {
                player.isSneaking = false;
                player.playerMovement.currentSpeed = player.playerMovement.walkSpeed;
            }
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, 0.5f, cam.transform.localPosition.z), camAdjustSpeed * Time.deltaTime);
        }
    }
}
