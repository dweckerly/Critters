using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAnythingBehaviour : EatBehaviour
{
    public override bool BehaviourTrigger(Critter _critter)
    {
        Initialize();
        return base.BehaviourTrigger(_critter);
    }

    public override void Initialize()
    {
        diet = FoodType.Any;
    }
}
