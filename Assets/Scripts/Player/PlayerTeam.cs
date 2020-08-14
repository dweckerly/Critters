using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    int maxCritters = 3;

    public List<CritterStats> critters = new List<CritterStats>();

    public bool AddCritter(Critter critter)
    {
        if(critters.Count < maxCritters)
        {
            critters.Add(critter.critterStats);
            return true;
        }
        return false;
    }
}
