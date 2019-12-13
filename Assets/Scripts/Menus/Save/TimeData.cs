using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
//Michael Schmidt
[System.Serializable]
public class TimeData
{
    //Michael Schmidt
    public int day;
	public int seconds;
    public int minutes;

    //Michael Schmidt
    //Create SaveFiles form the Time Datas
    public TimeData(Timer timer)
	{
        //Michael Schmidt
        day = timer.dayCounter;
		seconds = timer.time.Seconds;
        minutes = timer.time.Minutes;
	}
}