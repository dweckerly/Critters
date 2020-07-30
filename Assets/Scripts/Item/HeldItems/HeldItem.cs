using System;
using UnityEngine;

public abstract class HeldItem : Item
{
    public Animator animator;
    public AnimationClip swapOut;
    public AnimationClip swapIn;

    public float range;

    public HeldItemAction hia;

    public abstract void ClickAction();
    public abstract void UnClickAction();

    protected abstract void OnEnable();

    public float SwapOut()
    {
        animator.SetTrigger("swapOut");
        return swapOut.length;
    }

    public float SwapIn()
    {
        return swapIn.length;
    }

    public Critter CheckForCritter()
    {
        return (Critter) CheckForHit(typeof(Critter));
    }

    public Item CheckForItem()
    {
        return (Item) CheckForHit(typeof(Item)); ;
    }

    Component CheckForHit(Type type)
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            GameObject go = hit.collider.gameObject;
            if (go != null && go.GetComponent(type) != null)
            {
                return go.GetComponent(type);
            }
        }
        return null;
    }

    public void SingleUseItemCheck()
    {
        if (data.singleUse)
        {
            InventoryItem iItem = new InventoryItem(this);
            hia.player.inventory.RemoveItem(iItem);
            if (!hia.player.inventory.ItemIsLastInInventory(iItem))
            {
                hia.SwapItem(0);
            }
        }
    }
}
