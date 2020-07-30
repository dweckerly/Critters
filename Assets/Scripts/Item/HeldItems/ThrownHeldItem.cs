using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownHeldItem : HeldItem
{
    float arcHeight = 2f;
    float speed = 200;

    public Transform projectilePosition;

    protected override void OnEnable()
    {
        if (hia != null)
        {
            hia.clickCoolDown = 0.5f;
        }
        range = 2f;
    }

    public override void ClickAction()
    {
        Vector3 spawnPoint = projectilePosition.position;
        GameObject projectile = Instantiate(data.prefab, spawnPoint + (hia.transform.forward * 2), hia.transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(((transform.up * arcHeight) + hia.transform.forward) * speed);
        SingleUseItemCheck();
    }

    public override void UnClickAction()
    {
        return;
    }
}
