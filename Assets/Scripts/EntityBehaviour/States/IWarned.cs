using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWarned : MonoBehaviour, IState
{
    EntityBehaviour entity;

    GameObject target => entity.target;

    public IWarned(EntityBehaviour entity)
    {
        this.entity = entity;
    }
  
    public bool Condition()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            return true;
        }

        return false;
    }

    public void Execute()
    {
        Debug.Log("Warned" + target.name + this.entity.name);
    }
}
