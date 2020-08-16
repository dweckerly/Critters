using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAnythingBehaviour : EatBehaviour
{
    public override bool BehaviourTrigger(Critter _critter)
    {
        Initialize();
        Debug.Log("Base returns" + base.BehaviourTrigger(_critter).ToString());
        return base.BehaviourTrigger(_critter);
    }

    public override void Initialize()
    {
        diet = FoodType.Any;
    }
}
