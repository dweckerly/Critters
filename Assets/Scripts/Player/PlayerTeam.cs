using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    int maxCritters = 3;
    [SerializeField]
    public List<CritterData> critters = new List<CritterData>();

    public bool AddCritter(Critter critter)
    {
        if(critters.Count < maxCritters)
        {
            critters.Add(critter.data);
            return true;
        }
        return false;
    }
}
