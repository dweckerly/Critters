using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public PlayerInterface player;
    public Inventory inventory;

    public GameObject inventoryPanel;

    public Transform inventoryItemParent;
    public ItemUIObject inventoryItem;
    public ItemDetailsUIObject itemDetails;

    public Button useButton;
    public Button equipButton;
    public Button dropButton;

    InventoryItem selectedItem;

    int maxHeldItems = 4;

    void Update()
    {
        if (player.playerInput.BackPackButton()) 
        {
            if(inventoryPanel.activeSelf)
            {
                CloseInventoryPanel();
            }
            else
            {
                OpenInventoryPanel();
            }
        }
    }

    void OpenInventoryPanel()
    {
        PopulateInventory();
        inventoryPanel.SetActive(true);
        player.playerInput.mouse.DisableMouse();
        player.playerInput.DisablePlayerMovement();
        Cursor.lockState = CursorLockMode.None;
    }

    void CloseInventoryPanel()
    {
        player.playerInput.mouse.EnableMouse();
        player.playerInput.EnablePlayerMovement();
        Cursor.lockState = CursorLockMode.Locked;
        inventoryPanel.SetActive(false);
    }

    void ClearInventory()
    {
        foreach(Transform child in inventoryItemParent)
        {
            Destroy(child.gameObject);
        }
        itemDetails.itemName.text = "";
        itemDetails.itemImage.sprite = null;
        itemDetails.itemDescription.text = "";
        itemDetails.itemAmount.text = "";
        DisableItemButtons();
    }

    void PopulateInventory()
    {
        ClearInventory();
        for (int i = 0; i < inventory.items.Count; i++)
        {
            inventoryItem.inventoryManager = this;
            inventoryItem.itemImage.sprite = inventory.items[i].data.image;
            inventoryItem.index = i;
            Instantiate(inventoryItem, inventoryItemParent);
        }

        if (inventory.items.Count > 0)
        {
            if(selectedItem == null)
            {
                SetSelectedItem(0);
            }
            else
            {
                SetItemDetails();
                EnableItemButtons();
            }
        }
    }

    public void SetSelectedItem(int index)
    {
        selectedItem = inventory.items[index];
        SetItemDetails();
        EnableItemButtons();
    }

    public void SetItemDetails()
    {
        if(inventory.items.Contains(selectedItem))
        {
            itemDetails.itemName.text = selectedItem.data.itemName;
            itemDetails.itemImage.sprite = selectedItem.data.image;
            itemDetails.itemDescription.text = selectedItem.data.description;
            itemDetails.itemAmount.text = "x" + selectedItem.amount.ToString();
        }
    }

    void EnableItemButtons()
    {
        if(selectedItem != null)
        {
            Text equipText = equipButton.GetComponentInChildren<Text>();
            equipText.text = "EQUIP";

            if (selectedItem.itemEffect != null)
            {
                useButton.interactable = true;
            }
            else
            {
                useButton.interactable = false;
            }
            equipButton.interactable = true;
            if (IsItemEquipped(selectedItem) > -1)
            {
                equipText.text = "UNEQUIP";
            }
            dropButton.interactable = true;
        }
    }

    void DisableItemButtons()
    {
        useButton.interactable = false;
        equipButton.interactable = false;
        dropButton.interactable = false;
    }

    public bool AddItem(Item item)
    {
        if(inventory.AddItem(item))
        {
            InventoryItem iItem = new InventoryItem(item);
            if(iItem.data.singleUse)
            {
                player.playerHeldItems.heldItemsUIManager.IncrementItemAmount(iItem);
            }
            return true;
        }
        return false;
    }

    public void DropItem()
    {
        RemoveItem(selectedItem);
        GameObject itemGO = Instantiate(selectedItem.data.prefab, player.transform.position + player.transform.forward, selectedItem.data.prefab.transform.rotation);
        itemGO.gameObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * 100);

        if (ItemIsLastInInventory(selectedItem))
        {
            if (inventory.items.Count > 0)
            {
                SetSelectedItem(0);
            }
            else
            {
                selectedItem = null;
            }
        }
        else
        {
            SetSelectedItem(inventory.items.IndexOf(selectedItem));
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        inventory.RemoveItem(item);
        if(item.data.singleUse)
        {
            player.playerHeldItems.heldItemsUIManager.DecrementItemAmount(item);
        }
        int itemEquip = IsItemEquipped(item);
        if (itemEquip > -1 && ItemIsLastInInventory(item))
        {
            UnEquipItem(itemEquip);
        }
        PopulateInventory();
    }

    public void UseItem()
    {

    }

    public void EquipItem()
    {
        for (int i = 0; i < player.playerHeldItems.heldItems.Count; i++)
        {
            if(player.playerHeldItems.heldItems[i].data == selectedItem.data)
            {
                UnEquipItem(i);
                return;
            }
        }
        if (player.playerHeldItems.heldItems.Count > maxHeldItems)
        {
            player.messageSystem.AddMessage("You can't hold anything else...");
            return;
        }
        Text equipText = equipButton.GetComponentInChildren<Text>();
        equipText.text = "UNEQUIP";

        GameObject newEquip = Instantiate(selectedItem.data.prefab, player.playerHeldItems.gameObject.transform);
        player.playerHeldItems.heldItems.Add(newEquip.GetComponent<Item>());
        newEquip.GetComponent<Item>().hia = player.playerHeldItems;
        newEquip.SetActive(false);
        player.playerHeldItems.heldItemsUIManager.PopulateHeldItemsPanel(player.playerHeldItems.heldItems);
    }

    public void UnEquipItem(int itemIndex)
    {
        Text equipText = equipButton.GetComponentInChildren<Text>();
        equipText.text = "EQUIP";
        player.playerHeldItems.heldItems.Remove(player.playerHeldItems.heldItems[itemIndex]);
        if(player.playerHeldItems.gameObject.transform.GetChild(itemIndex).gameObject != null)
        {
            Destroy(player.playerHeldItems.gameObject.transform.GetChild(itemIndex).gameObject);
        }
        player.playerHeldItems.Initialize();
    }

    int IsItemEquipped(InventoryItem item)
    {
        for (int i = 0; i < player.playerHeldItems.heldItems.Count; i++)
        {
            if (player.playerHeldItems.heldItems[i].data == item.data)
            {
                return i;
            }
        }
        return -1;
    }

    public bool ItemIsLastInInventory(InventoryItem item)
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if(inventory.items[i].data == item.data)
            {
                return false;
            }
        }
        return true;
    }
}
