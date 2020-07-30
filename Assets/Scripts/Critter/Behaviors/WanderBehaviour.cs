using UnityEngine;

public class WanderBehaviour : NormalBehaviour
{
    public override void DoBehaviour()
    {
        critter.critterController.WanderAndIdle();
    }
}
