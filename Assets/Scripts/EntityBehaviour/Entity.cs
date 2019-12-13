using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Michael Schmidt
// Dimitrios Kitsikidis

//Adds Components to the gameObject when script Component is inserted
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Stats))]

public class Entity : MonoBehaviour
{
    [Header("Behaviour Configurations")]
    //Set target = Player
    public GameObject target;
    public GameObject facingDirection;
    
    [Space(10)]
    [Header("Radiuses")]
    [Space]
    public float observationRadius;
    public float attackRadius, dodgeRadius, lostRadius;
    [SerializeField]
    private bool showObservationRadius, showAttackRadius, showDodgeRadius, showLostRadius;
    public float currentAngle;

    [Space(10)]
    [Header("Infight checks")]
    [Space]
    public bool attack;
    public bool idle;


    [Space(10)]
    [Header("Timers")]
    [Space]
    public float timeToCompleteCircle;
    public float timeBeforeDestroy;
    public float waitTime;

    private int randomSpot;

    [Space(10)]
    [Header("FX")]
    [Space]
    public GameObject HurtFX;
    public GameObject HurtCriticalFX, SlowedFX, DazedFX, StunnedFX;

    [Header("Audio")]
    [SerializeField]
    GameObject DestroySFX;

    [Space(10)]
    [Header("Other..")]
    [Space]
    public CapsuleCollider2D characterCapsuleCollider;
    public Animator characterAnimator;
    public Rigidbody2D characterRB;
    public Stats stats;
    public AudioSource audioSource;

    private void Awake()
    {
        //Finds Component in gameObject and uses it
        characterCapsuleCollider = GetComponent<CapsuleCollider2D>();
        characterAnimator = GetComponent<Animator>();
        characterRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        target = GameObject.Find("Player");
        stats = GetComponent<Stats>();
    }


    private void FixedUpdate()
    {
        if (stats.health <= 0)
        {
            if (timeBeforeDestroy > 0f) timeBeforeDestroy -= Time.deltaTime;
            if (timeBeforeDestroy <= 0f)
            {
                //Michael Schmidt
                //Play audio
                if (DestroySFX != null) Instantiate(DestroySFX, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }


            //Dead();
            // Make a state system of unborn, alive, dead, etc.. probably in Stats.cs
            // By that handle the states and delete enemy only after 5 minutes or so idk..
        }
        else if (stats.health > 0)
        {
            if (characterRB != null && characterCapsuleCollider != null && characterAnimator != null) { }
            else if (characterCapsuleCollider == null) Debug.LogWarning("Capsule Collider not attached to " + gameObject.name);
            else if (characterAnimator == null) Debug.LogWarning("Animator not attached to " + gameObject.name);
            else if (characterRB == null) Debug.LogWarning("Rigidbody not attached to " + gameObject.name);
        }
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
        if(HurtFX != null) Instantiate(HurtFX, gameObject.transform.position, Quaternion.identity);
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
