using System.Collections;
using UnityEngine;

public class AttackBehaviour : ThreatenedBehaviour
{
    bool canAttack = true;
    float attackTime = 0f;

    HealthController targetHealth;

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        if (critter.IsWithinInteractDistance())
        {
            if (!canAttack && Time.time > attackTime + critter.data.atkSpeed)
            {
                canAttack = true;
            }
            if (canAttack)
            {
                attackTime = Time.time;
                canAttack = false;
                Attack();
            }
        }
        else
        {
            critter.critterController.MoveTowardsTarget();
        }
    }

    public override void EndBehaviour()
    {
        base.EndBehaviour();
        targetHealth = null;
    }

    void Attack()
    {
        SetTargetHealth();
        critter.animator.SetTrigger("attack");
        targetHealth.TakeDamage(critter.GetAtkDamage(), Interactables.Critter);
        critter.behaviourController.AddNewInteraction(Interactables.Critter, State.Threatened);
    }

    void SetTargetHealth()
    {
        targetHealth = critter.target.GetComponent<HealthController>();
    }
}
