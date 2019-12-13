using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt
public class EntityGenerator : MonoBehaviour
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
    [Range(0, 20)]
    int spawnMin;
    [SerializeField]
    [Range(0, 20)]
    int spawnMax;
    [SerializeField]
    GameObject entiyObject;

    int spawnCount;

    Collider2D[] colliders;
    GeneratorManager generatorManager;

    /// <summary>
    /// Spawns enemys in all areas except base area when night
    /// </summary>
    public void GenerateEntities()
    {
        generatorManager = new GeneratorManager(offset, radius, mask,
                                                spacing, spawnMin, spawnMax,
                                                entiyObject, colliders);

        generatorManager.SpawnObject(this.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(this.transform.position, offset);
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
