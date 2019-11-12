using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
public class ObjectGeneration : MonoBehaviour
{
    public int spawnCount = 10;
    public GameObject AreaObject;

    private void Start()
    {
        spawnObjects();
    }

	// Randomly generates objects in a specific area
	void spawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(AreaObject, new Vector3(transform.position.x + Random.Range(0, 18), transform.position.y + Random.Range(0, 8), transform.position.z), Quaternion.identity, this.transform);
        }
    }
}
