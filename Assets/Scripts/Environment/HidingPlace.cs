using System.Collections;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    public Critter critter;
    public ParticleSystem ps;
    public Animator animator;

    float minAnimStartDelay = 0.1f;
    float maxAnimStartDelay = 2f;

    float minAnimInterval = 1.7f;
    float maxAnimInterval = 3.2f;

    public void AddCritter(Critter _critter)
    {
        critter = _critter;
        StartCoroutine(RandomAnimTime());
    }

    public void RemoveCritter()
    {
        critter = null;
        StopCoroutine(RandomAnimTime());
        StopCoroutine(Animate());
    }

    IEnumerator RandomAnimTime()
    {
        yield return new WaitForSeconds(Random.Range(minAnimStartDelay, maxAnimStartDelay));
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while (critter != null)
        {
            yield return new WaitForSeconds(Random.Range(minAnimInterval, maxAnimInterval));
            if(critter != null)
            {
                animator.SetTrigger("wiggle");
                ps.Play();
            }
        }
    }
}
