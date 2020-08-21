using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Item : MonoBehaviour
{
    public ItemData data;
    public ItemEffect itemEffect;

    public Animator animator;

    public HeldItemAction hia;

    float arcHeight = 1.2f;
    float speed = 250;

    Rigidbody rb;
    Collider itemCollider;

    bool beingHeld;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator = data.animatorController;
        if(data.itemEffect != null)
        {
            itemEffect = (ItemEffect)gameObject.AddComponent(data.itemEffect.GetType());
            itemEffect.item = this;
        }
        if (data.decays)
        {
            gameObject.AddComponent(typeof(ItemDecay));
        }
        rb = GetComponent<Rigidbody>();
        itemCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        if (transform.parent != null)
        {
            beingHeld = true;
            ItemIsBeingHeld();
        }
        else
        {
            beingHeld = false;
            ItemIsOnGround();
        }
        Initialize();
    }

    public virtual void LeftClickAction()
    {
        if(itemEffect != null)
        {
            itemEffect.Use();
        }
        else
        {
            hia.player.messageSystem.AddMessage("This doesn't seem to do anything...");
        }
    }
    public virtual void UnClickAction()
    {
        return;
    }

    public virtual void RightClickAction()
    {
        if(data.singleUse)
        {
            Throw();
        }
    }

    protected virtual void Initialize() 
    {
        ItemDecay itemDecay = GetComponent<ItemDecay>();
        if (itemDecay != null)
        {
            itemDecay.enabled = !IsItemBeingHeld();
        }
    }

    public float SwapOut()
    {
        animator.SetTrigger("swapOut");
        return data.swapOut.length;
    }

    public float SwapIn()
    {
        return data.swapIn.length;
    }

    public void Throw()
    {
        if (data.prefab != null)
        {
            EnablePhysics();
            InventoryItem iItem = new InventoryItem(this);
            hia.player.inventory.RemoveItem(iItem);
            GameObject projectile = Instantiate(data.prefab, hia.player.transform.position + hia.player.transform.forward, hia.transform.rotation);
            projectile.transform.SetParent(null);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(((transform.up * arcHeight) + hia.transform.forward) * speed);
            float torque = Random.Range(-100, 100);
            rb.AddTorque(transform.forward * torque);
            if (!hia.player.inventory.ItemIsLastInInventory(iItem))
            {
                hia.SwapItem(0);
            }
            if (hia.player.inventory.ItemIsLastInInventory(iItem))
            {
                if(hia.player.inventory.inventory.items.Count > 0)
                {
                    hia.player.inventory.SetSelectedItem(0);
                }
            }
        }
    }

    public void SingleUseItemCheck()
    {
        if (data.singleUse)
        {
            InventoryItem iItem = new InventoryItem(this);
            hia.player.inventory.RemoveItem(iItem);
            if (!hia.player.inventory.ItemIsLastInInventory(iItem))
            {
                hia.SwapItem(0);
            }
        }
    }

    public bool IsItemBeingHeld()
    {
        return beingHeld;
    }

    private void ItemIsBeingHeld()
    {
        animator.enabled = true;
        DisablePhysics();
    }

    private void ItemIsOnGround()
    {
        animator.enabled = false;
        EnablePhysics();
    }

    private void EnablePhysics()
    {
        itemCollider.enabled = true;
        rb.detectCollisions = true;
    }

    private void DisablePhysics()
    {
        itemCollider.enabled = true;
        rb.detectCollisions = false;
    }
}
