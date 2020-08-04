using UnityEngine;

public class Hand : MeleeItem
{
    public Texture[] textures;
    public Renderer rend;

    protected override void Initialize()
    {
        if (hia != null)
        {
            hia.clickCoolDown = 0.2f;
        }
    }

    public override void LeftClickAction()
    {
        rend.material.mainTexture = textures[1];
        PickUpItem();
        PetCritter();
    }

    public override void UnClickAction()
    {
        rend.material.mainTexture = textures[0];
    }

    public override void RightClickAction()
    {
        return;
    }

    void PickUpItem()
    {
        Item item = CheckForItem();
        if (item != null)
        {
            Debug.Log(item.gameObject.name);
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
}
