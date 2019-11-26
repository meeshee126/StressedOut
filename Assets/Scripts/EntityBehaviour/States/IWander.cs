using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IWander : MonoBehaviour, IState
{
    Entity entity;

    //get target from entityBehavior class
    GameObject target => entity.target;

    //Set Layer for this state
    int environmentLayer = 1 << 9;

    float count;


    public IWander(Entity entity)
    {
        this.entity = entity;
    }


    /// <summary>
    /// Check this class Condition
    /// </summary>
    /// <returns></returns>
    public bool Condition()
    {
        float distance = GetRadiusToTarget();

        //Check if target ist outside entities radius
        return distance > this.entity.observationRadius;
    }


    /// <summary>
    /// Execute this state
    /// </summary>
    public void Execute()
    {
        SetRotation();
        if (entity.waitTime > 0f) entity.waitTime -= Time.deltaTime;
        if (entity.waitTime <= 0f)
        {
            entity.characterRB.mass = 3;
            Move();
        }

        //Check if Entity is walking against enviroment objects
        //WHILE stwitched with IF
        if (!Walkable())
        {
            AssignRandomRotation();
        }
    }


    /// <summary>
    /// Entity walking towards a random rotation
    /// </summary> 
    void Move()
    {
        //Moving towards
        entity.characterRB.velocity = entity.facingDirection.transform.up * 100 * entity.stats.movementSpeed * Time.deltaTime;

        //entity.transform.position += entity.gameObject.transform.up * entity.stats.movementSpeed * Time.deltaTime;

        count += Time.deltaTime;

        //assign a random rotation after a an amount of time
        if (count > Random.Range(1.5f, 5f))
        {
            AssignRandomRotation();
            count = 0;
        }
    }


    /// <summary>
    /// Check Entities path is walkable
    /// </summary>
    /// <returns></returns>
    bool Walkable()
    {
        Vector3 endpoint = entity.facingDirection.transform.up;

        //ray only hits environment objects
        RaycastHit2D hit = Physics2D.Raycast(entity.facingDirection.transform.position, entity.facingDirection.transform.up, 1, environmentLayer);

        //Set raylenght for scene view
        Debug.DrawRay(entity.facingDirection.transform.position, endpoint, Color.red);

        if (hit)
        {
            Debug.Log(hit.collider.name + "In range");
            return false;
        }

        return true;  
    }


    /// <summary>
    /// Reset Entity rotation
    /// </summary>
    void SetRotation()
    {
        //if Entity lost his target
        if(!entity.idle)
        {
            //Return default rotation
            entity.facingDirection.transform.rotation = Quaternion.Euler(0, 0, 0);

            //Set random rotation 
            AssignRandomRotation();

            //Entity is not in fight anymore
            entity.idle = true;
            entity.stats.movementSpeed = 1;
        }
    }


    /// <summary>
    /// Set Entities rotation 
    /// </summary>
    public void AssignRandomRotation()
    {
        entity.characterRB.velocity = new Vector3(0f, 0f);

        // Set a random Waiting time
        entity.waitTime = Random.Range(0f, 5f);
        entity.characterRB.mass = 300;

        entity.facingDirection.transform.Rotate(0, 0, Random.Range(0, 360)/*direction[Random.Range(0, direction.Length)]*/);
        // Set direction
    }


    /// <summary>
    /// Check distance between entity and target
    /// </summary>
    /// <returns></returns>
    float GetRadiusToTarget()
    {
        return Vector3.Distance(entity.facingDirection.transform.position, target.gameObject.transform.position);
    }
}
