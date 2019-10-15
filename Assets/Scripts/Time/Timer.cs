using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Schmidt

public class Timer : MonoBehaviour
{
    [Header("Set Timer")]
    public int minutes;
    public int seconds;
    public DayTime currentDayTime = DayTime.Day;

    Text uiTimer;
    public TimeSpan time;
    bool showTimer;
    bool dayOver;

    void Start()
    {
        uiTimer = GameObject.Find("Timer").GetComponent<Text>();
        time = new TimeSpan(0, minutes, seconds);
    }

    void Update()
    {
        if (dayOver)
        {
            NewDay();
        }

        TimerCountdown();
    }

    void TimerCountdown()
    {

        time = time.Subtract(new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime * 1000)));

        if (currentDayTime == DayTime.Day)
        {      
            if (time.Minutes <= 1 || (time.Minutes == 2 && time.Seconds == 0))
            {
                currentDayTime = DayTime.Panic;
            }      
        }

        if (currentDayTime == DayTime.Panic)
        {
            //enable UI Timer
            uiTimer.text = string.Format("{0:0}:{1:00}", time.Minutes, time.Seconds);

            if ( time.Seconds < 0)
            {
                currentDayTime = DayTime.Night;
            }
        }

        if (currentDayTime == DayTime.Night)
        {
            //disable UI Timer
            uiTimer.text = "";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Wave killed");
                dayOver = true;
            }
        }
    }

    public void SpeedTime(int timeCost)
    {
        time = time.Subtract(new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime * timeCost)));
    }
    

    void NewDay()
    {
        currentDayTime = DayTime.Day;
        time = new TimeSpan(0, minutes, seconds);
        dayOver = false;
    }

    public float TotalTimeInSeconds()
    {
        float total = (time.Minutes * 60) + time.Seconds;
        return total;
    }

    public enum DayTime
    {
        Day,
        Panic,
        Night
    }


}
