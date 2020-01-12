using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_AbilityList : MonoBehaviour
{
    public AbilityCategories[] abilitylist;
    
}


[System.Serializable]
public class AbilityCategories
{
    public string categoryName;
    public Ability_ListComponent[] AbilitiesSetup;

    [System.Serializable]
    public struct Ability_ListComponent
    {
        public string someNeededStuff;
        public GameObject abilityPrefab;


        public Ability_ListComponent(string someNeededStuff_e, GameObject abilityPrefab_e)
        {
            someNeededStuff = someNeededStuff_e;
            abilityPrefab = abilityPrefab_e;
        }
    }
}
