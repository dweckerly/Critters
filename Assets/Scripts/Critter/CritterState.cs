using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Critter))]
public class CritterState : MonoBehaviour
{
    Critter critter;
    Dictionary<string, string> stateMethods = new Dictionary<string, string>
    {
        { "Normal", "NormalState" },
        { "Interacting", "InteractingState" },
        { "Eating", "EatingState" },
        { "Sleep", "SleepState" },
        { "Threatened", "ThreatenedState" }
    };

    public State state;

    void Start()
    {
        critter = GetComponent<Critter>();
        state = State.Normal;
        ChangeState();
    }

    IEnumerator NormalState()
    {
        critter.normalBehaviour.StartBehaviour();
        while (state == State.Normal)
        {
            critter.normalBehaviour.DoBehaviour();
            yield return 0;
        }
        critter.normalBehaviour.EndBehaviour();
        ChangeState();
    }

    IEnumerator InteractingState()
    {
        critter.interactBehaviour.StartBehaviour();
        while (state == State.Interacting)
        {
            critter.interactBehaviour.DoBehaviour();
            yield return 0;
        }
        critter.interactBehaviour.EndBehaviour();
        ChangeState();
    }

    IEnumerator EatingState()
    {
        critter.eatingBehaviour.StartBehaviour();
        while (state == State.Threatened)
        {
            critter.eatingBehaviour.DoBehaviour();
            yield return 0;
        }
        critter.eatingBehaviour.EndBehaviour();
        ChangeState();
    }

    IEnumerator SleepState()
    {
        critter.sleepBehaviour.StartBehaviour();
        while (state == State.Sleep)
        {
            critter.sleepBehaviour.DoBehaviour();
            yield return 0;
        }
        critter.sleepBehaviour.EndBehaviour();
        ChangeState();
    }

    IEnumerator ThreatenedState()
    {
        critter.threatenedBehaviour.StartBehaviour();
        while (state == State.Threatened)
        {
            critter.threatenedBehaviour.DoBehaviour();
            yield return 0;
        }
        critter.threatenedBehaviour.EndBehaviour();
        ChangeState();
    }

    void ChangeState()
    {
        System.Reflection.MethodInfo info = GetType().GetMethod(stateMethods[state.ToString()], System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
