using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Dimitrios Kitsikidis
/// <summary>
/// Just a list of all the abilitylists categorized, ..
/// .. so we don't have to codewise look into the project files and search for all prefab abilities. 
/// </summary>
public class AbilityLists : MonoBehaviour
{
    [Header("Player")]
    [Space(15)]
    public GameObject[] playerAbilities;

    [Space(15)]
    [Header("Friendly Entities")]
    [Space(15)]

    [Space(15)]
    [Header("Neutral Entities")]
    [Space(15)]
    public GameObject[] slimeAbilities;

    [Space(15)]
    [Header("Enemy Entities")]
    [Space(15)]
    public GameObject[] banditAbilities;
    public GameObject[] bearAbilities;
    public GameObject[] boarAbilities;
}
