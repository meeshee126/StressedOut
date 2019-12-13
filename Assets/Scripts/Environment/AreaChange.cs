using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
public class AreaChange : MonoBehaviour
{
	public Camera mainCamera;
	Vector3 newPosition;
	TimeBehaviour timeBehaviour;

	private void Start()
	{
		timeBehaviour = GameObject.Find("GameManager").GetComponent<TimeBehaviour>();
		newPosition = mainCamera.transform.position;
	}

	/// <summary>
	/// Look for an area that the player collides with and checks where it is to move the player and the camera. 
	/// </summary>
	/// <param name="enter"></param>
	private void OnTriggerEnter2D(Collider2D area)
	{
		if (area.gameObject.tag == "Area")
		{
			newPosition.x = area.transform.position.x;
			newPosition.y = area.transform.position.y;

			if (transform.position.x < area.transform.position.x - 10)
			{
				transform.position = transform.position + new Vector3(5.5f, 0, 0);
			}
			else if (transform.position.x > area.transform.position.x + 10)
			{
				transform.position = transform.position + new Vector3(-5.5f, 0, 0);
			}
			else if (transform.position.y < area.transform.position.y - 7)
			{
				transform.position = transform.position + new Vector3(0, 3, 0);
			}
			else if (transform.position.y > area.transform.position.y + 7)
			{
				transform.position = transform.position + new Vector3(0, -3, 0);
			}

			// Michael Schmidt:
			// Call timecosts when player entering a new area
			timeBehaviour.timeCost = timeBehaviour.areaChanging;
		}
	}

	private void Update()
	{
		// Moves the camera and making it glide
		if (Vector3.Distance(mainCamera.transform.position, newPosition) > 0.03f)
		{
			mainCamera.transform.position += (newPosition - mainCamera.transform.position) * 0.15f;
		}
		else
		{
			mainCamera.transform.position = newPosition;
		}
	}
}
