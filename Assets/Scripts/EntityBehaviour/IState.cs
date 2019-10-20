using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// Check condition from each state
    /// </summary>
    /// <returns></returns>
    bool Condition();

    /// <summary>
    /// Execute state from state which condition is true
    /// </summary>
    void Execute();
}
