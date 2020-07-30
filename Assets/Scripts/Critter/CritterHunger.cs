using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Critter))]
public class CritterHunger : MonoBehaviour
{
    Critter critter;
    bool eating;
    int hunger = 0;
    int maxHunger;

    float hungerDecrementTime = 60f;
    float hungerIncrementTime = 3f;

    private void Start()
    {
        critter = GetComponent<Critter>();
        maxHunger = critter.data.size == CritterSize.Small ? 5 :
            critter.data.size == CritterSize.Medium ? 10 :
            critter.data.size == CritterSize.Large ? 15 : 0;
        StartCoroutine("DecrementHunger");
    }

    public bool IsEating()
    {
        return eating;
    }

    public bool IsFull()
    {
        return hunger == maxHunger;
    }

    public void Eat()
    {
        if(!eating)
        {
            eating = true;
            StopCoroutine("DecrementHunger");
            StartCoroutine("IncrementHunger");
        }
    }

    public void StopEating()
    {
        if(eating)
        {
            StopCoroutine("IncrementHunger");
            StartCoroutine("DecrementHunger");
            eating = false;
        }
    }

    IEnumerator IncrementHunger()
    {
        while (eating)
        {
            if (hunger != maxHunger)
            {
                hunger++;
            }
            else
            {
                StopEating();
            }
            yield return new WaitForSeconds(hungerIncrementTime);
        }
    }

    IEnumerator DecrementHunger()
    {
        while (!eating)
        {
            if (hunger != 0)
            {
                hunger--;
            }
            yield return new WaitForSeconds(hungerDecrementTime);
        }
    }
}
