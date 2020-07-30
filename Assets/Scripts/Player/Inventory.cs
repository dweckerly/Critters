using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<InventoryItem> items = new List<InventoryItem>();

    public bool AddItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].data == item.data)
            {
                items[i].amount++;
                return true;
            }
        }
        AddNewItem(item);
        return true;
    }

    void AddNewItem(Item item)
    {
        InventoryItem iItem = new InventoryItem(item);
        items.Add(iItem);
    }

    public void RemoveItem(InventoryItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].data == item.data)
            {
                if(items[i].amount > 1)
                {
                    items[i].amount--;
                    return;
                }
                else
                {
                    items.Remove(items[i]);
                    return;
                }
            }
        }
    }
}
