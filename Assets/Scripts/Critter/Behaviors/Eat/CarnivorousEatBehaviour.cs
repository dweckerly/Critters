using UnityEngine;

public class CarnivorousEatBehaviour : EatBehaviour
{
    public override bool BehaviourTrigger(Critter _critter)
    {
        Initialize();
        return base.BehaviourTrigger(_critter);
    }

    public override void Initialize()
    {
        diet = FoodType.Meat;
    }
}
