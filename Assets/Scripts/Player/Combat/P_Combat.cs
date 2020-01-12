using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class P_Combat : Player
{
    private void Awake()
    {
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


    /// <summary>
    /// Contributes in the current combo status handling
    /// Allows every other action to run thru based on player's input
    /// </summary>
    void Update()
    {
        CountDownTimers();
        AbilityCooldownManager();
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


    /// <summary>
    /// Filters thru what item the player is holding
    /// <para>what player's current combo status is</para>
    /// <para>and casts the correct attack based on that</para>
    /// </summary>
    private void GetAbility()
    {
        Item item = playerHand.GetComponent<Item>();
        if (item.itemType == Item.ItemType.weapon)
        { // is Weapon ?
            WeaponStats weaponStats = playerHand.GetComponent<WeaponStats>(); // Use to adjust Ability's attributes
            if (item.itemName.Contains("Sword"))
            { // is Sword ?
                switch (stats.currentCombo)
                { // Current Combo ?
                    case 0: AbilitySelector("Entry-Attack"); break;
                    case 1: AbilitySelector("First-MainAttack"); break;
                    case 2: AbilitySelector("Second-MainAttack"); break;
                }
            }
            if (item.itemName.Contains("Bow"))
            { // is Bow?
                // Insert Bow Mechanics
            }
        }
                               
        if (item.itemType != Item.ItemType.weapon) // is non-Weapon ?   
            AbilitySelector("NonWeaponAttack");

        // Read from Combo manager
        // Filter thru the abilities available for the currently holding item
    }


    /// <summary>
    /// Filters thru player's abilities
    /// <para>Also handles player's combo status</para>
    /// </summary>
    /// <param name="selectedAbilityName">Ability name to search for</param>
    private void AbilitySelector(string selectedAbilityName)
    {
        for (int i = 0; i < abilityLists.playerAbilities.Length; i++) // Run through List.
            if (abilityLists.playerAbilities[i].GetComponent<Ability>().CastName == selectedAbilityName)
            { // Ability Found ?
                ComboCountUp();
                ComboTimeSet(1.5f);
                if (stats.currentCombo > 2) // Max-Combo Reached ?
                    ComboReset();
                // Set NEW Ability Values.
                selectedAbility.GetComponent<Ability>().
                    SetValues(abilityLists.playerAbilities[i].GetComponent<Ability>());
            }
    }

    struct Abilities
    {
    };

    /// <summary>
    /// Simply allows all abilities that are on cooldown to run through the process of cooling down
    /// </summary>
    private void AbilityCooldownManager()
    {
        for (int i = 0; i < cooldownsListCopy.Length; i++)  // Run  through AbilitiesCooldowns List.
            if (cooldownsListCopy[i] > 0f)                  // is on Cooldown ?
                cooldownsListCopy[i] -= Time.deltaTime;     // Count Down Cooldown.

        for (int i = 0; i < castTimeListCopy.Length; i++)   // Run through AbilitiesCastTimes List.
            if (castTimeListCopy[i] > 0f)                   // is on CastTime ?
                castTimeListCopy[i] -= Time.deltaTime;      // Count Down CastTime.

        if (stats.comboTimer > 0f)                          // is ComboTime not done ?
            stats.comboTimer -= Time.deltaTime;             // Count Down ComboTime.
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
}
