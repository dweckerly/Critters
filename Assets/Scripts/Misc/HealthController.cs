using UnityEngine;

public abstract class HealthController : MonoBehaviour
{
    protected int maxHealth;
    protected int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int dmg, Interactables tag)
    {
        currentHealth -= dmg;
    }
}
