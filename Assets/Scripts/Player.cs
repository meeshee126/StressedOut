using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    private Stats stats;
    private Rigidbody2D characterRB;
    private Animator characterAnimator;
    private CapsuleCollider2D characterCollider;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        characterRB = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterCollider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {

        ApplyInput();
        //comboTimer -= Time.deltaTime;
        //if (comboTimer < 0f) lastCastID = "";

        //AbilityFilterHandling();
        ////CooldownManager();
    }


    /// <summary>
    /// Translates  [User Input]
    /// into        [Player Movement].
    /// </summary>
    void ApplyInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * movementSpeed;
        float moveVertical = Input.GetAxis("Vertical") * movementSpeed;

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






    //public enum CastInput
    //{
    //    none = 0,
    //    left = 1,
    //    up = 2,
    //    right = 3
    //}

    //public GameObject[] castedAbilities = new GameObject[200];
    //public GameObject CastPrefab;

    //private bool doesHitAll;
    //private string lastCastID;
    //private float comboTimer;


    


    ////public void CooldownManager()
    ////{
    ////    if (castedAbilities[199] != null)
    ////    {
    ////        for (int i = 0; i < castedAbilities.Length; i++)
    ////        {
    ////            if (castedAbilities[i] == null)
    ////            {
    ////                castedAbilities[i] = castedAbilities[199];
    ////                castedAbilities[199] = null;
    ////            }
    ////        }
    ////    }

    ////    for (int i = 0; i < castedAbilities.Length; i++)
    ////    {
    ////        if (castedAbilities[i] != null)
    ////        {
    ////            if (castedAbilities[i].GetComponent<Ability>().Cooldown <= 0f)
    ////            {
    ////                castedAbilities[i] = null;
    ////            }
    ////        }
    ////    }
    ////}


    //public void ComboManager()
    //{

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
    #endregion
}
