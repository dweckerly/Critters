using UnityEngine;

public enum State
{
    Normal,
    Interacting,
    Threatened,
    Sleep
}

public enum CritterType
{
    Any,
    Bug,
    Plant
}

public enum CritterSize
{
    Small,
    Medium,
    Large
}

public enum Interactables
{
    Food,
    Critter,
    Item,
    Player
}

public enum ActiveTime
{
    Morning,
    Day,
    Dusk,
    Night
}

[System.Serializable]
public struct CritterInteraction
{
    public Interactables interactableTag;
    public State inducedState;
    public CritterBehaviour behaviour;
}

public abstract class InteractableData : ScriptableObject
{
    
}
