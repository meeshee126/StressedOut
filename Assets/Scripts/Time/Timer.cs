﻿using System;
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
    [SerializeField]
    Text uiDay;
    [SerializeField]
    Text uiNewDay;
    [SerializeField]
    Animator fadeToBlack;

    [Header("(INFO) current day time")]
    public DayTime currentDayTime = DayTime.Day;

    [HideInInspector]
    public int dayCounter;

    Sun sunScript;
    NightWavesGen nightWaves;

    public AudioSource backgroundMusic;
    public AudioClip dayMusic;
    public AudioClip nightMusic;
    

    public TimeSpan time;

    bool showPanicTimer;
    bool dayOver;
    bool musicSeted;
  
    void Start()
    {
        sunScript = GameObject.Find("Sunset").GetComponent<Sun>();
        nightWaves = GameObject.Find("GameManager").GetComponent<NightWavesGen>();
        
        //Set Day
        SetDay();

        //Set Timer
        time = new TimeSpan(0, minutes, seconds);
    }

    void Update()
    {      
        SetDay();
        TimerCountdown();
        CheckDayTime();
        SetBackground();

        if (dayOver)
        {
            NewDay();
        }
    }

    /// <summary>
    /// Set current day
    /// </summary>
    void SetDay()
    {
        if(uiDay.text == "")
        {
            dayCounter = 1;
        }

        uiDay.text = dayCounter.ToString();
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

                if (!musicSeted)
                {
                    backgroundMusic.clip = dayMusic;
                    backgroundMusic.Play();
                    backgroundMusic.pitch = 1f;
                    musicSeted = true;
                }
                

                //disable UI Timer
                uiPanicTimer.text = "";
                
                //if timer gets a certain time
                if (sunScript.slider.normalizedValue <= (panicTimer / 100))
                {
                    musicSeted = false;

                    //switch to panic mode
                    currentDayTime = Timer.DayTime.Panic;
                }

                break;

            case DayTime.Panic:

                //enable UI Timer
                uiPanicTimer.text = string.Format("{0:0}:{1:00}", time.Minutes, time.Seconds);

                if (!musicSeted)
                {
                    if(backgroundMusic.clip != dayMusic)
                    {
                        backgroundMusic.clip = dayMusic;
                        backgroundMusic.Play();
                    }
                    backgroundMusic.pitch = 1.3f;
                    musicSeted = true;
                }
                

                //if timer gets a certain time
                if (sunScript.slider.normalizedValue > (panicTimer / 100))
                {
                    musicSeted = false;

                    //switch to day mode
                    currentDayTime = Timer.DayTime.Day;
                }

                //if timer countdown reach 0
                if (time.Seconds < 0)
                {
                    musicSeted = false;

                    //switch to night mode
                    currentDayTime = DayTime.Night;
                }

                break;

            case DayTime.Night:

                //disable UI Timer
                uiPanicTimer.text = "";

                if (!musicSeted)
                {
                    backgroundMusic.clip = nightMusic;
                    backgroundMusic.Play();
                    backgroundMusic.pitch = 1f;
                    musicSeted = true;
                }

                //if timer gets a certain time
                if (sunScript.slider.normalizedValue > (panicTimer / 100))
                {
                    musicSeted = false;

                    //switch to day mode
                    currentDayTime = Timer.DayTime.Day;
                }

                //If wave in night is killed
                if (nightWaves.areEnemiesDead)
                {
                    Debug.Log("Wave killed");

                    //day is over and switch to day mode
                    dayOver = true;


                    nightWaves.areEnemiesDeadCheck() = false;
                }
                break;
        }
    }

    /// <summary>
    /// setting for day mode
    /// </summary>
    public void NewDay()
    {
        //Fade Out
        fadeToBlack.SetBool("FadeToBlack", true);

        backgroundMusic.Stop();

        StartCoroutine(FadeIn());


        dayOver = false;
        
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.6f);

        //counting day
        dayCounter++;

        //Write new day count on black screen
        uiNewDay.text = dayCounter.ToString();

        //reset timer
        time = new TimeSpan(0, minutes, seconds);
        //reset sun
        sunScript.sliderSeted = false;

        //Generate new area objects
        GenerateNewMap();

        musicSeted = false;

        //Reset Day Time
        currentDayTime = DayTime.Day;

 

        //Fade In
        fadeToBlack.SetBool("FadeToBlack", false);
    }

    /// <summary>
    /// Generate new Areas aftter a day ends
    /// </summary>
    void GenerateNewMap()
    {
        //get all gatherable objects and add it to an array
        GameObject[] areaObjects = GameObject.FindGameObjectsWithTag("Gatherable");

        //get all object generators and add it to an array
        ObjectGeneration[] objectGenerators = GameObject.FindObjectsOfType<ObjectGeneration>();

        //destroy all gatherable objects
        for (int i = 0; i < areaObjects.Length; i++)
        {
            Destroy(areaObjects[i]);
        }

        //call all Object generators and call generation script
        for (int i = 0; i < objectGenerators.Length; i++)
        {
            objectGenerators[i].Generation();
        }
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
    /// called after interacting with gameobjects or areas
    /// </summary>
    /// <param name="timeCost"></param>
    public void SpeedTime(float timeCost)
    {
        time = time.Subtract(new TimeSpan(0, 0, 0, 0, (int)(Time.deltaTime * timeCost * 1000)));

        //faster sun rotation
        sun.transform.Rotate(0, 0, -100);
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
    /// List of all day time variations
    /// </summary>
    public enum DayTime
    {
        Day,
        Panic,
        Night
    }

    //Henrik Hafner
    //Save the Datas from the Timer for the Time
    public void SaveTime()
	{
		SaveSystem.SaveTime(this);
	}

	//Henrik Hafner
	// Load the SaveFiles to the Timer and changed the Time
	public void LoadTime()
	{
		TimeData data = SaveSystem.LoadTime();

        //Michael Schmidt
        dayCounter = data.day;
        time = new TimeSpan(0, data.minutes, data.seconds);
	}
}
