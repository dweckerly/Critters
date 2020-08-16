using UnityEngine;

public abstract class ConditionalFollowBehaviour : FollowBehaviour
{
    public override bool BehaviourTrigger(Critter _critter)
    {
        return true;
    }

    public override void DoBehaviour()
    {
        if(FollowCondition())
        {
            base.DoBehaviour();
        }
        else
        {
            if(critter.IsWithinDetectDistance())
            {
                critter.critterController.MoveAwayFromTarget();
            }
            else
            {
                critter = null;
            }
        }
    }

    public abstract bool FollowCondition();
}
