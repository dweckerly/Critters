using System.Collections.Generic;
using UnityEngine;

public class HeldItemsUIManager : MonoBehaviour
{
    public List<HeldItemUIObject> heldItemUIObjects;
    public Inventory inventory;

    public void PopulateHeldItemsPanel(List<HeldItem> heldItems)
    {
        for (int i = 0; i < heldItemUIObjects.Count; i++)
        {
            if(i < heldItems.Count)
            {
                heldItemUIObjects[i].itemImage.enabled = true;
                heldItemUIObjects[i].data = heldItems[i].data;
                heldItemUIObjects[i].itemImage.sprite = heldItemUIObjects[i].data.image;
                if (heldItems[i].data.singleUse)
                {
                    int amount = 0;
                    for (int j = 0; j < inventory.items.Count; j++)
                    {
                        if(inventory.items[j].data == heldItems[i].data)
                        {
                            amount += inventory.items[j].amount;
                        }
                    }
                    heldItemUIObjects[i].amountDisplay.text = amount.ToString();
                }
                else
                {
                    heldItemUIObjects[i].amountDisplay.text = "";
                }
            }
            else
            {
                heldItemUIObjects[i].data = null;
                heldItemUIObjects[i].itemImage.enabled = false;
                heldItemUIObjects[i].amountDisplay.text = "";
            }
        }
    }

    public void SetActiveItemDisplay(int index)
    {
        for (int i = 0; i < heldItemUIObjects.Count; i++)
        {
            if(i == index)
            {
                heldItemUIObjects[i].canvasGroup.alpha = 1;
            }
            else
            {
                heldItemUIObjects[i].canvasGroup.alpha = 0.5f;
            }
        }
    }

    public void DecrementItemAmount(InventoryItem item)
    {
        for (int i = 0; i < heldItemUIObjects.Count; i++)
        {
            if (heldItemUIObjects[i].data == item.data)
            {
                int amount = int.Parse(heldItemUIObjects[i].amountDisplay.text);
                amount--;
                heldItemUIObjects[i].amountDisplay.text = amount.ToString();
                return;
            }
        }
    }

    public void IncrementItemAmount(InventoryItem item)
    {
        for (int i = 0; i < heldItemUIObjects.Count; i++)
        {
            if (heldItemUIObjects[i].data == item.data)
            {
                int amount = int.Parse(heldItemUIObjects[i].amountDisplay.text);
                amount++;
                heldItemUIObjects[i].amountDisplay.text = amount.ToString();
                return;
            }
        }
    }
}
