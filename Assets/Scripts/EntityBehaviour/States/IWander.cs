using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IWander : MonoBehaviour, IState
{
    private Entity entity;

    //get target from entityBehavior class
    private GameObject target => entity.target;

    //Set Layer for this state
    private int environmentLayer = 1 << 9;

    private float count;


    public IWander(Entity entity) => this.entity = entity;


    /// <summary>
    /// Check if Target is OUTSIDE the Entity's(this) Radius
    /// </summary>
    /// <returns></returns>
    public bool Condition() =>
        GetTargetDistance() > entity.observationRadius ||
        IsTargetLost() == true;


    public void Execute()
    {
        entity.idle = true;
        entity.aggressive = false;
        entity.stats.movementSpeed = 1;
        entity.charIconAnimator.SetInteger("iconstate", 0);

        if (entity.waitTime > 0f) entity.waitTime -= Time.deltaTime;
        
        if (entity.waitTime <= 0f)
        {
            entity.characterRB.mass = 3;
            Move();
            PostMovingActions();
        }

        // is the Direction the Entity is Facing Not Walkable ?
        if (!Walkable()) AssignRandomRotation();
    }


    /// <summary>
    /// Moving towards facing direction
    /// </summary> 
    private void Move() =>
        entity.characterRB.velocity = entity.facingDirection.transform.up * 100 *
            entity.stats.movementSpeed * Time.deltaTime;


    /// <summary>
    /// Counts The Moving Time and Calls for New Direction Later On.
    /// </summary>
    private void PostMovingActions()
    {
        count += Time.deltaTime;

        // is moving time Over?
        if (count > Random.Range(0f, 5f))
        {
            AssignRandomRotation();
            count = 0;
        }
    }


    /// <summary>
    /// Check if the Entity's Facing Path is Walkable
    /// </summary>
    private bool Walkable()
    {
        Vector3 endpoint = entity.facingDirection.transform.up;

        //ray only hits environment objects
        RaycastHit2D hit = Physics2D.Raycast(entity.facingDirection.transform.position,
            entity.facingDirection.transform.up, 1, environmentLayer);

        //Set raylenght for scene view
        Debug.DrawRay(entity.facingDirection.transform.position, endpoint, Color.red);

        if (hit) return false;

        return true;
    }


    /// <summary>
    /// Changes Entity's Facing Rotation Randomly 
    /// </summary>
    private void AssignRandomRotation()
    {
        entity.characterRB.velocity = new Vector2(0f, 0f);

        // Set a random Waiting time (before entity proceeds to move again).
        entity.waitTime = Random.Range(0f, 5f);
        // Just to prevent the "easy push around" when the entity is standing still
        entity.characterRB.mass = 300;

        // Set Random Facing Direction
        entity.facingDirection.transform.Rotate(0, 0, Random.Range(0, 360));
    }


    /// <summary>
    /// Check if Entity lost his target
    /// </summary>
    /// <returns></returns>
    private bool IsTargetLost() => GetTargetDistance() > entity.lostRadius; //check if target is outside of radius


    /// <summary>
    /// Returns the Distance between
    /// <para>Entity(this) and Target</para>
    /// </summary>
    /// <returns></returns>
    private float GetTargetDistance() => Vector2.Distance(
        entity.gameObject.transform.position, target.gameObject.transform.position);
}
