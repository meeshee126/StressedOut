using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class IWander : MonoBehaviour, IState
{
    EntityBehaviour entity;

    GameObject target => entity.target;

    int environmentLayer = 1 << 9;

    float count;

    void Start()
    {
        entity.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public IWander(EntityBehaviour entity)
    {
        this.entity = entity;
    }
    
    public bool Condition()
    {
        float distance = GetRadiusToTarget();
        return distance > this.entity.observationRadius;
    }

    public void Execute()
    {
        SetIdle();
        Move();

        if (!Walkable())
        {
            AssignRandomRotation();
        }
    }

    float GetRadiusToTarget()
    {
        return Vector3.Distance(entity.gameObject.transform.position, target.gameObject.transform.position);
    }

    bool Walkable()
    {
        Vector3 endpoint = entity.transform.up;

        RaycastHit2D hit = Physics2D.Raycast(entity.transform.position, entity.transform.up, 1, environmentLayer);

        Debug.DrawRay(entity.transform.position, endpoint, Color.red);

        if (hit)
        {
            Debug.Log(hit.collider.name + "In range");
            return false;
        }

        return true;  
    }

    void SetIdle()
    {
        if(!entity.idle)
        {
            entity.transform.rotation = Quaternion.Euler(0, 0, 0);
            AssignRandomRotation();
            entity.idle = true;
        }
    }

    void Move()
    {
        entity.transform.position += entity.gameObject.transform.up * entity.movementSpeed * Time.deltaTime;

        count += Time.deltaTime;

        if (count > Random.Range(4, 6))
        {
            AssignRandomRotation();
            count = 0;
        }
    }


    public void AssignRandomRotation()
    {
        float[] degree = new float[] { 0, 90, 180, 270 };

        entity.transform.Rotate(0, 0, degree[Random.Range(0, degree.Length)]);
       
    }

}
