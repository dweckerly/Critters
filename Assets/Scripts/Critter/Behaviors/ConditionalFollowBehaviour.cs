public abstract class ConditionalFollowBehaviour : FollowBehaviour
{

    public override void DoBehaviour()
    {
        if (!critter.IsWithinDetectDistance())
        {
            critter.target = null;
        }
        else
        {
            if (FollowCondition())
            {
                base.DoBehaviour();
            }
            else
            {
                critter.critterController.MoveAwayFromTarget();
            }
        }
    }

    public abstract bool FollowCondition();
}
