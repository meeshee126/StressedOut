using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt
public class GeneratorManager : MonoBehaviour
{
    Vector3 offset;
    float radius;
    LayerMask mask;
    float spacing;
    int spawnMin;
    int spawnMax;
    GameObject spawnObject;
    Collider2D[] colliders;

    public GeneratorManager(Vector3 offset, float radius, LayerMask mask, 
                            float spacing, int spawnMin, int spawnMax,
                            GameObject spawnObject, Collider2D[] colliders)
    {
        this.offset = offset;
        this.radius = radius;
        this.mask = mask;
        this.spacing = spacing;
        this.spawnMin = spawnMin;
        this.spawnMax = spawnMax;
        this.spawnObject = spawnObject;
        this.colliders = colliders;
    }

    /// <summary>
    /// Spawns GameObject in a given offset
    /// </summary>
    /// <param name="generator"></param>
    public void SpawnObject(Transform generator)
    {
        int dropCount = Random.Range(spawnMin, spawnMax);

        Vector3 spawnPosition = new Vector3();

        bool canSpawnHere = false;

        int catcher = 0;

        // spawns between an offset area
        for (int i = 0; i < dropCount; i++)
        {
            do
            {
                spawnPosition = generator.position + new Vector3(Random.Range(-offset.x / 2, offset.x / 2),
                                                                 Random.Range(-offset.y / 2, offset.y / 2), 
                                                                 0);

                canSpawnHere = PreventSpawnOverlap(spawnPosition, generator);

                //spawn gameobject when not overlaping with other 
                if (canSpawnHere)
                {
                    GameObject newObject = Instantiate(spawnObject, spawnPosition, Quaternion.identity, generator) as GameObject;
                    break;
                }

                catcher++;

                //prevent while loop crash
                if (catcher > 50)
                {
                    Debug.Log("Too many attempts");
                    break;
                }
            } while (true);
        }
    }

    /// <summary>
    /// Avoids overlaping instantiates
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="generator"></param>
    /// <returns></returns>
    bool PreventSpawnOverlap(Vector3 spawnPosition, Transform generator)
    {
        colliders = Physics2D.OverlapCircleAll(generator.position, radius, mask);

        for (int i = 0; i < colliders.Length; i++)
        {
            //Get all bounds in colliding area
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtend = centerPoint.x - width - spacing;
            float rightExtend = centerPoint.x + width + spacing;
            float lowerExtend = centerPoint.y - height - spacing;
            float upperExtend = centerPoint.y + height + spacing;

            //Check overlaping
            if (spawnPosition.x >= leftExtend && spawnPosition.x <= rightExtend)
            {
                if (spawnPosition.y >= lowerExtend && spawnPosition.y <= upperExtend)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
