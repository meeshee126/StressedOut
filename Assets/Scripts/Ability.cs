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
    public float Cooldown;
    public float BurstWait;

    [Header("Area Ranges")]
    public float ColliderAreaAxisX;
    public float ColliderAreaAxisY;
    public float ColliderAreaRadius;

    [Header("Other Attributes")]
    public GameObject CastEffect;
    public LayerMask WhatCanItHit;
    // Addjust mask in code to change whenever it's about switch to hit everything and switch to hit only enemies
    public Transform attackDirection;

    // Add param: CrowdControl cc
    // Add param: bool lifesteal
    // Add param: bool shieldsteal

    /// <summary>
    /// Casual Block XY
    /// </summary>
    public void Initialize(bool hitAll, string abilityName, int abilityDamage,
        float abilityCastingTime, float abilityDuration, float abilityCooldown, float abilityRangeAxisX,
        float abilityRangeAxisY, GameObject abilityEffect, LayerMask whatCanTheAbilityHit)
    {
        HitAllEntities = hitAll;
        CastName = abilityName;
        Damage = abilityDamage;
        CastingTime = abilityCastingTime;
        Duration = abilityDuration;
        Cooldown = abilityCooldown;
        ColliderAreaAxisX = abilityRangeAxisX;
        ColliderAreaAxisY = abilityRangeAxisY;
        CastEffect = abilityEffect;
        WhatCanItHit = whatCanTheAbilityHit;
    }

    // MATH SECTION ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ ~~ MATH SECTION
    private void Update()
    {
        if (Duration <= 0f) Destroy(gameObject);

        CastingTime -= Time.deltaTime;
        if (CastingTime <= 0f)
        {
            Cooldown -= Time.deltaTime;
            Duration -= Time.deltaTime;
            CastAttack();
        }
    }


    /// <summary>
    /// Where the magic happens (once the casting phase is finished)
    /// </summary>
    /// <param name="abilityToCast"></param>
    /// <param name="abilitysStandards"></param>
    public void CastAttack()
    {
        // Animation: Ability-Casting
        if (CastingTime <= 0f)
        {
            do
            {
                Collider2D[] collisionsInCastArea = Physics2D.OverlapCircleAll(transform.forward, ColliderAreaRadius, WhatCanItHit);
                //for (int i = 0; i < collisionsInCastArea.Length; i++)
                //{
                //    // If the Entity.state == Neutral or Bad or Good & Entity.type  == Aggresive
                //    if (((collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Neutral ||
                //        collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Bad ||
                //        collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Good) &&
                //        collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Aggressive) ||

                //        // If the Entity.state == Bad & Entity.type == Passive
                //        (collisionsInCastArea[i].GetComponent<Stats>().state == Stats.BehaviourState.Bad &&
                //        collisionsInCastArea[i].GetComponent<Stats>().type == Stats.BehaviourType.Passive))
                //    {
                //        collisionsInCastArea[i].GetComponent<Entity>().TakeDamage(Damage);
                //    }
                //    else if (HitAllEntities)
                //    {
                //        collisionsInCastArea[i].GetComponent<Entity>().TakeDamage(Damage);
                //    }
                //}

                //Instantiate(abilityToCast, gameObject.transform.position + new Vector3(1, 1, 0), Quaternion.identity);
                // The instatiation has already happened...
            } while (Duration >= 0);

            //Duration = /*abilitysStandards.*/Duration;
            //We don't really want it to reset do we ?... ya idk i will look at it

        }
    }
}

