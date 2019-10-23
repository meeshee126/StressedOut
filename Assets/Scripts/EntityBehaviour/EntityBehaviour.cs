using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    Machine brain = new Machine();

    public GameObject target;
   

    void Start()
    {
        brain.AddState(new IAttack());
        brain.AddState(new IWander());
    }

    void Update()
    {
        brain.Update();
    }
}
