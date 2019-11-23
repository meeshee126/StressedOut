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

	public float maxTime = 305;
	public float seconds;

	Timer timer;
    Image image;

    bool sliderSeted = false;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        image = GameObject.Find("Sun").GetComponent<Image>();

        slider = GetComponent<Slider>();

		seconds = maxTime;
    }

    void Update()
    {
        SliderManager();
        SetColor();

		if (seconds >= 0)
		{
			seconds -= Time.deltaTime;
			slider.value = seconds;
		}
	}

    /// <summary>
    /// Set slider with correct values from timer
    /// </summary>
    void SliderManager()
    {
        if (!sliderSeted)
        {
            //set slider max value with with total ingame time
            slider.maxValue = maxTime;
            sliderSeted = true;
        }
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

	//Henrik Hafner
	//Save the Datas from the Sun for the Time
	public void SaveTime()
	{
		SaveSystem.SaveTime(this);
	}

	//Henrik Hafner
	// Load the SaveFiles to the Sun and changed the Time
	public void LoadTime()
	{
		TimeData data = SaveSystem.LoadTime();

		seconds = data.time;
	}
}