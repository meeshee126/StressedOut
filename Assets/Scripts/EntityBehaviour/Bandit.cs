using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class Bandit : EntityBehaviour
{
    Machine brain = new Machine();

    void Start()
    {
        //Add states to Bandit
        brain.AddState(new IWander(this));
        brain.AddState(new IAttack(this));
    }

    void Update()
    {
        //Call functions
        brain.Update();
    }
}
