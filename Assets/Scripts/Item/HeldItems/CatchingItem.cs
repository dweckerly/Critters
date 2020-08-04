using UnityEngine;

public class CatchingItem : MeleeItem
{
    public override void LeftClickAction()
    {
        animator.SetTrigger("use");
        Critter critter = CheckForCritter();
        if(critter != null)
        {
            if ((critter.data.type == data.critterCatchType || data.critterCatchType == CritterType.Any) && critter.data.size <= data.critterCatchSize)
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
            Instantiate(data.catchPS, critter.gameObject.transform.position, Quaternion.identity);
            Destroy(critter.gameObject);
        }
        else
        {
            hia.player.messageSystem.AddMessage("You can't carry anymore critters!");
        }
    }
}
