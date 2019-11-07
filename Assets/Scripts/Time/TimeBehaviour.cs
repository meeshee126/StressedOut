using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Michael Schmidt

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
    //how long the multiplication should run
    float durationInSeconds;

    Timer timer;

    public TimeCost timeCost = TimeCost.NoCost;

    float count;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    void Update()
    {
        //no timecost anymore when in panic mode
        if (timer.currentDayTime == Timer.DayTime.Panic)
            return;

        TimeCosts();
    }

    /// <summary>
    /// Time behaviour after interacting with gameobjects and areas
    /// </summary>
    void TimeCosts()
    {
        //Check which cost is choosed
        switch(timeCost)
        {
            case TimeCost.VeryLowCost:

                count += Time.deltaTime;

                //multiplicate current time with choosed timecost for a certain amount of time
                if (count <= durationInSeconds) { timer.SpeedTime(veryLowCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.NoCost;
                }

                break;

            case TimeCost.LowCost:

                count += Time.deltaTime;

                if (count <= durationInSeconds) { timer.SpeedTime(lowCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.NoCost;
                }

                break;

            case TimeCost.MiddleCost:

                count += Time.deltaTime;

                if (count <= durationInSeconds) { timer.SpeedTime(middleCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.NoCost;
                }

                break;

            case TimeCost.HighCost:

                count += Time.deltaTime;

                if (count <= durationInSeconds) { timer.SpeedTime(highCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.NoCost;
                }

                break;
        }
    }

    /// <summary>
    /// List of all cost variations
    /// </summary>
    public enum TimeCost
    {
        NoCost,
        VeryLowCost,
        LowCost,
        MiddleCost,
        HighCost
    }
}
