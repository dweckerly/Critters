using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraController cameraController;

    public bool detectNumericInput = true;

    public MouseInput mouse = new MouseInput();

    private KeyCode[] heldItemKeyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5
    };

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector2 WASDInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public bool SneakInput()
    {
        return Input.GetButton("Sneak");
    }

    public bool PlayerInfoButton()
    {
        return Input.GetKeyUp(KeyCode.X);
    }

    public bool CritterButton()
    {
        return Input.GetKeyUp(KeyCode.C);
    }

    public bool BackPackButton()
    {
        return Input.GetKeyUp(KeyCode.B);
    }

    public bool MapButton()
    {
        return Input.GetKeyUp(KeyCode.M);
    }

    public bool JournalButton()
    {
        return Input.GetKeyUp(KeyCode.J);
    }

    public int HeldItemKeyPressed()
    {
        if(detectNumericInput)
        {
            for (int i = 0; i < heldItemKeyCodes.Length; i++)
            {
                if (Input.GetKeyDown(heldItemKeyCodes[i]))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public void EnablePlayerMovement()
    {
        playerMovement.enabled = true;
    }

    public void EnableCameraLook()
    {
        cameraController.enabled = true;
    }

    public void DisablePlayerMovement()
    {
        playerMovement.enabled = false;
    }

    public void DisableCameraLook()
    {
        cameraController.enabled = false;
    }
}