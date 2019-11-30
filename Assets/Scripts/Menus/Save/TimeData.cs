using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
//Michael Schmidt
[System.Serializable]
public class TimeData
{
    public int day;
	public int seconds;
    public int minutes;

    //Create SaveFiles form the Time Datas
    public TimeData(Timer timer)
	{
        day = timer.dayCounter;
		seconds = timer.time.Seconds;
        minutes = timer.time.Minutes;
	}
}