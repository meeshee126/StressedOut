using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Schmidt

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

        //Set all resource display to 0
        SetUI(uiWood);
        SetUI(uiStone);
        SetUI(uiIron);
        SetUI(uiGold);
        SetUI(uiDiamond);
	}

    /// <summary>
    /// function for setting UI resource count
    /// </summary>
    /// <param name="resource"></param>
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

    /// <summary>
    /// function for displays all current resource amount
    /// </summary>
    void ShowUI()
    {
        uiWood.text = wood.ToString();
        uiStone.text = stone.ToString();
        uiIron.text = iron.ToString();
        uiGold.text = gold.ToString();
        uiDiamond.text = diamond.ToString();
    }

    /// <summary>
    /// Adding collected resources
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="amount"></param>
    public void AddResource(string itemName, int amount)
    {
        //Check which kind of resource is collected and add it to the assigned variable
        //resources can not be less then 0
        switch (itemName)
        {
            case "Wood":
                wood += amount;
                if (wood < 0) { wood = 0; }
                break;

            case "Stone_Chunk":
                stone += amount;
                if (stone < 0) { stone = 0; }
                break;

            case "Iron_Chunk":
                iron += amount;
                if (iron < 0) { iron = 0; }
                break;

            case "Gold_Chunk":
                gold += amount;
                if (gold < 0) { gold = 0; }
                break;

            case "Diamond_Chunk":
                diamond += amount;
                if (diamond < 0) { diamond = 0; }
                break;
        }
    }

	//Henrik Hafner
	/// <summary>
	/// Checks which building is to be built and gives the resource cost in the UI
	/// </summary>
	/// <param name="costsname"></param>
	/// <param name="costs"></param>
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
