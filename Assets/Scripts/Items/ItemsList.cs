using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Dimtirios Kitsikidis
/// <summary>
/// Represents the Items Collection
/// SOON: set's and organizes the Item ID's as well as the collection based on item ID
/// </summary>
public class ItemsList : MonoBehaviour
{
    public GameObject[] itemCollection;

    //  Needs fix: automatic itemIDCollection creation

    //public int[] itemIDCollection;


    //private void Start()
    //{
    //    itemIDCollection = new int[itemIDCollection.Length];
    //    SetItemIDs();
    //}


    //public void SetItemIDs()
    //{
    //    for (int i = 0; i < itemCollection.Length; i++)
    //    {
    //        itemIDCollection[i] = itemCollection[i].GetComponent<Item>().iD;
    //    }
    //}
}

// The first manual way used for Item  and ID collection.
#region Manual Database
////[Space(10)]
////[Header("Resources")]
////#region Resources
////#region General
////public GameObject Wood;
////public GameObject Stone_Chunk;
////public GameObject Iron_Chunk;
////public GameObject Gold_Chunk;
////public GameObject Diamond_Chunk;
////#endregion

////#region Special
////public GameObject Golden_Coin;
////#endregion
////#endregion // Resources End

////[Space(10)]
////[Header("Equipables \n\r")]
////[Space(10)]
////#region Equipables
////#region Weaponry
////public GameObject Wood_Sword;
////public GameObject Stone_Sword;
////public GameObject Iron_Sword;
////public GameObject Gold_Sword;
////public GameObject Diamond_Sword;
////[Space(10)]
////public GameObject Wood_Bow;
////public GameObject Stone_Bow;
////public GameObject Iron_Bow;
////public GameObject Gold_Bow;
////public GameObject Diamond_Bow;
////[Space(10)]
////#endregion // Weaponry End

////#region Armory
////#endregion // Armory End
////#endregion // Equipables End

////[Space(10)]
////[Header("Miscellaneous")]
////[Space(10)]
////#region Miscellaneous
////#region Entity Drops
////#endregion

////#region Combat Material
////public GameObject Stone_Arrow;
////public GameObject Iron_Arrow;
////public GameObject Gold_Arrow;
////public GameObject Diamond_Arrow;
////#endregion
////#endregion

//public void SetItemIDs()
//{
//    //  --  Resources   --
//    //      --  General --
//    Wood.GetComponent<Item>().iD = 1;
//    Stone_Chunk.GetComponent<Item>().iD = 2;
//    Iron_Chunk.GetComponent<Item>().iD = 3;
//    Gold_Chunk.GetComponent<Item>().iD = 4;
//    Diamond_Chunk.GetComponent<Item>().iD = 5;

//    //      --  Special --
//    Golden_Coin.GetComponent<Item>().iD = 100;

//    //  --  Equipables   --
//    //      --  Weaponry    --

//    Wood_Sword.GetComponent<Item>().iD = 501;
//    Stone_Sword.GetComponent<Item>().iD = 502;
//    Iron_Sword.GetComponent<Item>().iD = 503;
//    Gold_Sword.GetComponent<Item>().iD = 504;
//    Diamond_Sword.GetComponent<Item>().iD = 505;

//    Wood_Bow.GetComponent<Item>().iD = 531;
//    Stone_Bow.GetComponent<Item>().iD = 532;
//    Iron_Bow.GetComponent<Item>().iD = 533;
//    Gold_Bow.GetComponent<Item>().iD = 534;
//    Diamond_Bow.GetComponent<Item>().iD = 535;


//    //      --  Armory  --

//    //  -- Miscellaneous    --
//        //  -- Combat Material  --
//    Stone_Arrow.GetComponent<Item>().iD = 801;
//    Iron_Arrow.GetComponent<Item>().iD = 802;
//    Gold_Arrow.GetComponent<Item>().iD = 803;
//    Diamond_Arrow.GetComponent<Item>().iD = 804;
//}
#endregion
