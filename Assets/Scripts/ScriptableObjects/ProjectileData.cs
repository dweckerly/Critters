using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData", order = 4)]
public class ProjectileData : ScriptableObject
{
    public GameObject prefab;
    public float speed;
    public float arcHeight;
}
