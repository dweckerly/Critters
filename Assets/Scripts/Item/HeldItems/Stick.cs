using UnityEngine;

public class Stick : HeldItem
{
    int damage = 1;

    public AnimationClip use;

    protected override void OnEnable()
    {
        hia.clickCoolDown = use.length;
        range = 2f;
    }

    public override void ClickAction()
    {
        animator.SetTrigger("use");
        Critter critter = CheckForCritter();
        if(critter != null)
        {
            critter.critterHealth.TakeDamage(damage, Interactables.Player);
        }
    }

    public override void UnClickAction()
    {
        return;
    }
}
