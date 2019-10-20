using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    private void Start()
    {
        SetItemIDs();
        ItemCollection = new GameObject[]
        {
            Wood, Stone_Chunk, Iron_Chunk, Gold_Chunk, Diamond_Chunk,
            Wood_Sword, Stone_Sword, Iron_Sword, Gold_Sword, Diamond_Sword,
            Wood_Bow, Stone_Bow, Iron_Bow, Gold_Bow, Diamond_Bow,
            Stone_Arrow, Iron_Arrow, Gold_Arrow, Diamond_Arrow
        };

    }


    public GameObject[] ItemCollection;

    [Space(10)]
    [Header("Resources")]
    #region Resources
    public GameObject Wood;
    public GameObject Stone_Chunk;
    public GameObject Iron_Chunk;
    public GameObject Gold_Chunk;
    public GameObject Diamond_Chunk;
    #endregion // Resources End

    [Space(10)]
    [Header("Weaponry \n\r")]
    [Space(10)]
    #region Equipables
    #region Weaponry
    public GameObject Wood_Sword;
    public GameObject Stone_Sword;
    public GameObject Iron_Sword;
    public GameObject Gold_Sword;
    public GameObject Diamond_Sword;
    [Space(10)]
    public GameObject Wood_Bow;
    public GameObject Stone_Bow;
    public GameObject Iron_Bow;
    public GameObject Gold_Bow;
    public GameObject Diamond_Bow;
    [Space(10)]
    public GameObject Stone_Arrow;
    public GameObject Iron_Arrow;
    public GameObject Gold_Arrow;
    public GameObject Diamond_Arrow;
    #endregion // Weaponry End

    #region Armory
    #endregion // Armory End
    #endregion // Equipables End

    public void SetItemIDs()
    {
        //  --  Resources   --

        Wood.GetComponent<Item>().ID = 1;
        Stone_Chunk.GetComponent<Item>().ID = 2;
        Iron_Chunk.GetComponent<Item>().ID = 3;
        Gold_Chunk.GetComponent<Item>().ID = 4;
        Diamond_Chunk.GetComponent<Item>().ID = 5;

        //  --  Equipables   --
        //      --  Weaponry    --

        Wood_Sword.GetComponent<Item>().ID = 501;
        Stone_Sword.GetComponent<Item>().ID = 502;
        Iron_Sword.GetComponent<Item>().ID = 503;
        Gold_Sword.GetComponent<Item>().ID = 504;
        Diamond_Sword.GetComponent<Item>().ID = 505;

        Wood_Bow.GetComponent<Item>().ID = 531;
        Stone_Bow.GetComponent<Item>().ID = 532;
        Iron_Bow.GetComponent<Item>().ID = 533;
        Gold_Bow.GetComponent<Item>().ID = 534;
        Diamond_Bow.GetComponent<Item>().ID = 535;

        Stone_Arrow.GetComponent<Item>().ID = 561;
        Iron_Arrow.GetComponent<Item>().ID = 562;
        Gold_Arrow.GetComponent<Item>().ID = 563;
        Diamond_Arrow.GetComponent<Item>().ID = 564;

        //      --  Armory  --

    }
}