using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IAttack : MonoBehaviour, IState
{
    EntityBehaviour entity;

    GameObject target => entity.target;

    public IAttack(EntityBehaviour entity)
    {
        this.entity = entity;
    }
    

    public bool Condition()
    {
        if (entity.attackRadius >= GetRadiusToTarget() || entity.attack == true)
        {
            return true;
        }

        return false;
    }

    public void Execute()
    {
        Debug.Log("Attack " + target.name);

        entity.idle = false;
        
        Chase();

        if(lost())
        {
            entity.attack = false;

        }
    }

    void Chase()
    {
        entity.attack = true;
        entity.transform.up = target.transform.position - entity.transform.position;
        entity.transform.position += entity.gameObject.transform.up * entity.movementSpeed * Time.deltaTime;
    }

    bool lost()
    {
        if(entity.lostRadius <= GetRadiusToTarget())
        {
            return true;
        }

        return false;
    }

    float GetRadiusToTarget()
    {
        return Vector3.Distance(entity.gameObject.transform.position, target.gameObject.transform.position);
    }
}
