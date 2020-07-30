using System.Collections;
using UnityEngine;

public class ProjectileBehaviour : CritterBehaviour
{
    public ProjectileData data;

    public bool pursue;
    public bool flee;

    bool firing = false;
    float fireInterval;
    Collider sourceCollider;

    public override void StartBehaviour()
    {

    }

    public override void EndBehaviour()
    {

    }

    private void Start()
    {
        sourceCollider = critter.gameObject.GetComponent<Collider>();
    }

    public override void DoBehaviour()
    {
        if (critter.target == null)
        {
            if (firing)
            {
                StopCoroutine("FireProjectile");
                firing = false;

            }
        }
        else
        {
            if(pursue)
            {
                critter.critterController.MoveTowardsTarget();
            }
            else if(flee)
            {
                critter.critterController.MoveAwayFromTarget();
            }
            else
            {
                critter.critterController.Idle();
            }
            
            if (!firing)
            {
                firing = true;
                StartCoroutine("FireProjectile");
            }
        }
    }

    IEnumerator FireProjectile()
    {
        while (firing)
        {
            critter.animator.SetTrigger("attack");
            Vector3 spawnPoint = new Vector3(transform.position.x, (critter.transform.lossyScale.y / 2), transform.position.z);
            GameObject projectile = Instantiate(data.prefab, spawnPoint + (transform.forward * 2), Quaternion.identity);
            Physics.IgnoreCollision(projectile.transform.GetComponent<Collider>(), sourceCollider);
            projectile.GetComponent<Rigidbody>().AddForce(((transform.up * data.arcHeight) - transform.forward) * data.speed);
            
            fireInterval = critter.data.atkSpeed;
            yield return new WaitForSeconds(fireInterval);
        }
    }
}
