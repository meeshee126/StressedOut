using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

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
    public string itemName;
    public int iD;
    public int itemHealth;

    public bool isPickedUp = false;
    private bool isDead = false;

    [Header("Other Info")]
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D itemCollider;
    public Rigidbody2D itemRB;
    public PhysicsMaterial2D physicsMaterial;
    GameObject gameManager;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        physicsMaterial = gameManager.GetComponentInChildren<PhysicsMaterialList>().itemsMaterial;
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<BoxCollider2D>();
        itemRB = GetComponent<Rigidbody2D>();
        SetItemSettings();
    }


    private void Update()
    {
        if (isDead) Destroy(gameObject);
        if (isPickedUp)
        {
            // Instantiate picked Effect
            isDead = true;
        }
        if (itemHealth <= 0)
        {
            // Instantiate destroy Effect
            isDead = true;
        }
    }


    void SetItemSettings()
    {
        if (itemName == "") itemName = name;
        name = itemName;
        itemHealth = 50;
        itemCollider.size = new Vector2(0.5f, 0.5f);
        itemCollider.offset = new Vector2(0f, 0f);
        itemCollider.isTrigger = true;
        itemRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        itemRB.freezeRotation = true;
        itemRB.gravityScale = 0f;
        itemRB.angularDrag = 0f;
        itemRB.drag = 10f;
        spriteRenderer.sortingLayerName = "DroppedItem";
        spriteRenderer.sortingOrder = 5;
    }
}
