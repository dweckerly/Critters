using UnityEngine;

public abstract class CritterBehaviour : MonoBehaviour
{
    public Critter critter;

    public abstract bool BehaviourTrigger(Transform target);

    public void StartBehaviour()
    {
        if (critter == null)
        {
            critter = GetComponent<Critter>();
        }
        Initialize();
    }

    public abstract void Initialize();

    public abstract void DoBehaviour();

    public abstract void EndBehaviour();
}
