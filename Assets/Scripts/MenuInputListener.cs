using UnityEngine;
using UnityEngine.Events;

public class MenuInputListener : MonoBehaviour
{
    public PlayerInput playerInput;
    public UnityEvent inventoryUIEvent;
    public UnityEvent critterUIEvent;

    private void Update()
    {
        if (playerInput.BackPackButton())
        {
            inventoryUIEvent.Invoke();
        }

        if(playerInput.CritterButton())
        {
            critterUIEvent.Invoke();
        }
    }
}
