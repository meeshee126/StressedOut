using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
public class Turret : MonoBehaviour
{
    private float timeBtwShots;
    float startTimeBtwShots = 0.5f;
    float distance;

    public GameObject projectile;
    public GameObject fireSFX;

    private GameObject enemy;

    void Start()
    {
        timeBtwShots = startTimeBtwShots;
       
    }

    void Update()
    {
		FindClosestEnemy();

		//Finds the Enemy and start to instantiate projectiles
		if (enemy != null)
		{
			distance = Vector3.Distance(transform.position, enemy.transform.position);

			//Check the distance to the nearest enemy and start shooting at him
			if (distance <= 4)
			{
				if (timeBtwShots <= 0)
				{
					Instantiate(projectile, transform.position, Quaternion.identity);
                    
					timeBtwShots = startTimeBtwShots;

                    //Michael Schmidt
                    //play audio when firing
                    if (fireSFX != null) Instantiate(fireSFX, this.transform.position, Quaternion.identity);
				}
				else
				{
					timeBtwShots -= Time.deltaTime;
				}
			}

			RotateTowards(enemy.transform.position);
		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	/// <summary>
	/// Find the closest enemy
	/// </summary>
	/// <returns></returns>
	public float FindClosestEnemy()
    {        
		float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;

            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
				enemy = go;
                distance = curDistance;
            }
        }
        return distance;
    }

	/// <summary>
	/// Select the enemys position to rotate the Turret to the enemy
	/// </summary>
	/// <param name="target"></param>
	private void RotateTowards(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
