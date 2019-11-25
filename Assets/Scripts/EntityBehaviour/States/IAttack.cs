using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IAttack : MonoBehaviour, IState
{
    Entity entity;

    //get target from entityBehavior class
    GameObject target => entity.target;

    public IAttack(Entity entity)
    {
        this.entity = entity;
    }
    
    /// <summary>
    /// Check this class Condition
    /// </summary>
    /// <returns></returns>
    public bool Condition()
    {
        //Check if Entity is close to target or is in attack state 
        if (entity.attackRadius >= GetRadiusToTarget() || entity.attack == true)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {
        Debug.Log("Attack" + target.name);
        SwitchStates();
        Chase();

        if(lost())
        {
            //Change state to "Wander"
            entity.attack = false;
        }
    }

    /// <summary>
    /// Set Entity to attack state
    /// </summary>
    void SwitchStates()
    {
        entity.idle = false;
        entity.attack = true;
    }

    /// <summary>
    /// Move toward to target
    /// </summary>
    void Chase()
    {
        //Looks to target
        entity.transform.up = target.transform.position - entity.transform.position;

        //Move to target
        //entity.transform.position += entity.gameObject.transform.up * entity.stats.movementSpeed * Time.deltaTime;
        entity.characterRB.MovePosition(target.transform.position * (entity.stats.movementSpeed * Time.deltaTime));

    }

    /// <summary>
    /// Check if Entity lost his target
    /// </summary>
    /// <returns></returns>
    bool lost()
    {
        //check if target is outside of radius
        if(entity.lostRadius <= GetRadiusToTarget())
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check distance between entity and target
    /// </summary>
    /// <returns></returns>
    float GetRadiusToTarget()
    {
        return Vector3.Distance(entity.gameObject.transform.position, target.gameObject.transform.position);
    }
}
