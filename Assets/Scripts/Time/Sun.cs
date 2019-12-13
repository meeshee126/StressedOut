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

    [HideInInspector]
    public float maxValue;

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
            maxValue = timer.TotalTimeInSeconds();
            slider.maxValue = maxValue;
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

    /// <summary>
    /// Save all Sun datas
    /// </summary>
    public void SaveSun()
    {
        SaveSystem.SaveSun(this);
    }

    /// <summary>
    /// load Sun datas
    /// </summary>
    public void LoadSun()
    {
        SunData data = SaveSystem.LoadSun();

        slider.maxValue = data.maxValue;
        sliderSeted = data.sliderCondition;
    }
}