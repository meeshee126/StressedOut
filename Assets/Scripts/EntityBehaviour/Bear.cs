using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class Bear : Entity
{
    Machine brain = new Machine();

    void Start()
    {
        //Add states to Bear
        brain.AddState(new IAttack(this));
    }

    void Update()
    {
        //Call functions 
        brain.Update();
    }
}
