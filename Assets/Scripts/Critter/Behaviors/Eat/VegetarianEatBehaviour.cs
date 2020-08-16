using UnityEngine;

public class VegetarianEatBehaviour : EatBehaviour
{
    public override bool BehaviourTrigger(Critter _critter)
    {
        Initialize();
        return base.BehaviourTrigger(_critter);
    }

    public override void Initialize()
    {
        diet = FoodType.Vegetable;
    }
}
