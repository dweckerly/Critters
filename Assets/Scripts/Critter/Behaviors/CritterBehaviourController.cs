using UnityEngine;

public class CritterBehaviourController : MonoBehaviour
{
    Critter critter;

    private void Start()
    {
        critter = GetComponent<Critter>();
        SetState(State.Normal);
    }

    public void SetBehaviour(CritterInteraction interaction)
    {
        ReturnToNormalState();
        switch (interaction.inducedState)
        {
            case State.Threatened:
                if (interaction.behaviour != null)
                {
                    critter.threatenedBehaviour = UpdateBehaviourComponent(interaction.behaviour);
                }
                else
                {
                    critter.threatenedBehaviour = UpdateBehaviourComponent(critter.data.defaultThreatenedBehaviour);
                }
                break;
            case State.Interacting:
                if (interaction.behaviour != null)
                {
                    critter.interactBehaviour = UpdateBehaviourComponent(interaction.behaviour);
                }
                else
                {
                    critter.interactBehaviour = UpdateBehaviourComponent( critter.data.defaultInteractBehaviour);
                }
                break;
            case State.Sleep:
                if (interaction.behaviour != null)
                {
                    critter.sleepBehaviour = UpdateBehaviourComponent(interaction.behaviour);
                }
                else
                {
                    critter.sleepBehaviour = UpdateBehaviourComponent(critter.data.defaultSleepBehaviour);
                }
                break;
            case State.Eating:
                if(interaction.behaviour != null)
                {
                    critter.eatingBehaviour = UpdateBehaviourComponent(interaction.behaviour);
                }
                else
                {
                    critter.eatingBehaviour = UpdateBehaviourComponent(critter.data.defaultSleepBehaviour);
                }
                break;
        }
        SetState(interaction.inducedState);
    }
    
    CritterBehaviour UpdateBehaviourComponent(CritterBehaviour behaviour)
    {
        CritterBehaviour[] currentBehaviours = GetComponents<CritterBehaviour>();
        foreach(CritterBehaviour cb in currentBehaviours)
        {
            if(cb.GetType() == behaviour.GetType())
            {
                return cb;
            }
        }
        return null;
    }

    void SetState(State state)
    {
        if (state != critter.critterState.state)
        {
            critter.critterState.state = state;
        }
    }

    public void ReturnToNormalState()
    {
        SetState(State.Normal);
    }

    public void AddNewInteraction(Interactables tag, State state, CritterBehaviour behaviour = null)
    {
        foreach (CritterInteraction interaction in critter.critterInteractions)
        {
            if (interaction.interactableTag == tag)
            {
                if (interaction.inducedState == state)
                {
                    return;
                }
                else
                {
                    critter.critterInteractions.Remove(interaction);
                    break;
                }
            }
        }
        CritterInteraction newInteraction = new CritterInteraction();
        newInteraction.interactableTag = tag;
        newInteraction.inducedState = state;
        newInteraction.behaviour = behaviour;
        critter.critterInteractions.Add(newInteraction);
    }
}
