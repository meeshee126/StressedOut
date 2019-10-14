using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Schmidt

public class TimeSlider : MonoBehaviour
{
    Timer timer;
    Slider slider;

    bool sliderSeted = false;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if(!sliderSeted)
        {
            slider.maxValue = timer.TotalTimeInSeconds();
            sliderSeted = true;
        }

        slider.value = timer.TotalTimeInSeconds();
    }
}
