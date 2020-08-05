using System.Threading;
using UnityEngine;

public class DropItemConditionalFollow : ConditionalFollowBehaviour
{
    Item targetObject;
    int itemsNeeded = 5;

    public override void StartBehaviour()
    {
        targetObject = critter.data.dropItem.GetComponent<Item>();
    }

    public override bool FollowCondition()
    {
        PlayerInterface playerInterface = critter.target.GetComponent<PlayerInterface>();
        if (playerInterface != null)
        {
            if(playerInterface.inventory.inventory.items.Count > 0)
            {
                int count = 0;
                foreach (InventoryItem item in playerInterface.inventory.inventory.items)
                {
                    if (item.data == targetObject.data)
                    {
                        count += item.amount;
                    }
                    if (count >= itemsNeeded)
                    {
                        return true;
                    }
                }
            }
            
        }
        return false;
    }
}
