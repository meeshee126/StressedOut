using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IAttack : MonoBehaviour, IState
{
    private float count;
    private float count_e;

    private Entity entity;

    //get target from entityBehavior class

    private GameObject target => entity.target;


    public IAttack(Entity entity) => this.entity = entity;


    /// <summary>
    /// Check if Target is INSIDE the Entity's(this) Chase Radius
    /// <para>or is Aggressive</para>
    /// </summary>
    /// <returns></returns>
    public bool Condition() =>
        entity.isCasting == true;


    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {
        if (count <= 0f) count_e = count = entity.castTimer / 4f;
        // Don't move while casting
        entity.characterRB.velocity = new Vector2(0f, 0f);

        // CountDownTimeNextToHim
        count_e -= Time.deltaTime;
            // IF TimeNextToHimMet?
        if (count_e <= 0f)
        {
            // LookTowardsPlayer
            LookAtTarget();
            entity.castTimer_e -= Time.deltaTime;
            // Cast time
            if (entity.castTimer_e <= 0f)
            {
                // Reset timers
                entity.castTimer_e = entity.castTimer;
                count_e = count;

                // Cast Attack
                Instantiate(entity.abilityToCast,
                    entity.facingDirection.transform.position,
                    entity.facingDirection.transform.rotation);
                entity.isCasting = false;
            }
        }

    }


    /// <summary>
    /// Set Entity's facing direction towards Target position
    /// </summary>
    private void LookAtTarget() =>
        entity.facingDirection.transform.LookAt(target.transform);


    /// <summary>
    /// Returns the Distance between
    /// <para>Entity(this) and Target</para>
    /// </summary>
    /// <returns></returns>
    private float GetTargetDistance() => Vector2.Distance(
        entity.gameObject.transform.position, target.gameObject.transform.position);
}
