public class RunBehaviour : ThreatenedBehaviour
{
    public override void DoBehaviour()
    {
        base.DoBehaviour();
        critter.critterController.MoveAwayFromTarget();
    }
}
