using UnityEngine;

[RequireComponent(typeof(Critter))]
public class CritterHealth : HealthController
{
    Critter critter;

    private void Awake()
    {
        critter = GetComponent<Critter>();
        maxHealth = critter.data.health;
    }

    public override void TakeDamage(int dmg, Interactables tag)
    {
        base.TakeDamage(dmg, tag);
        critter.animator.SetTrigger("hit");
        
        if (currentHealth <= 0)
        {
            critter.Die();
            return;
        }
        else
        {
            critter.behaviourController.AddNewInteraction(tag, State.Threatened);
        }
        critter.critterStats.UpdateHP(currentHealth);
    }
}
