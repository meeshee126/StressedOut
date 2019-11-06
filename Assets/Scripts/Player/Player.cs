using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Stats))]

public class Player : MonoBehaviour
{
    private ItemsList itemList;
    private Stats stats;
    private Rigidbody2D characterRB;
    private Animator characterAnimator;
    private CapsuleCollider2D characterCollider;
    WeaponStats weaponStats;
    ResourceManager resourceManager;

    TimeBehaviour timeBehaviour;
    GameObject gameManager;

    public GameObject playerHand;
    public GameObject[] QuickbarContent = new GameObject[5];
    public int health;
    public GameObject Helmet, Suite, Boots;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        resourceManager = GameObject.Find("GameManager").GetComponent<ResourceManager>();
        timeBehaviour = gameManager.GetComponent<TimeBehaviour>();
        itemList = gameManager.GetComponentInChildren<ItemsList>();

        stats = GetComponent<Stats>();
        characterRB = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterCollider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        if (stats.comboTimer > 0f) stats.comboTimer -= Time.deltaTime;
        if (stats.comboTimer <= 0f) stats.comboAttack = 0;
        ItemPickingHandler();
        ApplyMovementInput();
        ApplyAttackInput();
        //AbilityFilterHandling();
        ////CooldownManager();
    }


    /// <summary>
    /// Translates  [User Input]
    /// into        [Player Movement].
    /// </summary>
    void ApplyMovementInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * stats.movementSpeed;
        float moveVertical = Input.GetAxis("Vertical") * stats.movementSpeed;

        //AnimationUpdate(moveHorizontal, moveVertical);

        characterRB.velocity = new Vector2(moveHorizontal, moveVertical);
        #region old
        //transform.Translate(new Vector2(moveHorizontal, moveVertical));
        #endregion
    }


    /// <summary>
    /// Updates the [Player's Animation]
    /// based on    [Player's Movement].
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    void AnimationUpdate(float moveX, float moveY)
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


    void ApplyAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(itemList.itemCollection.Length);

            for (int i = 0; i < itemList.itemCollection.Length; i++)
            {
                if (playerHand.name == itemList.itemCollection[i].name)
                {
                    playerHand = itemList.itemCollection[i];
                }
            }
        }
    }


    // Holds a library of all the available combos with each weapon
    void CombosLibrary()
    {

    }


    //Collider2D GetClosestItem(Collider2D[] itemsFound)
    //{
    //    Collider2D bestTarget = null;
    //    float closestDistanceSqr = 1f;
    //    for (int i = 0; i < itemsFound.Length; i++)
    //    {
    //        if (Vector2.Distance(transform.position, itemsFound[i].transform.position) < closestDistanceSqr)
    //        {
    //            closestDistanceSqr = Vector2.Distance(transform.position, itemsFound[i].transform.position);
    //            bestTarget = itemsFound[i];
    //        }
    //    }
    //    return bestTarget;
    //}


    public void ItemPickingHandler()
    {
        Collider2D[] collisionsInCastArea = Physics2D.OverlapCircleAll(transform.position, 1f);

        for (int i = 0; i < collisionsInCastArea.Length; i++)
        {
            if (collisionsInCastArea[i].GetComponent<Item>() && Input.GetKeyDown(KeyCode.F))
            {
                //if (collisionsInCastArea[i] == GetClosestItem(collisionsInCastArea))
                //{
                    // Is It an Item and Did Player PRESS <"F">

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
                //}
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>()) IfResourceItem(collision);
    }


    // Picks up the reffering item and drops already picked up item
    void ItemPickUp(GameObject pickedItem)
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


    // Item handler if the Item the player just interacted with is type of      None
    void IfNoneItem(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.none)
        {

        }
    }


    // Item handler if the Item the player just interacted with is type of      Resource
    void IfResourceItem(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.resource)
        {
            resourceManager.AddResource(collision.GetComponent<Item>().itemName, collision.GetComponent<Item>().amount);

            collision.GetComponent<Item>().isPickedUp = true;

        }
    }


    // Item handler if the Item the player just interacted with is type of      Consumable
    void IfConsumableItem(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.consumable)
        {
            stats.health += collision.GetComponent<Item>().ItemTypeAttribute;
            collision.GetComponent<Item>().isPickedUp = true;
        }
    }


    // Item handler if the Item the player just interacted with is type of      Weapon
    void IfWeaponItem(Collider2D collision)
    {
        ItemPickUp(collision.gameObject);
    }


    // Item handler if the Item the player just interacted with is type of      Armor
    void IfArmorItem(Collider2D collision)
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


    // Item handler if the Item the player just interacted with is type of      Miscellaneous
    void IfMiscellaneousItem(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>().itemType == Item.ItemType.miscelaneous)
        {

        }
    }


    // Input                                                                                    CHECK
    // Read What the player is holding (Enum)                                                   CHECK
    // READ:   It's attack and combos library
    // Read weapon damage attributes and other attributes
    // Instantiate the attack   AND     Start the read Cooldown
    // Do the the casttimer and attackthing     AND     Play animations


    //for (int i = 0; i < abilities.Length; i++) abilities[i].Cooldown -= Time.deltaTime;

    //public enum Items
    //{
    //    none = 0,
    //    left = 1,
    //    up = 2,
    //    right = 3
    //}

    //public GameObject CastPrefab;

    //private bool doesHitAll;
    //private string lastCastID;
    public GameObject[] castedAbilities = new GameObject[200];


    public void CooldownManager()
    {
        if (castedAbilities[199] != null)
        {
            for (int i = 0; i < castedAbilities.Length; i++)
            {
                if (castedAbilities[i] == null)
                {
                    castedAbilities[i] = castedAbilities[199];
                    castedAbilities[199] = null;
                }
            }
        }

        for (int i = 0; i < castedAbilities.Length; i++)
        {
            if (castedAbilities[i] != null)
            {
                if (castedAbilities[i].GetComponent<Ability>().Cooldown <= 0f)
                {
                    castedAbilities[i] = null;
                }
            }
        }
    }


    //public void ComboManager()
    //{

    //}

    //public void GetAbilityList(Stats stats, int inputedID, bool hitAll)
    //{
    //    selectedAbility = stats.gameObject.GetComponent<CombatHandler>().CastPrefab.GetComponent<Ability>();
    //    hitAllEntities = hitAll;
    //    //stats.gameObject.GetComponent<CombatHandler>().castedAbilities[]
    //    switch (stats.EntityName)
    //    {
    //        #region players
    //        case "Roger": RedPlayer(inputedID); break;
    //        case "Jake": GreenPlayer(inputedID); break;
    //        case "Hera": BluePlayer(inputedID); break;
    //        #endregion

    //        case "BehemothChick": CrawlingChickens(inputedID); break;
    //        case "BigChick": CrawlingChickens(inputedID); break;
    //        case "SmolChick": CrawlingChickens(inputedID); break;
    //        default: break;
    //    }
    //}


    #region This Ability Casting Works
    // selectedAbility = CastPrefab.GetComponent<Ability>();
    // selectedAbility.Initialize(113, Ability.CastType.casualCircle, "One-Hit-Circle", 2, 0.75f, 0.5f, 1f, 20f, null, LayerMask.NameToLayer("Enemy"));
    // lastCastedObject = Instantiate(CastPrefab, gameObject.transform.position, Quaternion.identity);
    #endregion

    #region This works too, but isn't as flexible
    // lastCastedObject = Instantiate(CastPrefab, gameObject.transform.position, Quaternion.identity);
    // lastCastedObject.GetComponent<Ability>().Initialize(1, Ability.CastType.burstCircle, "Cool bCircle", 1, 1f, 1f, 1f, 3f, 5f, null, LayerMask.NameToLayer("Enemy"));
    #endregion

    #region need a pausing timer?
    //IEnumerator WaitForThis(float timeToWait) { yield return new WaitForSeconds(timeToWait); }
    #endregion

    #region some crap


    // public void LeftMethod()
    // {
    //     Skipping Obj Declaration due to prefab use

    //     abilities[0].Initialize(113, Ability.CastType.casualCircle, "One-Hit-Circle", 2, 0.75f, 0.5f, 1f, 20f, null, LayerMask.NameToLayer("Enemy"));
    //     lastCastedObject = Instantiate(CastPrefab, gameObject.transform.position, Quaternion.identity);
    //     ^add a values definer so this can hold variables for each thing.
    // }


    // public void RightMethod()
    // {
    //     Skipping Obj Declaration due to prefab use



    //     abilities[1].Initialize(1, Ability.CastType.burstCircle, "Burst Circle", 50, 0.2f, 0.5f, 90f, 3f, 5f, null, LayerMask.NameToLayer("Enemy"));
    //     lastCastedObject = Instantiate(CastPrefab, gameObject.transform.position, Quaternion.identity);
    // }


    //-------------------------------

    //public void getaValue()
    //{
    //for (int i = 0; i < ItemsList.FindObjectsOfType<GameObject>().Length; i++)
    //{
    //}
    //Debug.Log(ItemsList.FindObjectsOfType<GameObject>().Length);
    //return ItemsList.FindObjectsOfType<GameObject>().Length;
    //.GetProperty(name).GetValue(this, null);
    //return this.GetType().GetProperty(name).GetValue(this, null);
    //}
    #endregion

    #region Cool code actually
    //Debug.Log(System.Enum.GetNames(typeof(ItemsList.Items)).Length);
    //for (int i = 0; i < System.Enum.GetNames(typeof(ItemsList.Items)).Length; i++)
    //{
    //    if (playerHands.ToString() == System.Enum.GetNames(typeof(ItemsList.Items)).GetValue(i).ToString())
    //    {
    //        for (int j = 0; j < itemList.ItemCollection.Length; j++)
    //        {
    //            if (playerHands.ToString() == itemList.ItemCollection[j].name)
    //            {
    //                playerHand = itemList.ItemCollection[j];
    //            }
    //        }
    //    }
    //}
    #endregion
}
