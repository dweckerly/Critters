using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealBehaviour : InteractBehaviour
{
    bool stoleItem;

    public override bool BehaviourTrigger(Critter _critter)
    {
        PlayerInterface playerInterface = _critter.target.GetComponent<PlayerInterface>();
        if (playerInterface != null)
        {
            InventoryManager inv = playerInterface.inventory;
            if (inv != null && inv.inventory.items.Count > 0)
            {
                return true;
            }
        }
        return false;
    }

    public override void DoBehaviour()
    {
        if(!stoleItem)
        {
            base.DoBehaviour();
            if (critter.IsWithinInteractDistance())
            {
                PlayerInterface playerInterface = critter.target.GetComponent<PlayerInterface>();
                if (playerInterface != null)
                {
                    StealItem(playerInterface);
                }
            }
        }
        else
        {
            critter.critterController.MoveAwayFromTarget();
        }
    }

    void StealItem(PlayerInterface pi)
    {
        InventoryManager inv = pi.inventory;
        if (inv != null && inv.inventory.items.Count > 0)
        {
            int randomItem = Random.Range(0, inv.inventory.items.Count);
            InventoryItem stealItem = inv.inventory.items[randomItem];
            critter.SetDropItem(stealItem.data.prefab);
            inv.RemoveItem(stealItem);
            stoleItem = true;
            pi.messageSystem.AddMessage("You backpack feels lighter...");
        }
    }
}
