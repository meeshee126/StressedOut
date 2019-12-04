using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
	public GameObject CraftMenu;

	private void OnTriggerStay2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
		{
			CraftMenu.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			CraftMenu.SetActive(false);
		}
	}
}
