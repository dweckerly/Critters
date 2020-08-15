using UnityEngine;

public class EatBehaviour : InteractBehaviour
{
    public FoodType diet;
    Food foodTarget;

    public override bool BehaviourTrigger(Transform target)
    {
        foodTarget = target.GetComponent<Food>();
        if (foodTarget != null && !critter.critterHunger.IsFull() && CritterEatsThis(foodTarget.data.foodType))
        {
            return true;
        }
        return false;
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        if (critter.IsWithinInteractDistance())
        {
            Eat();
        }
    }

    bool CritterEatsThis(FoodType targetFoodType)
    {
        if(targetFoodType != FoodType.None && (diet == FoodType.Any || targetFoodType == diet))
        {
            return true;
        }
        return false;
    }

    void Eat()
    {
        critter.critterHunger.Eat();
    }
}
