using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
