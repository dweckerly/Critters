using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerInput playerInput;
    public MessageSystem messageSystem;
    public HeldItemAction playerHeldItems;
    public InventoryManager inventory;
    public PlayerTeam playerTeam;

    public bool isSneaking = false;
}
