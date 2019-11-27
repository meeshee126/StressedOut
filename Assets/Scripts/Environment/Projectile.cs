using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
public class Projectile : MonoBehaviour
{
    float speed = 20;

    private Transform enemy;
    private Vector2 target;

	Entity entity;

	private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;

        target = new Vector2(enemy.position.x, enemy.position.y);

		entity = GameObject.FindWithTag("Enemy").GetComponent<Entity>();
	}

    private void Update()
    {
		//The projectile move himself to the target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
			entity.TakeDamage(1);
		}

        if (GameObject.FindWithTag("Enemy") != null)
        {
            RotateTowards(enemy.position);
        }

        else
        {
            return;
        }
    }

	/// <summary>
	/// Select the enemys position to rotate the projectile to the enemy, so face the projectile always at the target
	/// </summary>
	/// <param name="target"></param>
	private void RotateTowards(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
