using UnityEngine;

public abstract class CatchingItem : HeldItem
{
    public AnimationClip use;
    public ParticleSystem catchPS;

    protected CritterType critterCatchType;
    protected CritterSize critterCatchSize;

    public Item containerItem;

    public override void ClickAction()
    {
        animator.SetTrigger("use");
        Critter critter = CheckForCritter();
        if(critter != null)
        {
            if ((critter.data.type == critterCatchType || critterCatchType == CritterType.Any) && critter.data.size <= critterCatchSize)
            {
                Catch(critter);
            }
            else
            {
                hia.player.messageSystem.AddMessage("This critter can't be caught with the " + data.itemName + "!");
            }
        }
    }

    protected void Catch(Critter critter)
    {
        if (hia.player.playerTeam.AddCritter(critter))
        {
            SingleUseItemCheck();
            hia.player.messageSystem.AddMessage("You caught a wild " + critter.data.critterName + "!");
            Instantiate(catchPS, critter.gameObject.transform.position, Quaternion.identity);
            Destroy(critter.gameObject);
        }
        else
        {
            hia.player.messageSystem.AddMessage("You can't carry anymore critters!");
        }
    }
}
