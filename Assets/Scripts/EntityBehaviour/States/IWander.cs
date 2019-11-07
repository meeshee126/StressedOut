using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IWander : MonoBehaviour, IState
{
    EntityBehaviour entity;

    //get target from entityBehavior class
    GameObject target => entity.target;

    //Set Layer for this state
    int environmentLayer = 1 << 9;

    float count;

    void Start()
    {
        //Set Entity rotation to default
        entity.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public IWander(EntityBehaviour entity)
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
        Move();

        //Check if Entity is walking against enviroment objects
        if (!Walkable())
        {
            AssignRandomRotation();
        }
    }

    /// <summary>
    /// Check Entities path is walkable
    /// </summary>
    /// <returns></returns>
    bool Walkable()
    {
        Vector3 endpoint = entity.transform.up;

        //ray only hits environment objects
        RaycastHit2D hit = Physics2D.Raycast(entity.transform.position, entity.transform.up, 1, environmentLayer);

        //Set raylenght for scene view
        Debug.DrawRay(entity.transform.position, endpoint, Color.red);

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
            entity.transform.rotation = Quaternion.Euler(0, 0, 0);

            //Set random rotation 
            AssignRandomRotation();

            //Entity is not in fight anymore
            entity.idle = true;
        }
    }

    /// <summary>
    /// Entity walking towards a random rotation
    /// </summary>
    void Move()
    {
        //Moving towards
        entity.transform.position += entity.gameObject.transform.up * entity.movementSpeed * Time.deltaTime;

        count += Time.deltaTime;

        //assign a random rotation after a an amount of time
        if (count > Random.Range(4, 6))
        {
            AssignRandomRotation();
            count = 0;
        }
    }

    /// <summary>
    /// Set Entities rotation 
    /// </summary>
    public void AssignRandomRotation()
    {
        //get directions (up, right, down, left)
        float[] direction = new float[] { 0, 90, 180, 270 };

        //set direction
        entity.transform.Rotate(0, 0, direction[Random.Range(0, direction.Length)]);
    }

    /// <summary>
    /// Check distance between entity and target
    /// </summary>
    /// <returns></returns>
    float GetRadiusToTarget()
    {
        return Vector3.Distance(entity.gameObject.transform.position, target.gameObject.transform.position);
    }
}
