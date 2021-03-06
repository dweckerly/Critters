﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CritterController))]
[RequireComponent(typeof(CritterBehaviourController))]
[RequireComponent(typeof(CritterState))]
[RequireComponent(typeof(CritterHunger))]
[RequireComponent(typeof(CritterHealth))]
public class Critter : MonoBehaviour
{
    public CritterData data;
    public int level;
    public CritterStats critterStats;

    public CritterController critterController;
    public CritterBehaviourController behaviourController;
    public CritterState critterState;
    public CritterHunger critterHunger;
    public CritterHealth critterHealth;

    public Transform sprite;
    public Animator animator;

    public ParticleSystem petPS;
    public ParticleSystem deathPS;

    public EmoteController emoteController;

    [HideInInspector]
    public Transform target = null;

    public float detectRadius = 7f;
    float interactDistance = 2f;
    
    public float speed;
    int atkDamage;
    float atkSpeed;

    CritterUtil cUtil;

    [HideInInspector]
    public CritterBehaviour normalBehaviour;
    [HideInInspector]
    public CritterBehaviour interactBehaviour;
    [HideInInspector]
    public CritterBehaviour eatingBehaviour;
    [HideInInspector]
    public CritterBehaviour sleepBehaviour;
    [HideInInspector]
    public CritterBehaviour threatenedBehaviour;

    public List<CritterInteraction> critterInteractions;
    GameObject dropItem;

    float scaleVariance;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        normalBehaviour = (CritterBehaviour)gameObject.AddComponent(data.defaultNormalBehaviour.GetType());
        interactBehaviour = (CritterBehaviour)gameObject.AddComponent(data.defaultInteractBehaviour.GetType());
        eatingBehaviour = (CritterBehaviour)gameObject.AddComponent(data.defaultEatingBehaviour.GetType());
        sleepBehaviour = (CritterBehaviour)gameObject.AddComponent(data.defaultSleepBehaviour.GetType());
        threatenedBehaviour = (CritterBehaviour)gameObject.AddComponent(data.defaultThreatenedBehaviour.GetType());
        critterInteractions = new List<CritterInteraction>(data.critterInteractions);
        foreach (CritterInteraction interaction in critterInteractions)
        {
            if(interaction.behaviour != null)
            {
                gameObject.AddComponent(interaction.behaviour.GetType());
            }
        }
        dropItem = data.dropItem;
        scaleVariance = Random.Range(0.75f, 1.25f);
        //float yPosition = scaleVariance / 2f;
        float xScale = transform.localScale.x;
        float yScale = transform.localScale.y;
        transform.localScale = new Vector3(scaleVariance * xScale, scaleVariance * yScale, 1);
        //transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        interactDistance *= scaleVariance;
    }

    private void Start()
    {
        critterStats = new CritterStats(data, level, scaleVariance);
        speed = data.speed;
        atkDamage = data.atkDamage;
        atkSpeed = data.atkSpeed;
        cUtil = new CritterUtil(this);
    }

    private void FixedUpdate()
    {
        Detect();
        if(critterState.state != State.Normal && target == null)
        {
            behaviourController.ReturnToNormalState();
        }
    }

    void Detect()
    {
        Collider[] hitColliders = GetDetectableColliders();
        if(hitColliders != null)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject != gameObject)
                {
                    foreach (CritterInteraction interact in critterInteractions)
                    {
                        if (hitColliders[i].gameObject.CompareTag(interact.interactableTag.ToString()) && critterState.state < interact.inducedState)
                        {
                            Transform currentTarget = target;
                            target = hitColliders[i].transform;
                            if(interact.behaviour != null)
                            {
                                if (interact.behaviour.BehaviourTrigger(this))
                                {
                                    behaviourController.SetBehaviour(interact);
                                }
                                else
                                {
                                    target = currentTarget;
                                }
                            }
                            else
                            {
                                behaviourController.SetBehaviour(interact);
                            }
                        }
                    }
                }
            }
        }
    }

    public Transform LookFor(string tag)
    {
        Collider[] hitColliders = GetDetectableColliders();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].gameObject.CompareTag(tag))
            {
                return hitColliders[i].transform;
            }
        }
        return null;
    }

    Collider[] GetDetectableColliders()
    {
        Vector3 center = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, detectRadius);
        if (hitColliders.Length == 1)
        {
            return hitColliders;
        }
        else if (hitColliders.Length > 1)
        {
            return hitColliders.OrderBy(x => Vector3.Distance(center, x.transform.position)).ToArray();
        }
        return null;
    }

    public void Die()
    {
        Instantiate(deathPS, transform.position, Quaternion.identity);
        Instantiate(dropItem, new Vector3(transform.position.x, dropItem.transform.position.y, transform.position.z), dropItem.transform.rotation);
        Destroy(gameObject);
    }

    public void SetDropItem(GameObject _dropItem)
    {
        dropItem = _dropItem;
    }

    public void Pet()
    {
        petPS.Play();
    }

    public bool IsThreatenedByCritter(CritterData forgeignCritterData)
    {
        if (data == forgeignCritterData || data.size > forgeignCritterData.size || (data.type == forgeignCritterData.type && data.size > forgeignCritterData.size))
        {
            return false;
        }
        return true;
    }

    public bool IsWithinInteractDistance()
    {
        if(target != null)
        {
            return cUtil.DistanceFrom(transform.position, target.position) <= interactDistance;
        }
        return false;
    }

    public bool IsWithinSpecificInteractDistance(Transform _target)
    {
        if (_target != null)
        {
            return cUtil.DistanceFrom(transform.position, _target.position) <= interactDistance;
        }
        return false;
    }

    public bool IsWithinDetectDistance()
    {
        if(target != null)
        {
            return cUtil.DistanceFrom(transform.position, target.position) <= detectRadius;
        }
        return false;
    }

    public int GetAtkDamage()
    {
        return atkDamage;
    }
}
