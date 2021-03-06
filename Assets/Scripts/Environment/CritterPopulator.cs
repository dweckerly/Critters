﻿using System.Collections.Generic;
using UnityEngine;

public class CritterPopulator : MonoBehaviour
{
    public LocationData locationData;
    List<Critter> locationCritters = new List<Critter>();

    void Awake()
    {
        SetLocationCritters();
        //PlaceHidingCritters();
        PlaceWanderingCritter();
    }

    void SetLocationCritters()
    {
        for (int i = 0; i < locationData.critterSpawnChances.Count; i++)
        {
            for (int j = 0; j < locationData.critterSpawnChances[i].maxAmount; j++)
            {
                if(GetSpawnedCritters(locationData.critterSpawnChances[i].critter) < locationData.critterSpawnChances[i].minAmount)
                {
                    locationCritters.Add(locationData.critterSpawnChances[i].critter);
                }
                else
                {
                    float rand = Random.Range(0f, 1f);
                    if (rand < locationData.critterSpawnChances[i].chance)
                    {
                        locationCritters.Add(locationData.critterSpawnChances[i].critter);
                    }
                }
            }
        }
    }

    int GetSpawnedCritters(Critter critter)
    {
        return locationCritters.FindAll(s => s.Equals(critter)).Count;
    }

    Critter[] GetHidingCritters()
    {
        List<Critter> hidingCritters = new List<Critter>();
        for (int i = 0; i < locationCritters.Count; i++)
        {
            if(locationCritters[i].GetComponent<HideBehaviour>() != null)
            {
                hidingCritters.Add(locationCritters[i]);
            }
        }
        return hidingCritters.ToArray();
    }

    Critter[] GetWanderingCritters()
    {
        List<Critter> wanderCritters = new List<Critter>();
        for (int i = 0; i < locationCritters.Count; i++)
        {
            wanderCritters.Add(locationCritters[i]);
        }
        return wanderCritters.ToArray();
    }

    /*
    void PlaceHidingCritters()
    {
        HidingPlace[] hidingPlaces = FindObjectsOfType<HidingPlace>();
        Critter[] critters = GetHidingCritters();
        for (int i = 0; i < critters.Length; i++)
        {
            // Need to check for if Hiding Place already has a critter
            // currently skipping a critter if hiding place location is already populated
            int hideIndex = Random.Range(0, hidingPlaces.Length);
            if (hidingPlaces[hideIndex].critter == null)
            {
                hidingPlaces[hideIndex].AddCritter(critters[i]);
                Instantiate(critters[i], hidingPlaces[hideIndex].transform.parent.transform.parent.position, Quaternion.identity);
            }
        }
    }
    */

    void PlaceWanderingCritter()
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        Critter[] critters = GetWanderingCritters();

        if(spawnPoints.Length > 0)
        {
            for (int i = 0; i < critters.Length; i++)
            {
                // Need to check for if Hiding Place already has a critter
                // currently skipping a critter if hiding place location is already populated
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                if (!spawnPoints[spawnIndex].hasCritter)
                {
                    spawnPoints[spawnIndex].hasCritter = true;
                    critters[i].level = Random.Range(1, 5);
                    Instantiate(critters[i], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
                }
            }
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                Destroy(spawnPoint);
            }
        }
    }
}
