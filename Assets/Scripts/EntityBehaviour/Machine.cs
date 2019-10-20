using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine 
{
    protected List<IState> states = new List<IState>();
    
    public void AddState(IState newState)
    {
        this.states.Add(newState);
    }

    public void Update()
    {

        foreach (IState state in states)
        {
            if(state.Condition())
            {
                state.Execute();
                break;
            }
        }
    }
}
