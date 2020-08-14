using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CritterData", menuName = "ScriptableObjects/CritterData", order = 1)]
public class CritterData : InteractableData
{
    public GameObject prefab;
    public Sprite critterSprite;
    public string critterName;
    [TextArea(3, 10)]
    public string description;
    public CritterType type;
    public CritterSize size;

    #region STATS
    public int baseHP;
    public int baseATK;
    public int baseDEF;
    public int baseSATK;
    public int baseSDEF;
    public int baseSPD;
    #endregion

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
