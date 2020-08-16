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
        Debug.Log("Entering normal state ON " + critter.data.critterName);
        critter.normalBehaviour.StartBehaviour();
        while (state == State.Normal)
        {
            critter.normalBehaviour.DoBehaviour();
            yield return 0;
        }
        Debug.Log("Leaving normal state OFF " + critter.data.critterName);
        critter.normalBehaviour.EndBehaviour();
        ChangeState();
    }

    IEnumerator InteractingState()
    {
        Debug.Log("Entering interacting state ON " + critter.data.critterName);
        critter.interactBehaviour.StartBehaviour();
        while (state == State.Interacting)
        {
            critter.interactBehaviour.DoBehaviour();
            yield return 0;
        }
        Debug.Log("Leaving interacting state OFF " + critter.data.critterName);
        critter.interactBehaviour.EndBehaviour();
        ChangeState();
    }

    IEnumerator EatingState()
    {
        Debug.Log("Entering EATING state ON " + critter.data.critterName);
        critter.eatingBehaviour.StartBehaviour();
        while (state == State.Threatened)
        {
            critter.eatingBehaviour.DoBehaviour();
            yield return 0;
        }
        Debug.Log("Leaving EATING state OFF " + critter.data.critterName);
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
        Debug.Log("Entering threatened state ON " + critter.data.critterName);
        critter.threatenedBehaviour.StartBehaviour();
        while (state == State.Threatened)
        {
            critter.threatenedBehaviour.DoBehaviour();
            yield return 0;
        }
        Debug.Log("Leaving threatened state OFF " + critter.data.critterName);
        critter.threatenedBehaviour.EndBehaviour();
        ChangeState();
    }

    void ChangeState()
    {
        System.Reflection.MethodInfo info = GetType().GetMethod(stateMethods[state.ToString()], System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
