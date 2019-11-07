using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class EntityBehaviour : MonoBehaviour
{
    //Set target = Player
    public GameObject target;

    [Header("Set radiuses to target")]
    public float observationRadius;
    public float attackRadius;
    public float lostRadius;

    [Header("Entity Configurations")]
    public float movementSpeed;

    [Header("Infight checks")]
    public bool attack;
    public bool idle;
}
