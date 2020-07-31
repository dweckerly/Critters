using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInterface player;
    float xRotation = 0f;

    void LateUpdate()
    {
        Vector2 mouse = player.playerInput.mouse.MouseMove();
        float mouseX = mouse.x;
        float mouseY = mouse.y;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(Vector3.up, mouseX);
    }
}
