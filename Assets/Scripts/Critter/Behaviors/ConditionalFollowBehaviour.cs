using UnityEngine;

public abstract class ConditionalFollowBehaviour : FollowBehaviour
{
    public override bool BehaviourTrigger(Transform target)
    {
        return FollowCondition(target);
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
    }

    public abstract bool FollowCondition(Transform target);
}
