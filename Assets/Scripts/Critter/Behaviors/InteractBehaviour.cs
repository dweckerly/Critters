using UnityEngine;

public class InteractBehaviour : CritterBehaviour
{
    public override bool BehaviourTrigger(Transform target)
    {
        return true;
    }

    public override void Initialize() { }

    public override void DoBehaviour()
    {
        if (!critter.IsWithinDetectDistance())
        {
            critter.target = null;
        }
        else
        {
            critter.critterController.MoveTowardsTarget();
        }
    }

    public override void EndBehaviour() { }
}
