public class InventoryItem
{
    public ItemData data;
    public ItemEffect itemEffect;

    public int amount = 1;

    public InventoryItem(Item item)
    {
        data = item.data;
        itemEffect = item.itemEffect;
    }
}
