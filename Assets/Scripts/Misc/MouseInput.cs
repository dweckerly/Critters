using UnityEngine;

public class MouseInput
{
    public float mouseSensitivity = 100f;

    public bool detectLeftClick = true;
    public bool detectLeftRelease = true;
    public bool detectRightClick = true;
    public bool detectScrollWheel = true;

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

    public void EnableMouse()
    {
        detectLeftClick = true;
        detectLeftRelease = true;
        detectRightClick = true;
        detectScrollWheel = true;
    }

    public void DisableMouse()
    {
        detectLeftClick = false;
        detectLeftRelease = false;
        detectRightClick = false;
        detectScrollWheel = false;
    }
}
