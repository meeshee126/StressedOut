using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	[HideInInspector] public GenerateBase[] building;
	public GameObject[] buildingObj; //GameObject.FindGameObjectsWithTag("Building")

    private void Start()
	{
        building = new GenerateBase[buildingObj.Length];

        for (int i = 0; i < buildingObj.Length; i++)
		{
			building[i] = buildingObj[i].GetComponent<GenerateBase>();
		}
	}

	//Saving
	public void SaveController()
	{
		SaveSystem.SaveBuilding(this);
	}

	//Loading
	public void LoadController()
	{
		BuildingData data = SaveSystem.LoadBuilding();

		for (int i = 0; i < buildingObj.Length; i++)
		{
			if (data.isActive[i] == 0)
			{
				building[i].Wood.SetActive(false);
				building[i].Stone.SetActive(false);

				building[i].isBuild = false;
			}
			else if (data.isActive[i] == 1)
			{
				building[i].Wood.SetActive(true);
				building[i].Stone.SetActive(false);

				building[i].isBuild = true;
			}
			else if (data.isActive[i] == 2)
			{
				building[i].Wood.SetActive(false);
				building[i].Stone.SetActive(true);

				building[i].isBuild = true;
			}
		}
	}
}
