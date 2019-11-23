using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeData
{
	public float time;

	//Create SaveFiles form the Time Datas
	public TimeData(Sun sun)
	{
		time = sun.seconds;
	}
}