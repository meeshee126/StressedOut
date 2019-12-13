using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

[System.Serializable]
public class SunData
{
    public float maxValue;
    public bool sliderCondition;
   
    //Create Save files from Sun
    public SunData(Sun sun)
    {
        maxValue = sun.maxValue;
        sliderCondition = sun.sliderSeted;
    }
}
