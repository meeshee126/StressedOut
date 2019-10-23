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
        none,
        resource,
        consumable,
        weapon,
        armor,
        miscelaneous,
    }

    [Header("Identification Info")]
    public ItemType itemType;
    public string itemName;
    public int iD;
    public int amount;
    public int itemHealth;

    public bool isPickedUp = false;
    public bool isDead = false;

    [Space(15)]
    public int ItemTypeAttribute;
    [Space(15)]

    [Header("Other Info")]
    [Space(10)]
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D itemCollider;
    public Rigidbody2D itemRB;
    public PhysicsMaterial2D physicsMaterial;
    public GameObject gameManager;



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
        itemCollider.size = new Vector2(1.5f, 1.5f);
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
