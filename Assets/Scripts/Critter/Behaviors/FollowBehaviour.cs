public class FollowBehaviour : InteractBehaviour
{
    public override void DoBehaviour()
    {
        base.DoBehaviour();
        if(critter.IsWithinInteractDistance())
        {
            critter.critterController.Idle();
        }
    }
}
