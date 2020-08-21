using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string description;
    public Sprite image;
    public bool singleUse;
    public int weight;
    public GameObject prefab;
    public ItemEffect itemEffect;

    public float range;
    public Animator animatorController;
    public AnimationClip swapOut;
    public AnimationClip swapIn;
    public AnimationClip use;


    public ParticleSystem catchPS;
    public CritterType critterCatchType;
    public CritterSize critterCatchSize;

    public FoodType foodType;
    public bool decays;

    private void Awake()
    {
        if(itemEffect != null)
        {
            itemEffect.item = prefab.GetComponent<Item>();
        }
    }
}
