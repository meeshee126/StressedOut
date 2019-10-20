﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Michael Shcmidt

public class TimeBehaviour : MonoBehaviour
{

    [Header("Timecosts multiplied in milliseconds")]
    [SerializeField]
    int veryLowCost;
    [SerializeField]
    int lowCost;
    [SerializeField]
    int middleCost;
    [SerializeField]
    int highCost;

    [Header("")]
    [SerializeField]
    float durationInSeconds;

    public TimeCost timeCost = TimeCost.noCost;

    float count;

    Timer timer;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    void Update()
    {
        VeryLowTimeCost(veryLowCost, durationInSeconds);
        LowTimeCost(lowCost, durationInSeconds);
        MiddleTimeCost(middleCost, durationInSeconds);
        HighTimeCost(highCost, durationInSeconds);
    }

    void VeryLowTimeCost(int veryLowCost,float duration)
    {
        if(timeCost == TimeCost.veryLowCost)
        {
            count += Time.deltaTime;

            if (count <= duration) { timer.SpeedTime(veryLowCost); }

            else
            {
                count = 0;
                timeCost = TimeCost.noCost;
            }
        }
    }

    void LowTimeCost(int lowCost, float duration)
    {
        if (timeCost == TimeCost.lowCost)
        {
            count += Time.deltaTime;

            if (count <= duration) { timer.SpeedTime(lowCost); }

            else
            {
                count = 0;
                timeCost = TimeCost.noCost;
            }
        }
    }

    void MiddleTimeCost(int middleCost, float duration)
    {
        if (timeCost == TimeCost.middleCost)
        {
            count += Time.deltaTime;

            if (count <= duration) { timer.SpeedTime(middleCost); }

            else
            {
                count = 0;
                timeCost = TimeCost.noCost;
            }
        }
    }

    void HighTimeCost(int highCost, float duration)
    {
        if (timeCost == TimeCost.highCost)
        {
            count += Time.deltaTime;

            if (count <= duration) { timer.SpeedTime(highCost); }

            else
            {
                count = 0;
                timeCost = TimeCost.noCost;
            }
        }
    }

    public enum TimeCost
    {
        noCost,
        veryLowCost,
        lowCost,
        middleCost,
        highCost
    }
}