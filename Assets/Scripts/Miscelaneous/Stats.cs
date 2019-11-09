using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Dimitrios Kitsikidis
/// <summary>
/// Holds all the stats of the entity, etc.
/// (aka the stats which each entity uses for identifications of various elements, ..
/// .. such as name, health, movement speed, etc.)
/// </summary>
public class Stats : MonoBehaviour
{
    [Header("About")]
    public string EntityName = "Entity Sample";


    [Header("Main Stats")]
    public int health = 4;
    // more to be added?
    
    [Range(0, 500)]
    public int armor = 0;
    [Range(-30, 30)]
    public int movementSpeed = 5;
    // more to be added?


    [Header("Non-Player Stats")]
    public EntityIdentification entityID = EntityIdentification.Undefined;
    public BehaviourType type = BehaviourType.Neutral;
    public BehaviourState state = BehaviourState.Passive;

    [Header("Miscelaneous Combat Stats")]
    [Range(0f, 15f)]
    public float comboResetTimer = 0f;
    [Range(0,100)]
    public int currentCombo = 0;

    public enum BehaviourType
    {
        Bad = -1,
        Neutral = 0,
        Good = 1
    }


    public enum BehaviourState
    {
        Aggressive = -1,
        Passive = 1
    }


    /// <summary>
    /// Helps to filter out the right decision making
    /// </summary>
    public enum EntityIdentification
    {
        Undefined = 0,
        Slime = 1,
        Bandit = 2,
        Bear = 3,
        Boar = 4,
        Player = 100
    }
}
