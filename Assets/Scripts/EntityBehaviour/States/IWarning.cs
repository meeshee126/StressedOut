using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IWarning : MonoBehaviour, IState
{
    Entity entity;

    //get target from entityBehavior class
    GameObject target => entity.target;

    float count;
  
    public IWarning(Entity entity)
    {
        this.entity = entity;
    }

    /// <summary>
    /// Check this class Condition
    /// </summary>
    /// <returns></returns>
    public bool Condition()
    {
        float distance = GetRadiusToTarget();

        //Check if target is inside entities radius
        return distance <= this.entity.observationRadius;
    }

    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {
        Debug.Log("Warning");
        entity.idle = false;
        entity.stats.movementSpeed = 3;
        //LookAtTarget();
        TimeToAttack();
    }

    /// <summary>
    /// Set entities looking direction
    /// </summary>
    void LookAtTarget()
    {
		Vector2 direction = target.transform.position - entity.transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		entity.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
	}

    /// <summary>
    /// Set entities state to attack after an amount of time
    /// </summary>
    void TimeToAttack()
    {
        count += Time.deltaTime;

        //If target is in radius for an amount of time, entities state will change to attack
        if (count > 1.5f)
        {
            count = 0;
            entity.attack = true;
        }
    }

    /// <summary>
    /// Check distance between entity and target
    /// </summary>
    /// <returns></returns>
    float GetRadiusToTarget()
    {
        return Vector2.Distance(entity.gameObject.transform.position, target.gameObject.transform.position);
    }
}
