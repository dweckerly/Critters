using UnityEngine;

public abstract class ConditionalFollowBehaviour : FollowBehaviour
{
    public override bool BehaviourTrigger(Critter _critter)
    {
        return FollowCondition(_critter);
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
    }

    public abstract bool FollowCondition(Critter _critter);
}
