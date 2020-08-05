using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealPlayer : ItemEffect
{
    public int healAmount;

    public override void Use()
    {
        item.hia.player.playerHealth.HealDamage(healAmount);
        base.Use();
    }
}
