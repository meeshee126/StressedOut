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
    [Space]
    public string entityName = "Entity Sample";
    [TextArea]
    public string description;

    [Space(10)]
    [Header("Main Stats")]
    [Space]
    public int health = 4;

    [Range(-5, 15)]
    public int damage = 1;
    [Range(-25, 150)]
    public int armor = 0;
    [Range(-15, 15)]
    public int movementSpeed = 4;
    // more to be added?

    [Space(10)]
    [Header("Miscelaneous Combat Stats")]
    [Space]
    [Range(0f, 15f)]
    public float comboTimer = 0f;
    [Range(0,10)]
    public int currentCombo = 0;

    [Space(10)]
    [Header("VisualFX")]
    [Space]
    public GameObject HurtVFX;
    public GameObject HurtCriticalVFX, SlowedVFX, DazedVFX, StunnedVFX, DeadFX;

    [Space(10)]
    [Header("AudioFX")]
    [Space]
    public GameObject HurtFX;
    public GameObject HurtCriticalFX, SlowedFX, DazedFX, StunnedFX, DestroySFX;

     [Space(10)]
    [Header("Non-Player Stats")]
    [Space]
    public EntityIdentification entityID = EntityIdentification.undefined;
    public BehaviourType type = BehaviourType.undefined;
    public BehaviourState state = BehaviourState.undefined;

    public enum BehaviourType
    {
        undefined = -10,
        bad = -1,
        neutral = 0,
        good = 1
    }

    public enum BehaviourState
    {
        undefined = -10,
        aggressive = -1,
        passive = 1
    }

    /// <summary>
    /// Helps to filter out the right decision making
    /// </summary>
    public enum EntityIdentification
    {
        undefined = 0,
        slime = 1,
        bandit = 2,
        bear = 3,
        boar = 4,
        player = 100
    }


	//Henrik Hafner
	//Save the Datas from the Player
	public void SavePlayer()
	{
		SaveSystem.SavePlayer(this);
	}


	//Henrik Hafner
	// Load the SaveFiles to the Player
	public void LoadPlayer()
	{
		PlayerData data = SaveSystem.LoadPlayer();

		health = data.health;

		Vector2 position;
		position.x = data.position[0];
		position.y = data.position[1];
		transform.position = position;
	}
}
