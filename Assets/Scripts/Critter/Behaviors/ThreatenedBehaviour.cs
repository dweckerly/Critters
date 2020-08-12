using UnityEngine;

public class ThreatenedBehaviour : CritterBehaviour
{
    float safeTeimInterval = 3f;
    float startTime;

    public override void StartBehaviour()
    {
        base.StartBehaviour();
        startTime = Time.time;
        critter.emoteController.ShowEmote(Emotes.Exclamation);
    }

    public override void DoBehaviour()
    {
        ReturnToNormalBehaviourCheck();
    }

    public override void EndBehaviour()
    {
        critter.target = null;
    }


    public void ReturnToNormalBehaviourCheck()
    {
        if (critter.IsWithinDetectDistance())
        {
            startTime = Time.time;
        }
        else if(critter.target == null)
        {
            // condition covers if critter is destoryed
            critter.behaviourController.ReturnToNormalState();
        }
        else
        {
            if (Time.time > startTime + safeTeimInterval)
            {
                critter.behaviourController.ReturnToNormalState();
            }
        }
    }
}
