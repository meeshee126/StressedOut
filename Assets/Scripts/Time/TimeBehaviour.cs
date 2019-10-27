using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Michael Shcmidt

public class TimeBehaviour : MonoBehaviour
{

    [Header("Timecosts (multiplied with milliseconds)")]
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
        TimeCosts();
    }

    void TimeCosts()
    {
        switch(timeCost)
        {
            case TimeCost.veryLowCost:

                count += Time.deltaTime;

                if (count <= durationInSeconds) { timer.SpeedTime(veryLowCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.noCost;
                }

                break;

            case TimeCost.lowCost:

                count += Time.deltaTime;

                if (count <= durationInSeconds) { timer.SpeedTime(lowCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.noCost;
                }

                break;

            case TimeCost.middleCost:

                count += Time.deltaTime;

                if (count <= durationInSeconds) { timer.SpeedTime(middleCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.noCost;
                }

                break;

            case TimeCost.highCost:

                count += Time.deltaTime;

                if (count <= durationInSeconds) { timer.SpeedTime(highCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.noCost;
                }

                break;
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
