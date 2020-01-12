using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Stats))]


/// <summary>
/// Defines and handles the player himself.
/// Still quite incomplete
/// Sorry for the inconvenience of +450 Lines. code will be split, . . .
/// . . . each action section will turn into a class for better readability.
/// Example: ItemPickUpHandler.cs, PlayerMovement.cs, PlayerCombat.cs, etc.
/// </summary>
public class Player : MonoBehaviour
{
    [HideInInspector]
    public CapsuleCollider2D characterCollider;
    [HideInInspector]
    public Animator characterAnimator;
    [HideInInspector]
    public Rigidbody2D characterRB;
    [HideInInspector]
    public Stats stats;

    [HideInInspector]
    public ResourceManager resourceManager;
    [HideInInspector]
    public WeaponStats weaponStats;

    [HideInInspector]
    public TimeBehaviour timeBehaviour;

    private AudioSource audioSource;
    protected AbilityLists abilityLists;
    private GameObject gameManager;
    private ItemsList itemList;

    #region Timers
    private float invincibilityTimer;
    #endregion

    [Header("Equipment")]
    [Space(15)]
    public GameObject playerHand;
    public GameObject Helmet, Suite, Boots;
    public GameObject[] QuickbarContent = new GameObject[5];

    [Space(15)]
    [Header("Abilities Related")]
    [Space(15)]
    protected bool isCasting;
    public GameObject selectedAbility;
    public float[] cooldownsListCopy, castTimeListCopy;

    private float defaultSpeedMultiplier;


    // Unity
    /// <summary>
    /// Finds and sets all the components used in this script
    /// </summary>
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        resourceManager = gameManager.GetComponent<ResourceManager>();
        abilityLists = gameManager.GetComponentInChildren<AbilityLists>();
        itemList = gameManager.GetComponentInChildren<ItemsList>();
        timeBehaviour = gameManager.GetComponent<TimeBehaviour>();
        audioSource = GetComponent<AudioSource>();

        characterCollider = GetComponent<CapsuleCollider2D>();
        characterAnimator = GetComponent<Animator>();
        characterRB = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
    }


    private void Start()
    {
        cooldownsListCopy = new float[abilityLists.playerAbilities.Length];
        castTimeListCopy = new float[abilityLists.playerAbilities.Length];

        for (int i = 0; i < cooldownsListCopy.Length; i++)
        {
            cooldownsListCopy[i] = abilityLists.playerAbilities[i].GetComponent<Ability>().Cooldown;
            castTimeListCopy[i] = abilityLists.playerAbilities[i].GetComponent<Ability>().CastingTime;
        }
    }


    // Unity
    /// <summary>
    /// Contributes in the current combo status handling
    /// Allows every other action to run thru based on player's input
    /// </summary>
    void Update()
    {
        if (stats.health <= 0)
        {
            if (stats.DestroySFX != null) Instantiate(stats.DestroySFX, transform.position, Quaternion.identity);
            if (stats.DeadFX != null) Instantiate(stats.DeadFX, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        ApplyItemHandlerInput();
        ApplyMovementInput();

        CountDownTimers();

        CooldownManager();

        // MAKE IT READ THE 
    }

    private void CountDownTimers()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GetAbility();

        // is ComboTime Over ?
        if (stats.comboTimer <= 0f) ComboReset();

        // Run thru Ability List.
        for (int i = 0; i < abilityLists.playerAbilities.Length; i++)
        {
            // Ability Found ?
            if (selectedAbility.GetComponent<Ability>().CastName ==
                abilityLists.playerAbilities[i].GetComponent<Ability>().CastName)
            {
                // is on CastingTime ?
                if (castTimeListCopy[i] > 0f)
                {
                    isCasting = true;
                    characterRB.velocity = new Vector2(0f, 0f);
                    // ADD HERE:    Player Ability Casting animation
                }
                // is CastingTime Over ?
                if (castTimeListCopy[i] <= 0f)
                    isCasting = false;
            }
        }

        // is NOT Casting ?
        if (isCasting == false)
        {
            // Run thru Ability List.
            for (int i = 0; i < abilityLists.playerAbilities.Length; i++)
            {
                // Ability Found ?
                if (selectedAbility.GetComponent<Ability>().CastName ==
                    abilityLists.playerAbilities[i].GetComponent<Ability>().CastName)
                {
                    // is on Cooldown ?
                    //if (cooldownsListCopy[i] > 0f)
                        //Debug.Log(selectedAbility.GetComponent<Ability>().CastName + " . . . is Still on cooldown . . .");
                    // is Cooldown Over ?    
                    if (cooldownsListCopy[i] <= 0f)
                    {
                        // Get ability
                        // Instantiate
                        ApplyAttackInput(); // Set casting time
                    }
                }
            }
        }
    }

    // Dimitrios Kitsikidis
    /// <summary>
    /// Translates  [User Input]
    /// into        [Player Movement].
    /// </summary>
    private void ApplyMovementInput()
    {
        if (isCasting == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            MovementAnimationUpdate(moveHorizontal, moveVertical);

            defaultSpeedMultiplier = 100f;
            if (Input.GetButton("Horizontal") && Input.GetButton("Vertical")) defaultSpeedMultiplier = 75f;
            characterRB.velocity = new Vector2(moveHorizontal, moveVertical) * defaultSpeedMultiplier * stats.movementSpeed * Time.deltaTime;
            // transform.Translate(new Vector2(moveHorizontal, moveVertical) * stats.movementSpeed * Time.deltaTime);

            #region old
            //transform.Translate(new Vector2(moveHorizontal, moveVertical));
            #endregion
        }
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Updates the [Player's Animation]
    /// based on    [Player's Movement].
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    private void MovementAnimationUpdate(float moveX, float moveY)
    {
        // Changes Animation Based on direction facing.
        characterAnimator.SetFloat("FaceX", moveX);
        characterAnimator.SetFloat("FaceY", moveY);

        if (moveX != 0 || moveY != 0)
        {
            characterAnimator.SetBool("isWalking", true);
            if (moveX > 0) characterAnimator.SetFloat("LastMoveX", 1f);
            else if (moveX < 0) characterAnimator.SetFloat("LastMoveX", -1f);
            else characterAnimator.SetFloat("LastMoveX", 0f);

            if (moveY > 0) characterAnimator.SetFloat("LastMoveY", 1f);
            else if (moveY < 0) characterAnimator.SetFloat("LastMoveY", -1f);
            else characterAnimator.SetFloat("LastMoveY", 0f);
        }
        else
        {
            characterAnimator.SetBool("isWalking", false);
        }
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Uppon Input gets the selectedAttack..
    /// .. filters it to check wether it's still on cooldown..
    /// .. and finally instantiates it
    /// </summary>
    private void ApplyAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {


            // Run through list.
            for (int i = 0; i < abilityLists.playerAbilities.Length; i++)
            {
                // Ability Found ?
                if (selectedAbility.GetComponent<Ability>().CastName ==
                    abilityLists.playerAbilities[i].GetComponent<Ability>().CastName)
                {
                    Debug.Log("Casted! " + selectedAbility.GetComponent<Ability>().CastName);
                    // Cast it.
                    Instantiate(selectedAbility, transform.position, Quaternion.identity);
                    // Reset Cooldown.
                    cooldownsListCopy[i] = abilityLists.playerAbilities[i].GetComponent<Ability>().Cooldown;
                    // Reset Castime.
                    castTimeListCopy[i] = abilityLists.playerAbilities[i].GetComponent<Ability>().CastingTime;
                }
            }
        }
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Filters thru what item the player is holding
    /// <para>what player's current combo status is</para>
    /// <para>and casts the correct attack based on that</para>
    /// </summary>
    private void GetAbility()
    {
        Item item = playerHand.GetComponent<Item>();
        // is Weapon ?
        if (item.itemType == Item.ItemType.weapon)
        {
            WeaponStats weaponStats = playerHand.GetComponent<WeaponStats>(); // Use to adjust Ability's attributes
            // is Sword ?
            if (item.itemName.Contains("Sword"))
            {
                // Current Combo ?
                switch (stats.currentCombo)
                {
                    case 0: AbilitySelector("Entry-Attack"); break;
                    case 1: AbilitySelector("First-MainAttack"); break;
                    case 2: AbilitySelector("Second-MainAttack"); break;
                }
            }
            // is Bow?
            if (item.itemName.Contains("Bow"))
            {
                // Insert Bow Mechanics
            }
        }
        // is non-Weapon ?
        if (item.itemType != Item.ItemType.weapon)
        {
            AbilitySelector("NonWeaponAttack");
        }

        // Read from Combo manager
        // Filter thru the abilities available for the currently holding item
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Filters thru player's abilities
    /// <para>Also handles player's combo status</para>
    /// </summary>
    /// <param name="selectedAbilityName">Ability name to search for</param>
    private void AbilitySelector(string selectedAbilityName)
    {
        // Run through List.
        for (int i = 0; i < abilityLists.playerAbilities.Length; i++)
        {
            // Ability Found ?
            if (abilityLists.playerAbilities[i].GetComponent<Ability>().CastName == selectedAbilityName)
            {
                ComboCountUp();
                ComboTimeSet(1.5f);
                // Max-Combo Reached ?
                if (stats.currentCombo > 2)
                    ComboReset();
                // Set NEW Ability Values.
                selectedAbility.GetComponent<Ability>().
                    SetValues(abilityLists.playerAbilities[i].GetComponent<Ability>());
            }
        }
    }




    // Dimitrios Kitsikidis
    /// <summary>
    /// Simply allows all abilities that are on cooldown to run through the process of cooling down
    /// </summary>
    private void CooldownManager()
    {
        // Run through List.
        for (int i = 0; i < cooldownsListCopy.Length; i++)
        {
            // is on Cooldown ?
            if (cooldownsListCopy[i] > 0f)
            {
                // Count Down Cooldown.
                cooldownsListCopy[i] -= Time.deltaTime;
            }
        }
        for (int i = 0; i < castTimeListCopy.Length; i++)
        {
            // is on CastTime ?
            if (castTimeListCopy[i] > 0f)
            {
                // Count Down CastTime.
                castTimeListCopy[i] -= Time.deltaTime;
            }
        }

        if (stats.comboTimer > 0f) stats.comboTimer -= Time.deltaTime;
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Filters through all the collisions within inputed Collider2D array..
    /// .. until the closest collision to this.gameobject has been found
    /// </summary>
    /// <param name="list"></param>
    /// <returns> returns the closest collision found to this.gameobject </returns>
    private Collider2D GetClosest(Collider2D[] list)
    {
        Collider2D closestTarget = null;
        float closestDistance = 2f;
        foreach (Collider2D target in list)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (target.name != this.name)
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }
        return closestTarget;
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Filters
    /// Selects
    /// Sorts 
    /// ... the process of item pick-up
    /// <para>WARNING: Some components are incomplete</para>
    /// </summary>
    private void ApplyItemHandlerInput()
    {
        if (isCasting == false)
        {
            Collider2D[] collisionsInCastArea = Physics2D.OverlapCircleAll(transform.position, 1f);

            for (int i = 0; i < collisionsInCastArea.Length; i++)
            {
                if (collisionsInCastArea[i].gameObject.GetComponent<Item>() &&
                    collisionsInCastArea[i] == GetClosest(collisionsInCastArea))
                {
                    // Is It an Item and Did Player PRESS <"F">
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        // Which Item Type
                        switch (collisionsInCastArea[i].GetComponent<Item>().itemType)
                        {
                            case Item.ItemType.none:
                                Debug.Log("Called none");
                                IfNoneItem(collisionsInCastArea[i]);
                                break;
                            case Item.ItemType.consumable:
                                Debug.Log("Called consumable");
                                IfConsumableItem(collisionsInCastArea[i]);
                                break;
                            case Item.ItemType.weapon:
                                Debug.Log("Called Weapon");
                                IfWeaponItem(collisionsInCastArea[i]);
                                break;
                            case Item.ItemType.armor:
                                Debug.Log("Called armor");
                                IfArmorItem(collisionsInCastArea[i]);
                                break;
                            case Item.ItemType.miscelaneous:
                                Debug.Log("Called miscelaneous");
                                IfMiscellaneousItem(collisionsInCastArea[i]);
                                break;
                        }
                    }
                }
            }
        }
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Used for the resource pick up.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>()) IfResourceItem(collision);
    }


    // Dimitrios Kitsikidis
    /// <summary>
    /// Handles the item pickup process
    /// ..drops the held weapon and picks up the weapon on the ground
    /// <para>WARNING: It currently only works with weapons</para>
    /// </summary>
    /// <param name="pickedItem"></param>
    private void ItemPickUp(GameObject pickedItem)
    {
        // Go thru item Collection
        for (int i = 0; i < itemList.itemCollection.Length; i++)
        {
            if (pickedItem.GetComponent<Item>().iD == itemList.itemCollection[i].GetComponent<Item>().iD)
            {
                Instantiate(playerHand, transform.position, Quaternion.identity);
                playerHand = itemList.itemCollection[i];
                pickedItem.GetComponent<Item>().isPickedUp = true;
            }
        }
    }


    private void FoodSteps()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }


    // Dimitrios Kitsikidis
    #region Item Types
    /// <summary>
    /// Item handler if the Item the player just interacted with is type of      None
    /// <para>WARNING: method incomplete</para>
    /// </summary>
    /// <param name="collision"></param>
    private void IfNoneItem(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.none)
        {

        }
    }


    /// <summary>
    /// Item handler if the Item the player just interacted with is type of      Resource
    /// </summary>
    /// <param name="collision"></param>
    private void IfResourceItem(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.resource)
        {
            //Michael Schmidt
            //adding resource to resourceManager class
            resourceManager.AddResource(collision.GetComponent<Item>().itemName, collision.GetComponent<Item>().amount);

            collision.GetComponent<Item>().isPickedUp = true;

        }
    }


    /// <summary>
    /// Item handler if the Item the player just interacted with is type of      Consumable
    /// <para>WARNING: Method might be incomplete</para>
    /// </summary>
    /// <param name="collision"></param>
    private void IfConsumableItem(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.consumable)
        {
            stats.health += collision.GetComponent<Item>().ItemTypeAttribute;
            collision.GetComponent<Item>().isPickedUp = true;
        }
    }


    /// <summary>
    /// Item handler if the Item the player just interacted with is type of      Weapon
    /// </summary>
    /// <param name="collision"></param>
    private void IfWeaponItem(Collider2D collision)
    {
        ItemPickUp(collision.gameObject);
        stats.currentCombo = 0;
    }


    /// <summary>
    /// Item handler if the Item the player just interacted with is type of      Armor
    /// <para>WARNING: Method incomplete (Still under construction)</para>
    /// </summary>
    /// <param name="collision"></param>
    private void IfArmorItem(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.armor)
        {
            if (collision.GetComponent<Item>().itemName.Contains("Helmet")) Debug.Log("Yes the item contains Helmet");
            else if (collision.GetComponent<Item>().itemName.Contains("Suite")) Debug.Log("No it contains Suite Instead");
            else if (collision.GetComponent<Item>().itemName.Contains("Boots")) Debug.Log("Just boots...");
            else Debug.Log("Idk what this item is");
            ItemPickUp(collision.gameObject);
            collision.GetComponent<Item>().isPickedUp = true;
        }
    }


    /// <summary>
    /// Item handler if the Item the player just interacted with is type of      Miscellaneous
    /// <para>WARNING: method incomplete</para>
    /// </summary>
    /// <param name="collision"></param>
    private void IfMiscellaneousItem(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>().itemType == Item.ItemType.miscelaneous)
        {

        }
    }
    #endregion


    /// <summary>
    /// Reduces Players's health by ~damage
    /// <para>Uses Customizeable invincibilityTimer</para>
    /// <para>For Continuous Damaging</para>
    /// <para>USAGE EXAMPLE: Poisoning damage of 1 every 1/4 of a second, etc.</para>
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    /// <param name="hitEverySoLong">in Seconds</param>
    /// <param name="forSoLong">in Seconds</param>
    public void TakeDamageContinuous(int damage, float hitEverySoLong, float forSoLong)
    {
        // WIP 
        // WIP 
        // WIP 
        if (forSoLong > 0f) forSoLong -= Time.deltaTime;
        if (forSoLong > 0f && hitEverySoLong <= 0f)
        {
            if (stats.HurtVFX != null) Instantiate(stats.HurtVFX, gameObject.transform.position, Quaternion.identity);
            stats.health -= damage;
            Debug.Log("Damage DONE!!!  --  " + damage);
        }
    }


    /// <summary>
    /// Reduces Entity's health by ~damage
    /// <para>Uses Default invincibilityTimer</para>
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void TakeDamage(int damage)
    {
        if (invincibilityTimer <= 0f)
        {
            if (stats.HurtVFX != null) Instantiate(stats.HurtVFX, gameObject.transform.position, Quaternion.identity);
            stats.health -= damage;
            Debug.Log("Damage DONE!!!  --  " + damage);
        }
    }


    #region Mini Methods.
    /// <summary>
    /// stats.currentCombo++;
    /// </summary>
    private void ComboCountUp() => stats.currentCombo++;
    /// <summary>
    /// stats.currentCombo = 0;
    /// </summary>
    private void ComboReset() => stats.currentCombo = 0;
    /// <summary>
    /// stats.comboTimer = value;
    /// </summary>
    private void ComboTimeSet(float value) => stats.comboTimer = value;
    #endregion


    // * To-Do - implement properly the casttimer     AND     Play animations code
}
