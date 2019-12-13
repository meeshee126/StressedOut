using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Schmidt
public class SkipPanicMode : MonoBehaviour
{
    [SerializeField]
    Text uiSkipPanic;

    Timer timer;

    private void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    /// <summary>
    /// Can only skip panic mode when player is in base 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && timer.currentDayTime == Timer.DayTime.Panic)
        {
            //show text
            uiSkipPanic.enabled = true;

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                //set timer to 0
                timer.time = new TimeSpan(0);
            }
        }

        else
        {
            uiSkipPanic.enabled = false;
        }
    }

    /// <summary>
    /// disable text when leaving base
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && timer.currentDayTime == Timer.DayTime.Panic)
        {
            uiSkipPanic.enabled = false;
        }
    } 
}
