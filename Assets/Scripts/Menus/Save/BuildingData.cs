using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
[System.Serializable]
public class BuildingData
{
	public int[] isActive;
	public int[] health;

	//Create SaveFiles form the Building Datas
	public BuildingData(BuildingManager buildingManager)
	{
        GameObject[] gameObjects = buildingManager.buildingObj;

        isActive = new int[gameObjects.Length];
		health = new int[gameObjects.Length];

		for (int i = 0; i < gameObjects.Length; i++)
		{
            GenerateBase currentBuilding = gameObjects[i].GetComponent<GenerateBase>();
			health[i] = currentBuilding.health;

			if (currentBuilding.Wood.gameObject.activeSelf == false && currentBuilding.Stone.gameObject.activeSelf == false && currentBuilding.Iron.gameObject.activeSelf == false && currentBuilding.Ruin.gameObject.activeSelf == false)
			{
				isActive[i] = 0;
			}
			else if (currentBuilding.Wood.gameObject.activeSelf == true && currentBuilding.Stone.gameObject.activeSelf == false && currentBuilding.Iron.gameObject.activeSelf == false && currentBuilding.Ruin.gameObject.activeSelf == false)
			{
				isActive[i] = 1;
			}
			else if (currentBuilding.Wood.gameObject.activeSelf == false && currentBuilding.Stone.gameObject.activeSelf == true && currentBuilding.Iron.gameObject.activeSelf == false && currentBuilding.Ruin.gameObject.activeSelf == false)
			{
				isActive[i] = 2;
			}
			else if (currentBuilding.Wood.gameObject.activeSelf == false && currentBuilding.Stone.gameObject.activeSelf == false && currentBuilding.Iron.gameObject.activeSelf == false && currentBuilding.Ruin.gameObject.activeSelf == true)
			{
				isActive[i] = 3;
			}
			else if (currentBuilding.Wood.gameObject.activeSelf == false && currentBuilding.Stone.gameObject.activeSelf == false && currentBuilding.Iron.gameObject.activeSelf == true && currentBuilding.Ruin.gameObject.activeSelf == false)
			{
				isActive[i] = 4;
			}
		}
	}
}