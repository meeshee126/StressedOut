using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKey(KeyCode.J))
        {
            return true;
        }
                
        return false;
    }

    public void Execute()
    {
        Debug.Log("Attack");
    }
}
