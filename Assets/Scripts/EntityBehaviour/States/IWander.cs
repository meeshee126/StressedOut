using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWander : IState
{
    EntityBehaviour entity;

    public IWander(EntityBehaviour entity)
    {
        this.entity = entity;
    }

    public bool Condition()
    {
        if (Input.GetKey(KeyCode.K))
        {
            return true;
        }

        return false;
    }

    public void Execute()
    {
        Debug.Log("K");
    }
}
