using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
[System.Serializable]
public class PlayerData
{
	public int health;
	public float[] position;

	//Create SaveFiles form the Player Datas
	public PlayerData(Stats player)
	{
		health = player.health;

		position = new float[2];
		position[0] = player.transform.position.x;
		position[1] = player.transform.position.y;
	}
}
