using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]


// Dimitrios Kitsikidis
/// <summary>
/// Holds all values that define an item
/// Adds all the item required components to the gameobject
/// Sets all item default settings to all the gameobject components
/// Handles the resource pick up task (for now (will be moved to player in future gen))
/// </summary>
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
    public GameObject targetPlayer;


    /// <summary>
    /// Finds and sets all the components used in this script
    /// Calls all default settings to the item to be set
    /// </summary>
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        physicsMaterial = gameManager.GetComponentInChildren<PhysicsMaterialList>().itemsMaterial;
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<BoxCollider2D>();
        itemRB = GetComponent<Rigidbody2D>();
        targetPlayer = GameObject.Find("Player");
        SetItemSettings();
    }


    /// <summary>
    /// Checks wether the item is picked up or destroyed..
    /// .. and regarding on the situation handles the item behaviour
    /// </summary>
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


    /// <summary>
    /// Checks for the distance between player..
    /// ..and triggers the movement toward player uppon a certain distance.
    /// </summary>
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, targetPlayer.transform.position) < 1.5f &&
            itemType == ItemType.resource)
        {
            //Changed "is picked up" to Player class
            PlayerFound();
            if (Vector2.Distance(transform.position, targetPlayer.transform.position) < 0.1f &&
                itemType == ItemType.resource); 
        }
    }


    /// <summary>
    /// Sets all the default item settings uppon call.
    /// </summary>
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

    /// <summary>
    /// Transforms position towards player.
    /// </summary>
    void PlayerFound()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, 15f * Time.fixedDeltaTime);
    }
}
