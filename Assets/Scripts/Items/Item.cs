using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
//[RequireComponent(typeof(Stats))]

public class Item : MonoBehaviour
{
    private void Awake()
    {
        //stats = GetComponent<Stats>();
        itemCollider = GetComponent<BoxCollider2D>();
        itemRB = GetComponent<Rigidbody2D>();
        name = ItemName;
    }


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
    //public Stats stats;
    public BoxCollider2D itemCollider;
    public Rigidbody2D itemRB;
}
