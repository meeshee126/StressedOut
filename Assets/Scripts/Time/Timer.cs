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

    [SerializeField]
    Text uiPanicTimer;

    Sun sunScript;
    TimeSpan time;
    bool showPanicTimer;
    bool dayOver;

    void Start()
    {
        sunScript = GameObject.Find("Sunset").GetComponent<Sun>();

        //Set Timer
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

    /// <summary>
    /// Set Timer countdown behaviour
    /// </summary>
    void TimerCountdown()
    {
        //subtract timer by seconds
        time = time.Subtract(new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime * 1000)));

        //Sun rotation clockwise
        transform.Rotate(0, 0, 0.2f);
    }

    /// <summary>
    /// Time behaviour after checking which day time it is
    /// </summary>
    void CheckDayTime()
    {
        //Check current day time
        switch (currentDayTime)
        {
            case DayTime.Day:

                //disable UI Timer
                uiPanicTimer.text = "";

                //if timer gets a certain time
                if (sunScript.slider.normalizedValue <= (panicTimer / 100))
                {
                    //switch to panic mode
                    currentDayTime = Timer.DayTime.Panic;
                }

                break;

            case DayTime.Panic:

                //enable UI Timer
                uiPanicTimer.text = string.Format("{0:0}:{1:00}", time.Minutes, time.Seconds);

                //if timer countdown reach 0
                if (time.Seconds < 0)
                {
                    //switch to night mode
                    currentDayTime = DayTime.Night;
                }

                break;

            case DayTime.Night:

                //disable UI Timer
                uiPanicTimer.text = "";

                //If wave in night is killed
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Debug.Log("Wave killed");

                    //day is over and switch to day mode
                    dayOver = true;
                }

                break;
        }
    }

    /// <summary>
    /// setting for day mode
    /// </summary>
    void NewDay()
    {
        currentDayTime = DayTime.Day;

        //reset timer
        time = new TimeSpan(0, minutes, seconds);

        dayOver = false;
    }

    /// <summary>
    /// Convert hours and minutes to seconds
    /// </summary>
    /// <returns></returns>
    public float TotalTimeInSeconds()
    {
        float total = (time.Minutes * 60) + time.Seconds;
        return total;
    }

    /// <summary>
    /// called after interacting with gameobjects or areas
    /// </summary>
    /// <param name="timeCost"></param>
    public void SpeedTime(int timeCost)
    {
        time = time.Subtract(new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime * timeCost)));

        //faster sun rotation
        sun.transform.Rotate(0, 0, 100);
    }

    /// <summary>
    /// List of all day time variations
    /// </summary>
    public enum DayTime
    {
        Day,
        Panic,
        Night
    }
}
