using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    public CharacterController controller;
    
    public float walkSpeed = 8f;
    public float sneakSpeed = 2f;

    public float currentSpeed;

    float gravity = 18f;

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        Vector2 input = playerInput.WASDInput();

        Vector3 move = transform.right * input.x + transform.forward * input.y;
        move.y -= gravity;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }
}
