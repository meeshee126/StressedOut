using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float totalTime;

    void Start()
    {
        
    }

    void Update()
    {
        totalTime -= Time.deltaTime;
        float seconds = totalTime % 60;
        Debug.Log(seconds);
    }
}
