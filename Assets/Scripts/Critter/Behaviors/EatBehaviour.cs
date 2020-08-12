public class EatBehaviour : InteractBehaviour
{
    public FoodType diet;
    public override void DoBehaviour()
    {
        if(!critter.critterHunger.IsFull())
        {
            FoodType foodType = critter.target.GetComponent<Item>().data.foodType;
            if (CritterEatsThis(foodType))
            {
                base.DoBehaviour();
                if (critter.IsWithinInteractDistance())
                {
                    Eat();
                }
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
