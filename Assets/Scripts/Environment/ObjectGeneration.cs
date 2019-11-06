using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public int spawnCount = 10;
    public GameObject AreaObject;

    private void Start()
    {
        spawnGrenades();
    }

    void spawnGrenades()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(AreaObject, new Vector3(transform.position.x + Random.Range(0, 18), transform.position.y + Random.Range(0, 8), transform.position.z), Quaternion.identity);
        }
    }
}
