using UnityEngine;

public class Hand : HeldItem
{
    public Texture[] textures;
    public Renderer rend;

    protected override void OnEnable()
    {
        if (hia != null)
        {
            hia.clickCoolDown = 0.2f;
        }
        range = 2f;
    }

    public override void ClickAction()
    {
        rend.material.mainTexture = textures[1];
        PickUpItem();
        PetCritter();
    }

    void PickUpItem()
    {
        Item item = CheckForItem();
        if (item != null)
        {
            if(hia.player.inventory.AddItem(item))
            {
                hia.player.messageSystem.AddMessage("Picked up one " + item.data.itemName + ".");
                Destroy(item.gameObject);
            }
            else
            {
                hia.player.messageSystem.AddMessage("Inventory is full.");
            }
            
        }
    }

    void PetCritter()
    {
        Critter critter = CheckForCritter();
        if (critter != null)
        {
            hia.player.messageSystem.AddMessage("The wild " + critter.data.critterName + " doesn't like being touched...");
        }
    }

    public override void UnClickAction()
    {
        rend.material.mainTexture = textures[0];
    }
}
