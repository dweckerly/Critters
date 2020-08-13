using UnityEngine;

public class HideBehaviour : NormalBehaviour
{
    HidingPlace hidingPlace;
    Transform hidingPlaceT;
    bool hiding = false;

    public override void DoBehaviour()
    {
        Hiding();
    }

    public override void EndBehaviour()
    {
        if (hiding)
        {
            StopHiding();
        }
    }

    void Hiding()
    {
        if(!hiding)
        {
            LookforHidingSpot();
        }
    }

    void LookforHidingSpot()
    {
        if (hidingPlaceT == null)
        {
            hidingPlaceT = critter.LookFor("HidingPlace");
            critter.critterController.WanderAndIdle();
        }
        else
        {
            if (critter.IsWithinSpecificInteractDistance(hidingPlaceT))
            {
                Hide();
            }
            else
            {
                critter.critterController.MoveTowardsSpecificTarget(hidingPlaceT);
            }
        }
    }

    void Hide()
    {
        hiding = true;
        critter.sprite.gameObject.SetActive(false);
        critter.transform.position = hidingPlaceT.position;
        hidingPlace = hidingPlaceT.GetComponent<HidingPlace>();
        hidingPlace.AddCritter(critter);
    }

    void StopHiding()
    {
        hiding = false;
        hidingPlace.RemoveCritter();
        hidingPlace = null;
        hidingPlaceT = null;
        if(!critter.sprite.gameObject.activeSelf)
        {
            critter.sprite.gameObject.SetActive(true);
            critter.animator.SetTrigger("exitHide");
        }
    }
}
