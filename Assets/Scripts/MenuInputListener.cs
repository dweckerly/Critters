using UnityEngine;
using UnityEngine.Events;

public class MenuInputListener : MonoBehaviour
{
    public PlayerInput playerInput;
    public UnityEvent inventoryUIOpenEvent;
    public UnityEvent critterUIOpenEvent;
    public UnityEvent inventoryUICloseEvent;
    public UnityEvent critterUICloseEvent;

    private void Update()
    {
        if (playerInput.BackPackButton())
        {
            critterUICloseEvent.Invoke();
            inventoryUIOpenEvent.Invoke();
        }

        if(playerInput.CritterButton())
        {
            inventoryUICloseEvent.Invoke();
            critterUIOpenEvent.Invoke();
        }
    }
}
