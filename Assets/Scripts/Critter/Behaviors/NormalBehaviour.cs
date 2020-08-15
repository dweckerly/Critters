using UnityEngine;

public class NormalBehaviour : CritterBehaviour
{
    public override bool BehaviourTrigger(Transform target)
    {
        return true;
    }

    public override void Initialize() { }

    public override void DoBehaviour() { }

    public override void EndBehaviour() { }
}
