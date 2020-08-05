using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationData", menuName = "ScriptableObjects/LocationData", order = 2)]
public class LocationData : ScriptableObject
{
    public string locationName;
    [SerializeField]
    public List<CritterSpawnChance> critterSpawnChances;
}
