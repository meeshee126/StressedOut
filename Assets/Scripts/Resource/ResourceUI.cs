using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    Text uiWood, uiStone, uiIron, uiGold, uiDiamond;


    void Update()
    {
       
 
    }

    public void AddMaterial(string itenname, int amount)
    {
        switch(itenname)
        {
            case "Wood":
                uiWood.text += amount.ToString();
                Debug.Log("test");
                break;
        }
    }
}
