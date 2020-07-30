using UnityEngine;

public abstract class CritterBehaviour : MonoBehaviour
{
    public Critter critter;

    private void Start()
    {
        critter = GetComponent<Critter>();
    }

    public abstract void StartBehaviour();

    public abstract void DoBehaviour();

    public abstract void EndBehaviour();
}
