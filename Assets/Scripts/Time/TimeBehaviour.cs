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
    public List<TimeCost> executionList = new List<TimeCost>();

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
            //clears all saved execuitons in list
            executionList.Clear();
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
            case TimeCost.VeryLowCost: executionList.Add(TimeCost.VeryLowCost); break;
            case TimeCost.LowCost: executionList.Add(TimeCost.LowCost); break;
            case TimeCost.MiddleCost: executionList.Add(TimeCost.MiddleCost); break;
            case TimeCost.HighCost: executionList.Add(TimeCost.HighCost); break;  
        } 
    }

    /// <summary>
    /// decides which timecost will seted after interacting
    /// </summary>
    void SetTimeCost()
    {
        //do not continue when list is empty
        if (executionList.Count == 0)
            return;

        //start counter when list got one entry or more
        count += Time.deltaTime;

        //speed timer for certain amount of time
        if (count <= duration)
        {
            //check first entry in list and get timecost
            timer.SpeedTime(GetValue(executionList[0]));
        }

        if (count > duration)
        {
            //reset counter
            count = 0;

            //remove first entry in list
            executionList.RemoveAt(0);
        }

        // reset time cost when list gets empty
        timeCost = TimeCost.NoCost;
    }

    /// <summary>
    /// Geting value from current time cost 
    /// </summary>
    /// <param name="currentTimeCost"></param>
    /// <returns></returns>
    float GetValue(TimeCost currentTimeCost)
    {
        //check which timecost is activated
        switch (currentTimeCost)
        {
            //returns float to speed the time
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
