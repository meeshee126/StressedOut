using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : EntityBehaviour
{
    Machine brain = new Machine();

    void Start()
    {
        brain.AddState(new IAttack(this));
        brain.AddState(new IWarned(this));
        
    }

    private void Update()
    {
        brain.Update();
    }

}
