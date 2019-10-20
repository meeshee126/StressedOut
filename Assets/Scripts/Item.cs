using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Stats))]

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Resource,
        Weapon,
        Armor,
        Miscelaneous,
    }


    [Header("Identification Info")]
    public string ItemName;
    public int ID;

    [Header("Other Info")]
    public Stats stats;
    public BoxCollider2D itemCollider;
    public Rigidbody2D itemRB;

    private void Start()
    {
        stats = GetComponent<Stats>();
        itemCollider = GetComponent<BoxCollider2D>();
        itemRB = GetComponent<Rigidbody2D>();
        ItemName = stats.EntityName;
        stats.health = 50;
    }
}
