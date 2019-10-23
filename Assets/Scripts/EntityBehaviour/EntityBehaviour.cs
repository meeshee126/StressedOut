using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    Machine brain = new Machine();

    void Start()
    {
        brain.AddState(new IAttack(this));
        brain.AddState(new IWander(this));
    }

    void Update()
    {
        brain.Update();
    }
}
