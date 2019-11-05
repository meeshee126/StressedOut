using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    Text uiWood, uiStone, uiIron, uiGold, uiDiamond;
    Text uiWoodCosts, uiStoneCosts, uiIronCosts, uiGoldCosts, uiDiamondCosts;

    [SerializeField]
    public int wood, stone, iron, gold, diamond;

    private void Start()
    {
        uiWood = GameObject.Find("WoodCount").GetComponent<Text>();
        uiStone = GameObject.Find("StoneCount").GetComponent<Text>();
        uiIron = GameObject.Find("IronCount").GetComponent<Text>();
        uiGold = GameObject.Find("GoldCount").GetComponent<Text>();
        uiDiamond = GameObject.Find("DiamondCount").GetComponent<Text>();

        uiWoodCosts = GameObject.Find("WoodCosts").GetComponent<Text>();

        SetUI(uiWood);
        SetUI(uiStone);
        SetUI(uiIron);
        SetUI(uiGold);
        SetUI(uiDiamond);
	}

    void SetUI(Text resource)
    {
        if(resource.text == null)
        {
            resource.text = 0.ToString();
        }
    }

    void Update()
    {
        ShowUI();
    }

    void ShowUI()
    {
        uiWood.text = wood.ToString();
        uiStone.text = stone.ToString();
        uiIron.text = iron.ToString();
        uiGold.text = gold.ToString();
        uiDiamond.text = diamond.ToString();
    }


    public void AddResource(string itenname, int amount)
    {
        switch (itenname)
        {
            case "Wood":
                wood += amount;
                if (wood < 0) { wood = 0; }
                break;

            case "Stone_Chunk":
                stone += amount;
                break;

            case "Iron_Chunk":
                iron += amount;
                break;

            case "Gold_Chunk":
                gold += amount;
                break;

            case "Diamond_Chunk":
                diamond += amount;
                break;
        }
    }

    public void ResourceCosts(string costsname, string costs)
    {
        switch (costsname)
        {
            case "Wood_Chunk":
                uiWoodCosts.text = costs; 
                break;

            case "Stone_Chunk":
                break;

            case "Iron_Chunk":
                break;

            case "Gold_Chunk":
                break;

            case "Diamond_Chunk":
                break;
        }
    }
}
