using UnityEngine;

public class EatBehaviour : CritterBehaviour
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

    public override void Initialize() { }

    public override void DoBehaviour()
    {
        if (critter.IsWithinInteractDistance())
        {
            Eat();
        }
        else if (!critter.IsWithinDetectDistance())
        {
            critter.target = null;
        }
        else
        {
            critter.critterController.MoveTowardsTarget();
        }
    }

    public override void EndBehaviour() { }

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
