using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Dimitrios Kitsikidis
/// <summary>
/// Holds all the stats of a weapon type item
/// (aka the stats which each weapon type item uses for identifications of various elements, ..
/// .. such as type of weapon, damage, critical chance, etc.)
/// </summary>
public class WeaponStats : MonoBehaviour
{
    [Header("Attributes")]
    [Space(10)]
    public WeaponType type = WeaponType.undefined;
    [Range(-5,100)]
    [Tooltip("Points")]
    public int damage = 0;
    [Range(0.1f, 10.0f)]
    [Tooltip("Divides Cast-Time by itself.")]
    public float attackSpeed = 1f;
    [Range(0.1f, 100.0f)]
    [Tooltip("Percentage")]
    public float criticalChance = 5f;
    [Range(1f, 10f)]
    [Tooltip ("even if the damage is 1.8 with critical.. \n\r the damage infliction will be 1")]
    public float criticalDamageMultiplier = 2f;
    [Space(10)]
    public WeaponEnchantment enchantment = WeaponEnchantment.none;

    public enum WeaponType
    {
        undefined = -10,
        melee = 1,
        range = 2
    }

    public enum WeaponEnchantment
    {
        none = 0,
    }
}
