using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CritterData", menuName = "ScriptableObjects/CritterData", order = 1)]
public class CritterData : InteractableData
{
    public GameObject prefab;
    public string critterName;
    public CritterType type;
    public CritterSize size;
    public float speed;
    public int health;
    public int atkDamage;
    public float atkSpeed;

    public GameObject dropItem;

    public ActiveTime[] activeTimes;

    public List<CritterInteraction> critterInteractions;
    public CritterBehaviour defaultNormalBehaviour;
    public CritterBehaviour defaultThreatenedBehaviour;
    public CritterBehaviour defaultInteractBehaviour;
    public CritterBehaviour defaultSleepBehaviour;
}
