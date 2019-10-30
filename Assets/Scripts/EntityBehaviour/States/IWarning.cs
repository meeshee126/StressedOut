using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWarning : MonoBehaviour, IState
{
    EntityBehaviour entity;

    GameObject target => entity.target;

    float count;
  
    public IWarning(EntityBehaviour entity)
    {
        this.entity = entity;
    }

    public bool Condition()
    {
        float distance = GetRadiusToTarget();
        return distance <= this.entity.observationRadius;
    }

    public void Execute()
    {
        Debug.Log("Warning");
        entity.idle = false;
        LookAtTarget();
        TimeToAttack();
    }

    void TimeToAttack()
    {
        count += Time.deltaTime;

        if (count > 1.5f)
        {
            count = 0;
            entity.attack = true;
        }
    }

    void LookAtTarget()
    {
        entity.transform.up = target.transform.position - entity.transform.position;
    }

    float GetRadiusToTarget()
    {
        return Vector3.Distance(entity.gameObject.transform.position, target.gameObject.transform.position);
    }
}
