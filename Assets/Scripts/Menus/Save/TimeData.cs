using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
//Michael Schmidt
[System.Serializable]
public class TimeData
{
	public int seconds;
    public int minutes;

    //Create SaveFiles form the Time Datas
    public TimeData(Timer timer)
	{
		seconds = timer.seconds;
        minutes = timer.minutes;
	}
}