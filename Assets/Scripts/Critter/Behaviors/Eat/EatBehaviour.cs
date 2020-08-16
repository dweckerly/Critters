using UnityEngine;

public class EatBehaviour : InteractBehaviour
{
    public FoodType diet;
    Food foodTarget;

    public override bool BehaviourTrigger(Critter _critter)
    {
        foodTarget = _critter.target.GetComponent<Food>();
        if(foodTarget != null)
        {
            Debug.Log("FoodTarget is NOT null");
            if (CritterEatsThis(foodTarget.data.foodType))
            {
                Debug.Log("Critter EATS THIS");
            }
        }
        if(_critter.critterHunger.IsFull())
        {
            Debug.Log("Critter is FULL");
        }
        if (foodTarget != null && !_critter.critterHunger.IsFull() && CritterEatsThis(foodTarget.data.foodType))
        {
            Debug.Log("Eat Behaviour Returned TRUE");
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
