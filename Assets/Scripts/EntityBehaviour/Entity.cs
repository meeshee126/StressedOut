using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds Components to the gameObject when script Component is inserted
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]

public class Entity : MonoBehaviour
{
    //Set target = Player
    public GameObject target;

    [Header("Infight checks")]
    public bool attack;
    public bool idle;

    [Header("Radiuses ")]
    public float observationRadius;
    public float attackRadius;
    public float dodgeRadius;
    public float lostRadius;
    [SerializeField]
    private bool showObservationRadius, showAttackRadius, showDodgeRadius, showLostRadius;

    [Header("Entity Configurations")]
    public float movementSpeed;
    public float currentAngle;

    [Header("Timers")]
    public float timeToCompleteCircle;
    public float startWaitTime;
    private float waitTime;

    private int randomSpot;

    [Header("FX")]
    public GameObject HurtFX;
    public GameObject HurtCriticalFX, SlowedFX, DazedFX, StunnedFX;

    [Header("")]
    public Stats stats;
    public CapsuleCollider2D characterCapsuleCollider;
    public Rigidbody2D characterRB;
    public Animator characterAnimator;
    public RaycastHit2D raycastHit2D;
    public Transform[] moveSpots;



    private void Awake()
    {
        //Finds Component in gameObject and uses it
        characterCapsuleCollider = GetComponent<CapsuleCollider2D>();
        characterRB = GetComponent<Rigidbody2D>();
        randomSpot = Random.Range(0, moveSpots.Length);
        characterAnimator = GetComponent<Animator>();
        stats = GetComponent<Stats>();
        waitTime = startWaitTime;
    }


    private void FixedUpdate()
    {
        if (stats.health <= 0)
        {
            // Make a state system of unborn, alive, dead, etc.. probably in Stats.cs
            // By that handle the states and delete enemy only after 5 minutes or so idk..
            Dead();
        }
        if (stats.health > 0)
        {
            Dead();
        }
        if (characterRB != null && characterCapsuleCollider != null && characterAnimator != null){ }
        else if (characterCapsuleCollider == null) Debug.LogWarning("Capsule Collider not attached to " + gameObject.name);
        else if (characterAnimator == null) Debug.LogWarning("Animator not attached to " + gameObject.name);
        else if (characterRB == null) Debug.LogWarning("Rigidbody not attached to " + gameObject.name);
    }

    bool Unborn()
    { return false; }


    bool Alive()
    { return false; }

    
    bool Dead()
    { return false; }


    /// <summary>
    /// Reduces Entity's health by ~damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        Instantiate(HurtFX, gameObject.transform.position, Quaternion.identity);
        stats.health -= damage;
        Debug.Log("Damage DONE!!!  --  " + damage);
    }


    /// <summary>
    /// Stuns Entity for ~duration long in seconds
    /// </summary>
    /// <param name="duration"></param>
    public void TakeStun(float duration)
    { }


    /// <summary>
    /// Slows Entity for ~duration long in seconds
    /// </summary>
    /// <param name="duration"></param>
    public void TakeSlow(float duration)
    { }


    /// <summary>
    /// Dazes Entity for ~duration long in seconds
    /// </summary>
    /// <param name="duration"></param>
    public void TakeDaze(float duration)
    { }


    /// <summary>
    /// Draws The Apropriate Gizmos
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (showObservationRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, observationRadius);
        }
        if (showAttackRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, attackRadius);
        }
        if (showDodgeRadius)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, dodgeRadius);
        }
        if (showLostRadius)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, lostRadius);
        }
    }


    // Add Controlled method (when hit by CC)

    // Probably put all this in independent classes..
    // and / or make a entity behaviour system for them.
}
