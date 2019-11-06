using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class Bear : EntityBehaviour
{
    Machine brain = new Machine();

    void Start()
    {
        //Add states to Bear
        brain.AddState(new IAttack(this));
        brain.AddState(new IWarning(this));
        brain.AddState(new IWander(this));
    }

    void Update()
    {
        //Call functions 
        brain.Update();
    }
}
