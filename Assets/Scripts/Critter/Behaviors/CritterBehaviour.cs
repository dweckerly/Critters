using UnityEngine;

public abstract class CritterBehaviour : MonoBehaviour
{
    public Critter critter;

    public virtual void StartBehaviour() 
    {
        if (critter == null)
        {
            critter = GetComponent<Critter>();
        }
    }

    public abstract void DoBehaviour();

    public abstract void EndBehaviour();
}
