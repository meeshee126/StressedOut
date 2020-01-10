using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IWarning : MonoBehaviour, IState
{
    private Entity entity;
        //get target from entityBehavior class

    private GameObject target => entity.target;

    private float count;
    
    
    public IWarning(Entity entity) => this.entity = entity;


    /// <summary>
    /// Check if Target is INSIDE the Entity's(this) Observation Radius
    /// </summary>
    /// <returns></returns>
    public bool Condition() =>
        GetTargetDistance() <= entity.observationRadius &&
        entity.aggressive == false;


    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {

        Debug.Log("Warning");

        if (entity.waitTime > 0f) entity.waitTime = 0f;
        entity.idle = false;
        entity.stats.movementSpeed = 3;
        if (entity.aggressive == false) TimeToAttack();
    }


    /// <summary>
    /// Set Entity's facing direction towards Target position
    /// </summary>
    private void LookAtTarget() =>
        entity.facingDirection.transform.LookAt(target.transform);


    /// <summary>
    /// Set entities state to attack after an amount of time
    /// </summary>
    private void TimeToAttack()
    {
        count += Time.deltaTime;
        entity.charIconAnimator.SetInteger("iconstate", 1);
        //If target is in radius for an amount of time, entities state will change to attack
        if (count > 1.5f || GetTargetDistance() < entity.chaseRadius)
        {
            count = 0;
            entity.aggressive = true;
        }
    }


    /// <summary>
    /// Returns the Distance between
    /// <para>Entity(this) and Target</para>
    /// </summary>
    /// <returns></returns>
    private float GetTargetDistance() => Vector2.Distance(
        entity.gameObject.transform.position, target.gameObject.transform.position);
}
