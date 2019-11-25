using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public List<TimeCost> executeList = new List<TimeCost>();

    [HideInInspector]
    public TimeCost timeCost = TimeCost.NoCost;

    Timer timer;

    float count;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    void Update()
    {
        CheckDayTime();
        GetTimeCost();
        SetTimeCost();
    }

    void CheckDayTime()
    {
        //no timecost anymore when in panic mode
        if (timer.currentDayTime != Timer.DayTime.Day)
        {
            executeList.Clear();
            return;
        }
    }

    /// <summary>
    /// Time behaviour after interacting with gameobjects and areas
    /// </summary>
    void GetTimeCost()
    {
        //Check which cost is choosed
        switch (timeCost)
        {
            default: timeCost = TimeCost.NoCost; break;
            case TimeCost.VeryLowCost: executeList.Add(TimeCost.VeryLowCost); break;
            case TimeCost.LowCost: executeList.Add(TimeCost.LowCost); break;
            case TimeCost.MiddleCost: executeList.Add(TimeCost.MiddleCost); break;
            case TimeCost.HighCost: executeList.Add(TimeCost.HighCost); break;  
        } 
    }

    void SetTimeCost()
    {
        if (executeList.Count == 0)
            return;

        count += Time.deltaTime;
        if (count <= duration)
        {
            timer.SpeedTime(GetValue(executeList[0]));
        }

        if (count > duration)
        {
            count = 0;
            executeList.RemoveAt(0);
        }
        timeCost = TimeCost.NoCost;
    }

    float GetValue(TimeCost currentTimeCost)
    {
        switch (currentTimeCost)
        {
            default: return 0;
            case TimeCost.VeryLowCost: return veryLowCost;
            case TimeCost.LowCost: return lowCost; 
            case TimeCost.MiddleCost: return middleCost;
            case TimeCost.HighCost: return highCost; 
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
