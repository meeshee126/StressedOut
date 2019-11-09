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

    private Transform enemy;

    Vector3 EnemyDistance;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;

        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
		//Finds the Enemy and start to instantiate projectiles
        if (GameObject.FindWithTag("Enemy") != null)
        {
            EnemyDistance = GameObject.FindWithTag("Enemy").transform.position;
            distance = Vector3.Distance(transform.position, EnemyDistance);

            //Check the distance to the nearest enemy and start shooting at him
            if (distance <= 4)
            {
                if (timeBtwShots <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }

                RotateTowards(enemy.position);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
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
