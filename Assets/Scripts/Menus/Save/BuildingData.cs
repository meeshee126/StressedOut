using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
[System.Serializable]
public class BuildingData
{
	public int building;

	public BuildingData(GenerateBase generateBase)
	{
		if (generateBase.Wood.gameObject.activeSelf == false && generateBase.Stone.gameObject.activeSelf == false)
		{
			building = 0;
		}
		else if (generateBase.Wood.gameObject.activeSelf == true && generateBase.Stone.gameObject.activeSelf == false)
		{
			building = 1;
		}
		else if (generateBase.Wood.gameObject.activeSelf == false && generateBase.Stone.gameObject.activeSelf == true)
		{
			building = 2;
		}
	}
}
