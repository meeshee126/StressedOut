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

    [Header("Set Panic Timer (in percent)")]
    public float panicTimer;

    [Header("")]
    public DayTime currentDayTime = DayTime.Day;

    [SerializeField]
    GameObject sun;

    public Text uiPanicTimer;

    TimeSpan time;
    bool showPanicTimer;
    bool dayOver;

    void Start()
    {
        time = new TimeSpan(0, minutes, seconds);
    }

    void Update()
    {
        TimerCountdown();
        CheckDayTime();

        if (dayOver)
        {
            NewDay();
        }
    }

    void TimerCountdown()
    {
        time = time.Subtract(new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime * 1000)));
        sun.transform.Rotate(0, 0, 0.2f);
    }

    void CheckDayTime()
    {
        if (currentDayTime == DayTime.Day)
        {
            //disable UI Timer
            uiPanicTimer.text = "";
        }

        if (currentDayTime == DayTime.Panic)
        {
            //enable UI Timer
            uiPanicTimer.text = string.Format("{0:0}:{1:00}", time.Minutes, time.Seconds);

            if (time.Seconds < 0)
            {
                currentDayTime = DayTime.Night;
            }
        }

        if (currentDayTime == DayTime.Night)
        {
            //disable UI Timer
            uiPanicTimer.text = "";

            if (Input.GetKeyDown(KeyCode.Alpha1))
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

    public float TotalTimeInSeconds()
    {
        float total = (time.Minutes * 60) + time.Seconds;
        return total;
    }

    public void SpeedTime(int timeCost)
    {
        time = time.Subtract(new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime * timeCost)));
        sun.transform.Rotate(0, 0, 100);
    }

    public enum DayTime
    {
        Day,
        Panic,
        Night
    }
}
