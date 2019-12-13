using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Michael Schmidt
//Henrick Hafner
public class ObjectGenerator : MonoBehaviour
{
    [Header("Set Spawn offset")]
    [SerializeField]
    Vector3 offset;

    [Header("Collider configurations")]
    [SerializeField]
    float radius;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    float spacing;

    [Header("Spawn configurations")]
    [SerializeField]
    [Range(0,20)]
    int spawnMin;
    [SerializeField]
    [Range(0,20)]
    int spawnMax;
    [SerializeField]
    GameObject areaObject;

    int spawnCount;

    Collider2D[] colliders;
    GeneratorManager generatorManager;

    public void Awake()
    {
        //Michael Schmidt
        //Generate gatherrables when start scene
        Generation();
    }

    //Michael Schmidt
    /// <summary>
    /// Generate new gatherrables
    /// </summary>
    public void Generation()
    {
        generatorManager = new GeneratorManager(offset, radius, mask,
                                                              spacing, spawnMin, spawnMax,
                                                              areaObject, colliders);

        generatorManager.SpawnObject(this.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(this.transform.position, offset);
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
