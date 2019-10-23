using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    Machine brain = new Machine();

    void Start()
    {
        brain.AddState(new IWarned());
        brain.AddState(new IAttack());
    }

    void Update()
    {
        brain.Update();
    }
}
