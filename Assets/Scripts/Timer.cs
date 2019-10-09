using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float totalTimeInSeconds;

    [SerializeField]
    Text uiSeconds, uiMinutes;

    void Start()
    {
        uiSeconds = GetComponent<Text>();
        uiMinutes = GetComponent<Text>();
    }

    void Update()
    {
        TimerCountdown();
    }

    void TimerCountdown()
    {
        totalTimeInSeconds -= Time.deltaTime;
        float seconds = Mathf.RoundToInt(totalTimeInSeconds % 60);
        float minutes = Mathf.Floor(totalTimeInSeconds / 60);

        uiSeconds.text = seconds.ToString();
        uiMinutes.text = minutes.ToString();
    }
}
