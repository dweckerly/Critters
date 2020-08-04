using UnityEngine;

public class Stick : MeleeItem
{
    int damage = 1;

    public override void LeftClickAction()
    {
        animator.SetTrigger("use");
        Critter critter = CheckForCritter();
        if(critter != null)
        {
            critter.critterHealth.TakeDamage(damage, Interactables.Player);
        }
    }
}
