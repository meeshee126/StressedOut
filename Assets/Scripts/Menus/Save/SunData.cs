using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SunData
{
    public float maxValue;
    public bool sliderCondition;
   

    //Create SaveFiles form the Time Datas
    public SunData(Sun sun)
    {
        maxValue = sun.maxValue;
        sliderCondition = sun.sliderSeted;
    }
}
