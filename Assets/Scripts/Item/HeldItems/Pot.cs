public class Pot : CatchingItem
{
    protected override void OnEnable()
    {
        critterCatchType = CritterType.Plant;
        critterCatchSize = CritterSize.Small;
        if (hia != null)
        {
            hia.clickCoolDown = use.length;
        }
        range = 2f;
    }

    public override void UnClickAction()
    {
        return;
    }
}
