using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Michael Schmidt

public class TimeBehaviour : MonoBehaviour
{
    [Header("Timecosts")]
    [SerializeField]
    float veryLowCost;
    [SerializeField]
    float lowCost;
    [SerializeField]
    float middleCost;
    [SerializeField]
    float highCost;

    [Header("Duration in Seconds")]
    [SerializeField]
    //how long the multiplication should run
    float duration;

    [Header("Set Time Cost for interactions")]
    public TimeCost winQuickTimeEvent;
    public TimeCost loseQuickTimeEvent;
    public TimeCost crafting;
    public TimeCost areaChanging;

    [Header("(INFO) Current Time Cost")]
    public TimeCost timeCost = TimeCost.NoCost;

    Timer timer;

    float count;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    void Update()
    {
        //no timecost anymore when in panic mode
        if (timer.currentDayTime != Timer.DayTime.Day)
        {
            timeCost = TimeCost.NoCost;
            return;
        }
           

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
                if (count <= duration) { timer.SpeedTime(veryLowCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.NoCost;
                }

                break;

            case TimeCost.LowCost:

                count += Time.deltaTime;

                if (count <= duration) { timer.SpeedTime(lowCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.NoCost;
                }

                break;

            case TimeCost.MiddleCost:

                count += Time.deltaTime;

                if (count <= duration) { timer.SpeedTime(middleCost); }

                else
                {
                    count = 0;
                    timeCost = TimeCost.NoCost;
                }

                break;

            case TimeCost.HighCost:

                count += Time.deltaTime;

                if (count <= duration) { timer.SpeedTime(highCost); }

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
