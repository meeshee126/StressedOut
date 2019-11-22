using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    GameObject sun;
    [SerializeField]
    Image background;
    [SerializeField]
    Text uiPanicTimer;

    [Header("(INFO) current day time")]
    public DayTime currentDayTime = DayTime.Day;

    [HideInInspector]
    public bool dayOver;

    Sun sunScript;
    TimeSpan time;
    bool showPanicTimer;
  
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
        SetBackground();

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
        sun.transform.Rotate(0, 0, -0.2f);
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
    public void NewDay()
    {
        currentDayTime = DayTime.Day;

        //reset timer
        time = new TimeSpan(0, minutes, seconds);

        //Generate new area objects
        GenerateNewMap();

        dayOver = false;
    }

    /// <summary>
    /// Generate new Areas aftter a day ends
    /// </summary>
    void GenerateNewMap()
    {
        List<Transform> areaList = new List<Transform>();
        List<Transform> areaChildList = new List<Transform>();
        List<Transform> areaObjectsList = new List<Transform>();
        GameObject areaLength;

        areaLength = GameObject.Find("Areas");

        //Add all objects from "Areas" to a list
        areaList = areaLength.GetComponentsInChildren<Transform>().ToList();

        //get all objects which are gatherable and add it to a new list
        for (int i = 0; i < areaList.Count; i++)
        {
            if (areaList[i].gameObject.tag == "Gatherable")
            {
                areaChildList.Add(areaList[i]);
            }
        }

        //destroy all gatherable objects
        for (int i = 0; i < areaChildList.Count; i++)
        {
            Destroy(areaChildList[i].gameObject);
        }

        for (int i = 0; i < areaLength.transform.childCount; i++)
        {
            //add all object generators to a list
            areaObjectsList.Add(areaLength.transform.GetChild(i));

            //call ObjectGeneration script for each object generator
            areaObjectsList[i].gameObject.GetComponent<ObjectGeneration>().Generation();
        }

        //clear all lists
        areaList.Clear();
        areaChildList.Clear();
        areaObjectsList.Clear();
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
        sun.transform.Rotate(0, 0, -100);
    }

    /// <summary>
    /// Set Background when changing daytime
    /// </summary>
    void SetBackground()
    {
        //get Colors
        Color day = new Color(0, 0, 0, 0);
        Color dusk = new Color(0, 0, 0, 0.15f);
        Color night = new Color(0, 0, 0, 0.5f);

        //set Colors
        if (currentDayTime == Timer.DayTime.Day) { background.color = day; }     
        if (currentDayTime == Timer.DayTime.Panic) { background.color = dusk; }
        if (currentDayTime == Timer.DayTime.Night) { background.color = night; }
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
