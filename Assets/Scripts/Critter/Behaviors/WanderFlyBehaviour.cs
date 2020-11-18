public class WanderFlyBehaviour : NormalBehaviour
{
    public override void Initialize() 
    {
        critter.critterController.flying = true;
    }

    public override void DoBehaviour()
    {
        critter.critterController.Wander();
    }
}
