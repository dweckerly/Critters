using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAnythingBehaviour : EatBehaviour
{
    void Start()
    {
        diet = FoodType.Any;
    }
}
