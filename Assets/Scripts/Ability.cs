using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ability with Attributes Creation
/// </summary>
public class Ability : MonoBehaviour
{

    // Add enum which handles attacking friendly units too
    // Add enum Crowd control (stun, daze, knock)

    //for (int i = 0; i < abilities.Length; i++) abilities[i].Cooldown -= Time.deltaTime;


    [Header("Identification")]
    public string CastName;

    [Header("Main Attributes")]
    public bool HitAllEntities;
    public int Damage;
    public int Bursts;

    [Header("Timers")]
    public float CastingTime;
    public float Duration;
    public float BurstWait;
    public float StartBurstWait;
    public float ChildAbilityWait;

    [Header("Area Ranges")]
    public float ColliderAreaAxisX;
    public float ColliderAreaAxisY;
    public float ColliderAreaRadius;

    [Header("Other Attributes")]
    public GameObject ChildAbility;
    public LayerMask WhatCanItHit;
    // Addjust mask in code to change whenever it's about switch to hit everything and switch to hit only enemies
    public Transform attackDirection;

    // Add param: CrowdControl cc
    // Add param: bool lifesteal
    // Add param: bool shieldsteal


    // MATH SECTION ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ MATH SECTION
    private void Update()
    {
        if (Duration <= 0f) Destroy(gameObject);

        // Animation: Ability-Casting
        CastingTime -= Time.deltaTime;
        if (CastingTime <= 0f)
        {
            Duration -= Time.deltaTime;
            BurstWait -= Time.deltaTime;
            ChildAbilityWait -= Time.deltaTime;
            if (BurstWait <= 0f && Bursts > 0)
            {
                CastAttack();
                Bursts--;
                BurstWait = StartBurstWait;
            }
            if (ChildAbilityWait <= 0 && ChildAbility != null) Instantiate(ChildAbility);
        }
    }


    /// <summary>
    /// Where the magic happens (once the casting phase is finished)
    /// </summary>
    /// <param name="abilityToCast"></param>
    /// <param name="abilitysStandards"></param>
    public void CastAttack()
    {
        Collider2D[] collisionsInCastArea = Physics2D.OverlapBoxAll(
            transform.position, new Vector2(ColliderAreaAxisX, ColliderAreaAxisY),
            Quaternion.identity.x, LayerMask.NameToLayer("Entity"));

        for (int i = 0; i < collisionsInCastArea.Length; i++)
        {
            // If the Entity.state == Neutral or Bad or Good & Entity.type  == Aggresive
            if (((collisionsInCastArea[i].CompareTag("Enemy")) ||
                (collisionsInCastArea[i].CompareTag("Neutral") &&

                (collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Aggressive &&
                (collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Neutral ||
                collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Bad ||
                collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Good)) ||

                (collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Passive &&
                (collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Bad ||
                collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Neutral)))))
            {
                collisionsInCastArea[i].GetComponent<Entity>().TakeDamage(Damage);
            }
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


    ///// <summary>
    ///// Casual Block XY (with child ability)
    ///// </summary>
    //public void Initialize(bool hitAll, string abilityName, int abilityDamage, int abilityBursts,
    //    float abilityCastingTime, float abilityDuration, float abilityBurstWait, float abilityChildCastWait,
    //    float abilityRangeAxisX, float abilityRangeAxisY, GameObject childCast, LayerMask whatCanTheAbilityHit)
    //{
    //    HitAllEntities = hitAll;
    //    CastName = abilityName;
    //    Damage = abilityDamage;
    //    Bursts = abilityBursts;
    //    CastingTime = abilityCastingTime;
    //    Duration = abilityDuration;
    //    BurstWait = abilityBurstWait;
    //    StartBurstWait = abilityBurstWait;
    //    ChildAbilityWait = abilityChildCastWait;
    //    ColliderAreaAxisX = abilityRangeAxisX;
    //    ColliderAreaAxisY = abilityRangeAxisY;
    //    ChildAbility = childCast;
    //    WhatCanItHit = whatCanTheAbilityHit;
    //}

    ///// <summary>
    ///// Casual Block XY
    ///// </summary>
    //public void Initialize(bool hitAll, string abilityName, int abilityDamage, int abilityBursts,
    //    float abilityCastingTime, float abilityDuration, float abilityBurstWait,
    //    float abilityRangeAxisX, float abilityRangeAxisY, LayerMask whatCanTheAbilityHit)
    //{
    //    HitAllEntities = hitAll;
    //    CastName = abilityName;
    //    Damage = abilityDamage;
    //    Bursts = abilityBursts;
    //    CastingTime = abilityCastingTime;
    //    Duration = abilityDuration;
    //    BurstWait = abilityBurstWait;
    //    StartBurstWait = abilityBurstWait;
    //    ColliderAreaAxisX = abilityRangeAxisX;
    //    ColliderAreaAxisY = abilityRangeAxisY;
    //    WhatCanItHit = whatCanTheAbilityHit;
    //}
}

