using UnityEngine;

public class EatBehaviour : InteractBehaviour
{
    public FoodType diet;
    Food foodTarget;

    public override void DoBehaviour()
    {
        if(critter.target != null)
        {
            foodTarget = critter.target.GetComponent<Food>();
        }
        if (foodTarget != null)
        {
            if (!critter.critterHunger.IsFull() && CritterEatsThis(foodTarget.data.foodType))
            {
                base.DoBehaviour();
                if (critter.IsWithinInteractDistance())
                {
                    Eat();
                }
            }
            else
            {
                critter.normalBehaviour.DoBehaviour();
            }
        }
        else
        {
            critter.normalBehaviour.DoBehaviour();
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
