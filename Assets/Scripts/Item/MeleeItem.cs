using System;
using UnityEngine;

public class MeleeItem : Item
{
    protected override void Initialize()
    {
        if(hia != null)
        {
            hia.clickCoolDown = data.use.length;
        }   
    }

    public Critter CheckForCritter()
    {
        return (Critter)CheckForHit(typeof(Critter));
    }

    public Item CheckForItem()
    {
        return (Item)CheckForHit(typeof(Item)); ;
    }

    Component CheckForHit(Type type)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, data.range))
        {
            GameObject go = hit.collider.gameObject;
            if (go != null && go.GetComponent(type) != null)
            {
                return go.GetComponent(type);
            }
        }
        return null;
    }
}
