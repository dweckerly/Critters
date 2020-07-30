using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CritterAttackData", menuName = "ScriptableObjects/CritterData", order = 6)]
public class CritterAttackData : ScriptableObject
{
    public int atkDmg;
    public float atkSpeed;

}
