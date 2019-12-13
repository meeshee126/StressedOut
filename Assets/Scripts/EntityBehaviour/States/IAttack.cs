using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IAttack : MonoBehaviour, IState
{
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
        entity.chaseRadius > GetTargetDistance() ||
        entity.aggressive == true;


    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {
        Debug.Log("Chasing :" + target.name);
        SwitchStates();
        Chase();

        if(IsTargetLost())
            entity.aggressive = false; //Change state to "Wander"
    }


    /// <summary>
    /// Set Entity to "Attack" State
    /// </summary>
    private void SwitchStates()
    {
        entity.idle = false;
        entity.aggressive = true;
    }


    /// <summary>
    /// Moving towards facing direction
    /// </summary>
    private void Chase() =>
        entity.characterRB.velocity = entity.facingDirection.transform.forward * 100 *
            entity.stats.movementSpeed * Time.deltaTime;


    /// <summary>
    /// Check if Entity lost his target
    /// </summary>
    /// <returns></returns>
    private bool IsTargetLost() => entity.observationRadius > GetTargetDistance(); //check if target is outside of radius


    /// <summary>
    /// Returns the Distance between
    /// <para>Entity(this) and Target</para>
    /// </summary>
    /// <returns></returns>
    private float GetTargetDistance() => Vector2.Distance(
        entity.gameObject.transform.position, target.gameObject.transform.position);
}
