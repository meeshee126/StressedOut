﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Schmidt

public class TimeSlider : MonoBehaviour
{
    Timer timer;
    Slider slider;
    Image image;

    [Header("Sun Color")]
    [SerializeField]
    Color red;
    [SerializeField]
    Color yellow;


    bool sliderSeted = false;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        image = GameObject.Find("Sun").GetComponent<Image>();

        slider = GetComponent<Slider>();
    }

    void Update()
    {
        SetSlider();
        PanicMode();
    }

    void SetSlider()
    {
        if (!sliderSeted)
        {
            slider.maxValue = timer.TotalTimeInSeconds();
            sliderSeted = true;
        }

        slider.value = timer.TotalTimeInSeconds();

        image.color = Color.Lerp(red, yellow, slider.normalizedValue >
                     (timer.panicTimer / 100) ? slider.normalizedValue : (timer.panicTimer / 100));
    }

    void PanicMode()
    {
        if(slider.normalizedValue <= (timer.panicTimer / 100))
        {
            timer.currentDayTime = Timer.DayTime.Panic;
        }
    }
}

