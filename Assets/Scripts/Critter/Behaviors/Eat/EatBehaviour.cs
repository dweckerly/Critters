using UnityEngine;

public class EatBehaviour : InteractBehaviour
{
    public FoodType diet;
    Food foodTarget;

    public override bool BehaviourTrigger(Critter _critter)
    {
        foodTarget = _critter.target.GetComponent<Food>();
        if (foodTarget != null && !_critter.critterHunger.IsFull() && CritterEatsThis(foodTarget.data.foodType))
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
        if(diet == FoodType.Any || targetFoodType == diet)
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
