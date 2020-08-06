using UnityEngine;

[System.Serializable]
public abstract class ItemEffect : MonoBehaviour
{
    public Item item;

    public virtual void Use()
    {
        InventoryItem iItem = new InventoryItem(item);
        item.hia.player.inventory.RemoveItem(iItem);
        if(item.hia.player.inventory.IsItemEquipped(iItem) > -1)
        {
            if (!item.hia.player.inventory.ItemIsLastInInventory(iItem))
            {
                item.hia.SwapItem(0);
            }
            if (item.hia.player.inventory.ItemIsLastInInventory(iItem) && item.hia.player.inventory.inventory.items.Count > 0)
            {
                item.hia.player.inventory.SetSelectedItem(0);
            }
        }
        
    }
}
