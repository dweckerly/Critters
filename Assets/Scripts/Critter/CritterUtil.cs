using System.Collections.Generic;
using UnityEngine;

public class CritterUtil
{
    Critter critter;

    public CritterUtil(Critter _critter)
    {
        critter = _critter;
    }

    public float DistanceFrom(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1, pos2);
    }

    bool IsThisCloserThanThat(Transform _this, Transform _that)
    {
        float thisDistance = DistanceFrom(critter.transform.position, _this.position);
        float thatDistance = DistanceFrom(critter.transform.position, _that.position);
        if (thisDistance < thatDistance)
        {
            return true;
        }
        return false;
    }

    bool IsThisFartherThanThat(Transform _this, Transform _that)
    {
        float thisDistance = DistanceFrom(critter.transform.position, _this.position);
        float thatDistance = DistanceFrom(critter.transform.position, _that.position);
        if (thisDistance > thatDistance)
        {
            return true;
        }
        return false;
    }

    bool IsThisTheSameDistanceAsThat(Transform _this, Transform _that)
    {
        float thisDistance = DistanceFrom(critter.transform.position, _this.position);
        float thatDistance = DistanceFrom(critter.transform.position, _that.position);
        if (thisDistance == thatDistance)
        {
            return true;
        }
        return false;
    }

    public void SortByDistance(GameObject[] goArr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(goArr, left, right);

            if (pivot > 1)
            {
                SortByDistance(goArr, left, pivot - 1);
            }
            if (pivot + 1 < right)
            {
                SortByDistance(goArr, pivot + 1, right);
            }
        }
    }

    int Partition(GameObject[] arr, int left, int right)
    {
        GameObject pivot = arr[left];
        while (true)
        {
            while (IsThisCloserThanThat(arr[left].transform, pivot.transform))
            {
                left++;
            }
            while (IsThisFartherThanThat(arr[right].transform, pivot.transform))
            {
                right--;
            }
            if (left < right)
            {
                if (IsThisTheSameDistanceAsThat(arr[left].transform, arr[right].transform))
                {
                    return right;
                }
                GameObject temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
            }
            else
            {
                return right;
            }
        }
    }

    public Transform DetermineTarget(List<GameObject> detectedObjects)
    {
        GameObject detectedTarget = null;
        for (int i = 0; i < detectedObjects.Count; i++)
        {
            if (detectedTarget == null)
            {
                detectedTarget = detectedObjects[i];
            }
            else
            {
                // if both food check which is closer
                if (detectedObjects[i].CompareTag("Food") && detectedTarget.CompareTag("Food"))
                {
                    if (IsThisCloserThanThat(detectedObjects[i].transform, detectedTarget.transform))
                    {
                        detectedTarget = detectedObjects[i];
                    }
                }
                // replace food, compare for closer critter
                if (detectedObjects[i].CompareTag("Player"))
                {
                    if (detectedTarget.CompareTag("Food"))
                    {
                        detectedTarget = detectedObjects[i];
                    }
                    else if (detectedTarget.CompareTag("Critter"))
                    {
                        if (IsThisCloserThanThat(detectedObjects[i].transform, detectedTarget.transform))
                        {
                            detectedTarget = detectedObjects[i];
                        }
                    }
                }
                // replace food, compare for closer critter or closer player
                if (detectedObjects[i].CompareTag("Critter") && critter.IsThreatenedByCritter(detectedObjects[i].GetComponent<CritterData>()))
                {
                    if (detectedTarget.CompareTag("Food"))
                    {
                        detectedTarget = detectedObjects[i];
                    }
                    else if (detectedTarget.CompareTag("Critter") || (detectedTarget.CompareTag("Player")))
                    {
                        if (IsThisCloserThanThat(detectedObjects[i].transform, detectedTarget.transform))
                        {
                            detectedTarget = detectedObjects[i];
                        }
                    }
                }
            }
        }
        return detectedTarget.transform;
    }
}
