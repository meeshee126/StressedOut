using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Schmidt

public class Timer : MonoBehaviour
{
    [SerializeField]
    int minutes, seconds;

    Text uiTimer;
    TimeSpan time;
    bool showTimer;
    bool dayOver;

    public DayTime currentDayTime = DayTime.Day;

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

    void NewDay()
    {
        currentDayTime = DayTime.Day;
        time = new TimeSpan(0, minutes, seconds);
        dayOver = false;
    }

    public enum DayTime
    {
        Day = 0,
        Panic = 1,
        Night = 2
    }
}
