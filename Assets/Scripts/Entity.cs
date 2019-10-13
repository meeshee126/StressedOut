using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds Components to the gameObject when script Component is inserted
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]

public class Entity : MonoBehaviour
{
    Stats stats;

    public float movementSpeed;

    [SerializeField]
    private float startWaitTime;

    private float waitTime, minimumRoamDistance = 10f;

    private int randomSpot;

    public Transform[] moveSpots;
    public GameObject lowDamageBloodParticle;
    public GameObject highDamageBloodParticle;
    private Rigidbody2D characterRigidbody;
    private Animator characterAnimator;
    private CapsuleCollider2D characterCapsuleCollider;
    private RaycastHit2D raycastHit2D;

    private void Awake()
    {
        //Finds Component in gameObject and uses it
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterCapsuleCollider = GetComponent<CapsuleCollider2D>();
        randomSpot = Random.Range(0, moveSpots.Length);
        waitTime = startWaitTime;
        stats = GetComponent<Stats>();
    }

    void FixedUpdate()
    {
        if (stats.health <= 0)
        {
            Destroy(gameObject);
        }
        if (characterRigidbody != null && characterCapsuleCollider != null && characterAnimator != null)
        {
            MovementHandler();
        }
        else if (characterCapsuleCollider == null) Debug.LogWarning("Capsule Collider not attached to " + gameObject.name);
        else if (characterAnimator == null) Debug.LogWarning("Animator not attached to " + gameObject.name);
        else if (characterRigidbody == null) Debug.LogWarning("Rigidbody not attached to " + gameObject.name);
    }


    public void MovementHandler()
    {
        //Point-Patroll
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.5f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }


        // Path Finding towards TARGET
        // Use Tags?
    }


    public void TakeDamage(int damageTaken)
    {
        Instantiate(lowDamageBloodParticle, gameObject.transform.position, Quaternion.identity);
        stats.health -= damageTaken;
        Debug.Log("Damage DONE!!!  --  " + damageTaken);
    }

    // Add Controlled method (when hit by CC)

    public void Neutral()
    {

    }
    public void Friendly()
    {

    }
    public void Enemy()
    {

    }
}
