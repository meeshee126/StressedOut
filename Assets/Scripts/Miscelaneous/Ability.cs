using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Dimitrios Kitsikidis
/// <summary>
/// Holds all values that define an Ability
/// Holds the ability behaviour upon instantiation
/// </summary>
public class Ability : MonoBehaviour
{
    [Header("Identification")]
    public string CastName;

    [Header("Main Attributes")]
    public bool isCircle;
    public bool HitAllEntities;
    public int Damage;
    public int Bursts;

    [Header("Timers")]
    public float CastingTime;
    public float Duration;
    public float BurstWait;
    public float Cooldown;
    public float StartBurstWait;
    public float ChildAbilityWait;

    [Header("Area Ranges")]
    public bool isBoxDirectionalExpanding;
    public float Position_X;
    public float Position_Y;
    public float boxColliderX, boxColliderY, circleColliderRadius;

    [Header("Other Attributes")]
    public GameObject ChildAbility;
    public LayerMask WhatCanItHit;

    [Header("Temporal Attributes")]
    public SpriteRenderer spriteRenderer;
    public Sprite Casting, Idle, Attack;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    /// <summary>
    /// Runs and Filters through the timers to handle the ability behaviours
    /// </summary>
    private void Update()
    {
        Position_X = transform.position.x;
        Position_Y = transform.position.y;

        if (Duration <= 0f && Bursts <= 0) Destroy(gameObject);

        // Animation: Ability-Casting
        CastingTime -= Time.deltaTime;
        if (CastingTime > 0f)
        {
            spriteRenderer.sprite = Casting;
        }
        if (CastingTime <= 0f)
        {
            spriteRenderer.sprite = Idle;
            Duration -= Time.deltaTime;
            BurstWait -= Time.deltaTime;
            ChildAbilityWait -= Time.deltaTime;
            if (BurstWait <= 0f && Bursts > 0)
            {
                spriteRenderer.sprite = Attack;
                CastAttack();
                Bursts--;
                BurstWait = StartBurstWait;
            }
            if (ChildAbilityWait <= 0 && ChildAbility != null) Instantiate(ChildAbility);
        }
    }


    /// <summary>
    /// Where the magic happens (once the casting phase is finished)
    /// Makes a list of collisions..
    /// .. Filters the entities that shall be harmed
    /// and calls the entity's method to harm them.
    /// <para>WARNING: Functionality is currently only restricted to box shaped abilities</para>
    /// </summary>
    /// <param name="abilityToCast"></param>
    /// <param name="abilitysStandards"></param>
    public void CastAttack()
    {
        Collider2D[] collisionsInCastArea = Physics2D.OverlapBoxAll(
            new Vector3(Position_X, Position_Y), new Vector2(boxColliderX, boxColliderY), 0f, WhatCanItHit);

        for (int i = 0; i < collisionsInCastArea.Length; i++)
        {
            // If the Entity.state == Neutral or Bad or Good & Entity.type  == Aggresive
            if (collisionsInCastArea[i].GetComponent<Entity>())
                collisionsInCastArea[i].GetComponent<Entity>().TakeDamage(Damage);

            else if(collisionsInCastArea[i].GetComponent<Player>())
                collisionsInCastArea[i].GetComponent<Player>().TakeDamage(Damage);

            else if (HitAllEntities)
            {
                collisionsInCastArea[i].GetComponent<Entity>().TakeDamage(Damage);
            }
        }

        //Instantiate(abilityToCast, gameObject.transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        // The instatiation has already happened...

        //Duration = /*abilitysStandards.*/Duration;
        //We don't really want it to reset do we ?... ya idk i will look at it

    }


    /// <summary>
    /// Allows for in-scene visibility of the ability area etc.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Position_X = transform.position.x;
        Position_Y = transform.position.y;
        Gizmos.color = Color.blue;

        if (isBoxDirectionalExpanding)
        {
            BoxDirectionalExpanding();
        }

        if (!isCircle)
        {
            Gizmos.DrawWireCube(
                new Vector3(Position_X, Position_Y),
                new Vector3(boxColliderX, boxColliderY));
        }

        if (isCircle)
        {
            Gizmos.DrawWireSphere(
                new Vector3(Position_X, Position_Y),
                circleColliderRadius);
        }
    }
    
    
    /// <summary>
    /// ...UNDER CONSTRUCTION
    /// Handles the ability's - collider's size in the directional expansion
    /// </summary>
    void BoxDirectionalExpanding ()
    {
        Debug.LogWarning("This is currently under Construction and hereby NOT FUNCTIONING yet");
        //if (boxColliderX > 1f && boxColliderY > 1f)
        //{

        //}
        //if (boxColliderY > 1f)
        //{

        //}
        //if (boxColliderX < -1f)
        //{

        //}
        //if (boxColliderY < -1f)
        //{

        //}
        //Gizmos.DrawWireCube(
        //       new Vector3(transform.position.x + ((boxColliderX / 2f) - 0.5f),
        //       transform.position.y + ((boxColliderY / 2f) - 0.5f)),
        //       new Vector3(boxColliderX, boxColliderY));
    }


    /// <summary>
    /// Use this to apply values from one ability to another.
    /// </summary>
    /// <param name="Values"></param>
    public void SetValues(Ability Values)
    {
        name = Values.name;
        CastName = Values.CastName;
        isCircle = Values.isCircle;
        HitAllEntities = Values.HitAllEntities;
        Damage = Values.Damage;
        Bursts = Values.Bursts;

        CastingTime = Values.CastingTime;
        Duration = Values.Duration;
        BurstWait = Values.BurstWait;
        Cooldown = Values.Cooldown;
        StartBurstWait = Values.StartBurstWait;
        ChildAbilityWait = Values.ChildAbilityWait;

        isBoxDirectionalExpanding = Values.isBoxDirectionalExpanding;
        Position_X = Values.Position_X;
        Position_Y = Values.Position_Y;
        boxColliderX = Values.boxColliderX;
        boxColliderY = Values.boxColliderY;
        circleColliderRadius = Values.circleColliderRadius;

        ChildAbility = Values.ChildAbility;
        WhatCanItHit = Values.WhatCanItHit;
    }
}


 //||
 //               (collisionsInCastArea[i].CompareTag("Neutral") &&

 //               (collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Aggressive &&
 //               (collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Neutral ||
 //               collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Bad ||
 //               collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Good)) ||

 //               (collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Passive &&
 //               (collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Bad ||
 //               collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Neutral))) ||

 //               (collisionsInCastArea[i].CompareTag("Friendly") &&
 //               collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Aggressive)