using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWarned : IState
{
  
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
        Debug.Log("Warned");
    }
}
