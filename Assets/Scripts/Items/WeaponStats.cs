﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Attributes")]
    [Space(10)]
    public WeaponType type = WeaponType.none;
    [Range(-5,100)]
    public int damage = 1;
    [Range(0.1f, 10.0f)]
    public float attackSpeed = 1f;
    [Range(0.1f, 100.0f)]
    public float criticalChance = 5f;
    [Range(1f, 10f)]
    public float criticalDamageMultiplier = 2f;
    [Space(10)]
    public WeaponEnchantment enchantment = WeaponEnchantment.none;

    public enum WeaponType
    {
        none = 0,
        melee = 1,
        range = 2
    }

    public enum WeaponEnchantment
    {
        none = 0,
    }
}