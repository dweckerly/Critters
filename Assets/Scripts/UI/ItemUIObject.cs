using UnityEngine;
using UnityEngine.UI;

public class ItemUIObject : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Image itemImage;
    public int index;

    public void SelectThisItem()
    {
        inventoryManager.SetSelectedItem(index);
    }
}
