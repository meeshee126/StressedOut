using System;
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
        int i;

        switch (itenname)
        {
            case "Wood":
                if (Int32.TryParse(uiWood.text, out i))
                {
                    i += amount;
                    uiWood.text = i.ToString();
                }
                    break;

            case "Stone_Chunk":
                if (Int32.TryParse(uiStone.text, out i))
                {
                    i += amount;
                    uiStone.text = i.ToString();
                }
                    break;

            case "Iron_Chunk":
                if (Int32.TryParse(uiIron.text, out i))
                {
                    i += amount;
                    uiIron.text = i.ToString();
                }
                    break;

            case "Gold_Chunk":
                if (Int32.TryParse(uiGold.text, out i))
                {
                    i += amount;
                    uiGold.text = i.ToString();
                }
                    break;
            case "Diamond_Chunk":
                if (Int32.TryParse(uiDiamond.text, out i)) i += amount;
                uiDiamond.text = i.ToString(); break;
        }
    }
}
