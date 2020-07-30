public class EatBehaviour : InteractBehaviour
{
    public override void DoBehaviour()
    {
        if(!critter.critterHunger.IsFull())
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

    void Eat()
    {
        critter.critterHunger.Eat();
    }
}
