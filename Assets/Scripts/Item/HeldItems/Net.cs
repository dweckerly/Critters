public class Net : CatchingItem
{
    protected override void OnEnable()
    {
        critterCatchType = CritterType.Bug;
        critterCatchSize = CritterSize.Small;
        if(hia != null)
        {
            hia.clickCoolDown = use.length;
        }
        range = 2.5f;
    }

    public override void UnClickAction()
    {
        return;
    }
}
