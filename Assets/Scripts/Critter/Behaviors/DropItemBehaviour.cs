using UnityEngine;

public class DropItemBehaviour : WanderBehaviour
{
    GameObject dropItem;
    float startTime;
    float dropTime;

    float minDropInterval = 10f;
    float maxDropInterval = 30f;

    public override void StartBehaviour() 
    {
        base.StartBehaviour();
        dropItem = critter.data.dropItem;
        SetTime();
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        DropItem();
    }

    void SetTime()
    {
        startTime = Time.time;
        dropTime = Random.Range(minDropInterval, maxDropInterval);
    }

    void DropItem()
    {
        if(startTime + dropTime < Time.time)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
            SetTime();
        }
    }
}
