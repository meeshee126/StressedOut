using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : EntityBehaviour
{
    Machine brain = new Machine();

    public List<ScriptableObject> state = new List<ScriptableObject>();

    void Start()
    {
        
        
    }

    void Update()
    {
        brain.Update();
    }
}
