using UnityEngine;

public class MouseInput
{
    public float mouseSensitivity = 100f;

    public bool detectLeftClick = true;
    public bool detectLeftRelease = true;
    public bool detectRightClick = true;
    public bool detectScrollWheel = true;

    CameraController cc;

    public MouseInput(CameraController _cc)
    {
        cc = _cc;
    }

    public Vector2 MouseMove()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity * Time.deltaTime;
    }

    public bool LeftClick()
    {
        if (detectLeftClick)
        {
            return Input.GetMouseButtonDown(0);
        }
        return false;
    }

    public bool LeftClickRelease()
    {
        if (detectLeftRelease)
        {
            return Input.GetMouseButtonUp(0);
        }
        return false;
    }

    public bool RightClick()
    {
        if (detectLeftClick)
        {
            return Input.GetMouseButtonDown(1);
        }
        return false;
    }

    public float ScrollWheelInput()
    {
        if (detectScrollWheel)
        {
            return Input.GetAxis("Mouse ScrollWheel");
        }
        return 0f;
    }

    public void EnableCameraLook()
    {
        cc.enabled = true;
    }

    public void DisableCameraLook()
    {
        cc.enabled = false;
    }

    public void EnableMouse()
    {
        EnableCameraLook();
        EnableMouseClickAndScroll();
    }

    public void DisableMouse()
    {
        DisableCameraLook();
        DisableMouseClickAndScroll();
    }

    public void EnableMouseClickAndScroll()
    {
        detectLeftClick = true;
        detectLeftRelease = true;
        detectRightClick = true;
        detectScrollWheel = true;
    }

    public void DisableMouseClickAndScroll()
    {
        detectLeftClick = false;
        detectLeftRelease = false;
        detectRightClick = false;
        detectScrollWheel = false;
    }
}
