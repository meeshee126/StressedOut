using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    Machine brain = new Machine();

    void Start()
    {
        brain.AddState(new IWander());
        brain.AddState(new IAttack());
    }

    void Update()
    {
        brain.Update();
    }
}
