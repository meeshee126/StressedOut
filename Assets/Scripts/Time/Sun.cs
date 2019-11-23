using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Schmidt

public class Sun : MonoBehaviour
{
    [Header("Sun Color")]
    [SerializeField]
    Color red;
    [SerializeField]
    Color yellow;

    [HideInInspector]
    public Slider slider;

    [HideInInspector]
    public bool sliderSeted = false;

    Timer timer;
    Image image;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        image = GameObject.Find("Sun").GetComponent<Image>();

        slider = GetComponent<Slider>();
    }

    void Update()
    {
        SliderManager();
        SetColor();     
    }

    /// <summary>
    /// Set slider with correct values from timer
    /// </summary>
    void SliderManager()
    {
        if (!sliderSeted)
        {
            //set slider max value with with total ingame time
            slider.maxValue = timer.TotalTimeInSeconds();
            sliderSeted = true;
        }

        //transforms the sun
        slider.value = timer.TotalTimeInSeconds();
    }

    /// <summary>
    /// Set Sun color
    /// </summary>
    void SetColor()
    {
        //changes sun color while sunset
        image.color = Color.Lerp(red, yellow, slider.normalizedValue >
                     (timer.panicTimer / 100) ? slider.normalizedValue : (timer.panicTimer / 100));
    }
}