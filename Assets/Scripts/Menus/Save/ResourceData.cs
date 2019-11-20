using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
[System.Serializable]
public class ResourceData
{
	public int wood, stone, iron, gold, diamond;

	//Create SaveFiles form the ResourceManager Datas
	public ResourceData(ResourceManager resource)
	{
		wood = resource.wood;
		stone = resource.stone;
		iron = resource.iron;
		gold = resource.gold;
		diamond = resource.diamond;
	}
}
