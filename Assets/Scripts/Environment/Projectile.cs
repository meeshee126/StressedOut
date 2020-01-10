using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
public class Projectile : MonoBehaviour
{
    float speed = 20;

    private GameObject enemy;
    //private Vector2 target;
    [SerializeField]
    private GameObject hitParticle;

	Entity entity;

	private void Start()
    {
        //target = new Vector2(enemy.position.x, enemy.position.y);

		entity = GameObject.FindWithTag("Enemy").GetComponent<Entity>();
	}

    private void Update()
    {
		FindClosestEnemy();

		//The projectile move himself to the target
		transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);

        if (transform.position.x == enemy.transform.position.x && transform.position.y == enemy.transform.position.y)
        {
			entity.TakeDamage(1);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
		}

        if (GameObject.FindWithTag("Enemy") != null)
        {
            RotateTowards(enemy.transform.position);
        }

        else
        {
            return;
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
