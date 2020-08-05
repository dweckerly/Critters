public class InteractBehaviour : CritterBehaviour
{
    public override void StartBehaviour() { }

    public override void EndBehaviour() { }

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
}
