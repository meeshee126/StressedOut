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

    TimeBehaviour timeBehaviour;
    GameObject gameManager;
    //for testing

    public GameObject playerHand;
    public GameObject[] QuickbarContent = new GameObject[5];
    public GameObject Helmet, Suite, Boots;


    private void Awake()
    {
        //for testing
        gameManager = GameObject.Find("GameManager");
        timeBehaviour = gameManager.GetComponent<TimeBehaviour>();
        itemList = gameManager.GetComponentInChildren<ItemsList>();

        stats = GetComponent<Stats>();
        characterRB = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterCollider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;
        }
        if (stats.comboTimer > 0f) stats.comboTimer -= Time.deltaTime;
        if (stats.comboTimer <= 0f) stats.comboAttack = 0;
        ApplyInput();
        CastAttack();
        //AbilityFilterHandling();
        ////CooldownManager();
    }


    /// <summary>
    /// Translates  [User Input]
    /// into        [Player Movement].
    /// </summary>
    void ApplyInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * stats.movementSpeed;
        float moveVertical = Input.GetAxis("Vertical") * stats.movementSpeed;

        //transform.Translate(new Vector2(moveHorizontal, moveVertical));
        //AnimationUpdate(moveHorizontal, moveVertical);

        characterRB.velocity = new Vector2(moveHorizontal, moveVertical);
    }


    /// <summary>
    /// Updates the [Player's Animation]
    /// based on    [Player's Movement].
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    void AnimationUpdate(float moveX, float moveY)
    {
        //Changes Animation Based on direction facing.
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


    void CastAttack()
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


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>().itemType == Item.ItemType.armor)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (collision.GetComponent<Item>().itemName.Contains("Helmet"))
                {
                    Debug.Log("Yes the item contains Helmet");
                }
                else if (collision.GetComponent<Item>().itemName.Contains("Suite")) Debug.Log("No it contains Suite Instead");
                else if (collision.GetComponent<Item>().itemName.Contains("Boots")) Debug.Log("Just boots...");
                else Debug.Log("Idk what this item is");
                ItemPickUp(collision.gameObject);
                collision.GetComponent<Item>().isPickedUp = true;
            }
        }
        if (collision.GetComponent<Item>().itemType == Item.ItemType.weapon)
        {
            // Open up closest collision GUI
            if (Input.GetKeyDown(KeyCode.F))
            {
                ItemPickUp(collision.gameObject);
                collision.gameObject.GetComponent<Item>().isPickedUp = true;
            }
        }
        if (collision.GetComponent<Item>().itemType == Item.ItemType.consumable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                collision.GetComponent<Item>().isPickedUp = true;
            }
        }
        if (collision.GetComponent<Item>().itemType == Item.ItemType.resource)
        {

        }
        if (collision.gameObject.GetComponent<Item>().itemType == Item.ItemType.miscelaneous)
        {

        }
    }


    void ItemPickUp(GameObject pickedItem)
    {
        Instantiate(playerHand, transform.position, Quaternion.identity);
        playerHand = pickedItem;
    }


    void CombosLibrary()
    {
        
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
